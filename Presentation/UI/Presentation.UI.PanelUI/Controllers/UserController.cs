using AutoMapper;
using Core.Application.Dtos;
using Core.Application.Dtos.EmailDtos;
using Core.Application.Dtos.LoginDtos;
using Core.Application.Features.Commands.OwnerEntityCommands.Commands;
using Core.Application.Features.Commands.UserCommands.Commands;
using Core.Application.Features.Commands.UserRegisterCodeCommands.Commands;
using Core.Application.Features.Queries.UserQueries.Queries;
using Core.Application.Features.Queries.UserResetPasswordQueries.Queries;
using Core.Application.Helper;
using Core.Application.Interfaces.Dtos;
using Core.Application.Interfaces.Repositories;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace Presentation.UI.PanelUI.Controllers
{
    [AllowAnonymous]
    public class UserController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IJwtRepository _jwtRepository;
        private readonly string _secretKey;
        private readonly IHttpClientFactory _httpClientFactory;
        public UserController(IMediator mediator, IMapper mapper, IJwtRepository jwtRepository, IConfiguration configuration, IHttpClientFactory httpClientFactory)
        {
            _mediator = mediator;
            _mapper = mapper;
            _jwtRepository = jwtRepository;
            _secretKey = configuration["JwtBearer:ResetPasswordKey"]
                     ?? throw new Exception("ResetPasswordKey is not configured in appsettings.json");
            _httpClientFactory = httpClientFactory;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ForgotPassword()
        {
            return View();
        }

        public IActionResult LoginPage()
        {
            return View();
        }


        #region Register

        public IActionResult RegisterPage()
        {
            return View();
        }

        [HttpPost]
        [EnableRateLimiting("AoGenLimit")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            try
            {
                IResultDataDto<UserDto> resultUser = await this._mediator.Send(new GetByEmailUserQuery() { Email = registerDto.Email });
                if (resultUser.IsSuccess) return BadRequest("User already exist!");

                IResultDataDto<OwnerEntityDto> resultOwner = await this._mediator.Send(new AddOwnerEntityCommand() { OwnerTitle = $"{registerDto.Name} {registerDto.Surname}" });
                if (!resultOwner.IsSuccess) return BadRequest(resultOwner.Error);

                var userCommand = _mapper.Map<AddUserCommand>(registerDto);
                userCommand.OwnerId = resultOwner.Data.Id;
                userCommand.IsInvited = false;
                userCommand.PasswordHash = Cipher.Encrypt(registerDto.PasswordHash);
                IResultDataDto<UserDto> result = await this._mediator.Send(userCommand);
                if (!result.IsSuccess) return BadRequest(result.Error);

                IResultDataDto<UserRegisterCodeDto> resUserRegCode = await this._mediator.Send(new AddUserRegisterCodeCommand() { UserId = result.Data.Id });
                if (!resUserRegCode.IsSuccess) return BadRequest(resUserRegCode.Error);

                IResultDataDto<EmailDto> resEmail = await this._mediator.Send(new GetEmailByTypeQuery() { EmailType = 2 });
                if (!resEmail.IsSuccess) return BadRequest(resEmail.Error);

                string fullname = result.Data.Name + " " + result.Data.SurName;

                string bodyHTML = resEmail.Data.HtmlBody.Replace("{code}", resUserRegCode.Data.RegisterCode.ToString())
                    .Replace("{fullName}", (result.Data.Name + " " + result.Data.SurName).ToString());

                string subjectTitle = resEmail.Data.Subject;

                var client = _httpClientFactory.CreateClient("InternalApiClient");

                var response = await client.PostAsJsonAsync("api/internal/email/send", new
                {
                    emailFormat = resEmail.Data,
                    toEmail = result.Data.Email,
                    subject = subjectTitle,
                    body = bodyHTML
                });

                TransferEntryInfoDto transferEncode = new();
                transferEncode.EncodedUserId = Cipher.EncryptUserId(result.Data.Id.ToString(), _secretKey);
                return Ok(transferEncode.EncodedUserId);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        #endregion

        public async Task<IActionResult> CodePage([FromQuery] string userId)
        {
            var transferEncode = TransferHelper.DecodeUserId(userId);

            IResultDataDto<UserDto> result = await this._mediator.Send(new GetByIdUserQuery() { Id = Guid.Parse(transferEncode.DecodedUserId) });

            if (!result.IsSuccess) { return View(transferEncode); }

            return View(transferEncode);
        }

        [HttpPost]
        public async Task<IActionResult> CheckCode([FromForm] string resetCode, [FromForm] string userId)
        {
            var transferEncode = TransferHelper.DecodeUserId(userId);

            IResultDataDto<UserResetPasswordDto> res = await this._mediator.Send(new CheckResetCodeQuery() { ResetCode = resetCode, UserId = Guid.Parse(transferEncode.DecodedUserId) });

            if (res.IsSuccess) { return Ok(userId); }

            return BadRequest("Code doesn't match!");
        }

        public async Task<IActionResult> ResetPassword([FromQuery] string userId)
        {
            var transferEncode = TransferHelper.DecodeUserId(userId);

            IResultDataDto<UserDto> res = await this._mediator.Send(new GetByIdUserQuery() { Id = Guid.Parse(transferEncode.DecodedUserId) });

            if (!res.IsSuccess) { return View(transferEncode); }

            return View(transferEncode);
        }

        [HttpPost]
        [EnableRateLimiting("AoGenLimit")]
        public async Task<IActionResult> UserLogin([FromBody] LoginDto loginDto)
        {
            try
            {
                var map = _mapper.Map<UserLoginCommand>(loginDto);

                IResultDataDto<UserDto> result = await _mediator.Send(map);

                if (!result.IsSuccess) return BadRequest(result.Error);

                HttpContext.Session.SetString("JwtToken", result.Data?.Token);

                var cookieOptions = new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict,
                    Expires = DateTime.UtcNow.AddDays(30)
                };

                HandleXXXLoginCookie(result.Data.Id.ToString(), cookieOptions);

                if (loginDto.RememberMe == true)
                {
                    Response.Cookies.Append("RememberMe", loginDto.RememberMe.ToString(), cookieOptions);
                }

                if (!result.Data.IsSetup) return RedirectToAction("SetupPage", "SetupSetting");

                return Ok(new { Token = result.Data?.Token });
            }
            catch (Exception)
            {
                return BadRequest("An error occurred during login. Please try again later.");
            }
        }


        [HttpPost]
        public IActionResult UserLogout()
        {
            try
            {
                HttpContext.Session.Remove("JwtToken");

                if (Request.Cookies["RememberMe"] != null)
                {
                    Response.Cookies.Delete("RememberMe");
                }

                if (Request.Cookies["XXXLogin"] != null)
                {
                    Response.Cookies.Delete("XXXLogin");
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest("An error occurred while logging out.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdatePassword([FromBody] ResetPasswordDto resetPasswordDto)
        {
            if (resetPasswordDto.NewPassword != resetPasswordDto.ConfirmPassword) { return BadRequest("Password doens't match !"); }

            var transferEncode = TransferHelper.DecodeUserId(resetPasswordDto.UserId);

            IResultDataDto<UserDto> resUser = await this._mediator.Send(new GetByIdUserQuery() { Id = Guid.Parse(transferEncode.DecodedUserId) });
            if (!resUser.IsSuccess) return BadRequest("User not exist!");


            string newPassword = Cipher.Encrypt(resetPasswordDto.NewPassword);

            IResultDataDto<UserDto> res = await this._mediator.Send(new UpdateUserPasswordCommand() { ConfirmedPassword = newPassword, UserId = Guid.Parse(transferEncode.DecodedUserId) });
            if (!res.IsSuccess) return BadRequest("Password could not updated!");


            return Ok(true);
        }

        [HttpPost]
        public async Task<IActionResult> UserAdd([FromBody] UserDto userDto)
        {
            var req = _mapper.Map<AddUserCommand>(userDto);

            if (userDto.Id != Guid.Empty) { return BadRequest(""); }

            IResultDataDto<UserDto> res = await this._mediator.Send(req);
            if (res.IsSuccess) return Ok(res);

            return BadRequest("User is not valid!");
        }

        [HttpPost]
        [EnableRateLimiting("AoGenLimit")]
        public async Task<IActionResult> CheckUserEmail(string email, int emailType)
        {
            try
            {
                IResultDataDto<UserDto> resUser = await this._mediator.Send(new GetByEmailUserQuery() { Email = email });

                if (resUser.IsSuccess)
                {
                    IResultDataDto<UserResetPasswordDto> resUserResPas = await this._mediator.Send(new AddUserResetPasswordCommand() { UserId = resUser.Data.Id });

                    if (resUserResPas.IsSuccess)
                    {
                        string fullname = resUser.Data.Name + " " + resUser.Data.SurName;

                        IResultDataDto<EmailDto> resEmail = await this._mediator.Send(new GetEmailByTypeQuery() { EmailType = emailType });

                        if (resEmail.IsSuccess)
                        {
                            string bodyHTML = resEmail.Data.HtmlBody.Replace("{code}", resUserResPas.Data.ResetCode.ToString())
                                .Replace("{fullName}", (resUser.Data.Name + " " + resUser.Data.SurName).ToString());

                            string subjectTitle = resEmail.Data.Subject;

                            var client = _httpClientFactory.CreateClient("InternalApiClient");

                            var response = await client.PostAsJsonAsync("api/internal/email/send", new
                            {
                                emailFormat = resEmail.Data,
                                toEmail = resUser.Data.Email,
                                subject = subjectTitle,
                                body = bodyHTML
                            });

                            TransferEntryInfoDto transferEncode = new();
                            transferEncode.EncodedUserId = Cipher.EncryptUserId(resUser.Data.Id.ToString(), _secretKey);
                            return Ok(transferEncode.EncodedUserId);
                        }

                        return Ok();
                    }
                }
                else
                {
                    return BadRequest(resUser.Error);
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Email could not send!");
            }

            return BadRequest("User not found or process failed.");
        }




        private void HandleXXXLoginCookie(string userId, CookieOptions cookieOptions)
        {
            const string cookieName = "XXXLogin";

            if (!Request.Cookies.ContainsKey(cookieName))
            {
                // Yeni Cookie Oluştur
                var xxxLoginValue = Cipher.EncryptUserId(userId, _secretKey);
                Response.Cookies.Append(cookieName, xxxLoginValue, cookieOptions);
            }
            else
            {
                // Mevcut Cookie'yi Kontrol Et
                var cookieValue = Request.Cookies[cookieName];
                var parts = cookieValue.Split('|');

                if (parts.Length == 2 && DateTime.TryParse(parts[1], out var expirationDate))
                {
                    if (expirationDate < DateTime.UtcNow)
                    {
                        // Süresi Dolmuşsa Yeniden Oluştur
                        ReplaceXXXLoginCookie(userId, cookieOptions);
                    }
                }
                else
                {
                    // Geçersiz Cookie, Yeniden Oluştur
                    ReplaceXXXLoginCookie(userId, cookieOptions);
                }
            }
        }

        private void ReplaceXXXLoginCookie(string userId, CookieOptions cookieOptions)
        {
            const string cookieName = "XXXLogin";

            Response.Cookies.Delete(cookieName);

            var newExpiration = DateTime.UtcNow.AddMinutes(30);
            var newXxxLoginValue = $"{Cipher.EncryptUserId(userId, _secretKey)}|{newExpiration:O}";
            Response.Cookies.Append(cookieName, newXxxLoginValue, cookieOptions);
        }
    }
}
