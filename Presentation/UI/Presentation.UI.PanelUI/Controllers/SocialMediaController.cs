using System.Security.Claims;
using AutoMapper;
using Core.Application.Dtos;
using Core.Application.Dtos.ResultDtos;
using Core.Application.Enums;
using Core.Application.Features.Commands.OwnerEntityCommands.Commands;
using Core.Application.Features.Commands.UserCommands.Commands;
using Core.Application.Features.Queries.UserQueries.Queries;
using Core.Application.Helper;
using Core.Application.Interfaces.Dtos;
using Core.Application.Interfaces.Repositories;
using Core.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Facebook;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;

namespace Presentation.UI.PanelUI.Controllers;

[AllowAnonymous]
public class SocialMediaController : Controller
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly IJwtRepository _jwtRepository;
    private readonly string _secretKey;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public SocialMediaController(IMediator mediator, IMapper mapper, IJwtRepository jwtRepository,
        IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
    {
        _mediator = mediator;
        _mapper = mapper;
        _secretKey = configuration["JwtBearer:ResetPasswordKey"]
                     ?? throw new Exception("ResetPasswordKey is not configured in appsettings.json");
        _jwtRepository = jwtRepository;
        _httpContextAccessor = httpContextAccessor;
    }

    // GET
    public IActionResult Index()
    {
        return View();
    }

    public async Task<IActionResult> GoogleLogin()
    {
        HttpContext.Session.Remove("SocialMediaIssuer");
        HttpContext.Session.Remove("UserLoginInfo");
        
        var redirectUrl = $"{Request.Scheme}://{Request.Host}{Url.Action("SocialMediaLoginResponse", "SocialMedia")}";
        var properties = new AuthenticationProperties { RedirectUri = redirectUrl };
        return Challenge(properties, GoogleDefaults.AuthenticationScheme);
    }

    public IActionResult FacebookLogin()
    {
        HttpContext.Session.Remove("SocialMediaIssuer");
        HttpContext.Session.Remove("UserLoginInfo");
        
        var redirectUrl = Url.Action("SocialMediaLoginResponse", "SocialMedia");
        var properties = new AuthenticationProperties { RedirectUri = redirectUrl };
        return Challenge(properties, FacebookDefaults.AuthenticationScheme);
    }


    public async Task<IActionResult> SocialMediaLoginResponse()
    {
        try
        {
            IResultDataDto<UserDto> resultUser = new ResultDataDto<UserDto>();
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.Now.AddDays(30)
            };
            
            var resultSocialMedia =
                await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            if (resultSocialMedia?.Principal == null)
            {
                return BadRequest("Invalid credentials");
            }

            var claims = resultSocialMedia.Principal.Identities.FirstOrDefault()?.Claims;

            var socialMediaId =
                claims?.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value; // SocialMediaID
            var socialMediaFullName = claims?.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
            var socialMediaName = claims?.FirstOrDefault(c => c.Type == ClaimTypes.GivenName)?.Value;
            var socialMediaSurname = claims?.FirstOrDefault(c => c.Type == ClaimTypes.Surname)?.Value;
            var socialMediaEmail = claims?.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            var socialMediaIssuer = claims?.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Issuer;

            if (socialMediaEmail != null && socialMediaId != null && socialMediaFullName != null)
            {
                resultUser = await this._mediator.Send(new GetByEmailUserQuery() { Email = socialMediaEmail });

                if (resultUser.Data == null)
                {
                    resultUser.Data = new UserDto();
                    IResultDataDto<OwnerEntityDto> resultOwner = await this._mediator.Send(new AddOwnerEntityCommand()
                        { OwnerTitle = socialMediaFullName });
                    if (!resultOwner.IsSuccess) return BadRequest(resultOwner.Error);

                    resultUser.Data.Name = socialMediaName;
                    resultUser.Data.SurName = socialMediaSurname;
                    resultUser.Data.SocialMediaId = socialMediaId;
                    resultUser.Data.Email = socialMediaEmail;
                    resultUser.Data.IsInvited = false;
                    
                    if (socialMediaIssuer == "Google")
                        resultUser.Data.SocialMediType = (int?)SocialMediaLoginTypeEnum.Google;
                    if (socialMediaIssuer == "Facebook")
                        resultUser.Data.SocialMediType = (int?)SocialMediaLoginTypeEnum.Facebook;

                    var userCommand = _mapper.Map<AddUserCommand>(resultUser.Data);
                    userCommand.OwnerId = resultOwner.Data.Id;
                    userCommand.IsInvited = false;

                    IResultDataDto<UserDto> result = await this._mediator.Send(userCommand);
                    if (!result.IsSuccess) return BadRequest(result.Error);
                    
                    HttpContext.Session.SetString("JwtToken", result.Data?.Token);

                    UserRememberCookieHelper.HandleXXXLoginCookie(result.Data.Id.ToString(), cookieOptions, _httpContextAccessor, _secretKey);
                    
                    Response.Cookies.Append("RememberMe", true.ToString(), cookieOptions);

                    return RedirectToAction("Index", "Home");
                }

                if (resultUser.Data.SocialMediaId == socialMediaId && resultUser.Data.Email == socialMediaEmail)
                {
                    var mapUser = _mapper.Map<User>(resultUser.Data);
                    var token = _jwtRepository.GenerateJwtToken(mapUser);

                    resultUser.Data.Token = token;
                    
                    HttpContext.Session.SetString("JwtToken", token);
                    
                    UserRememberCookieHelper.HandleXXXLoginCookie(resultUser.Data.Id.ToString(), cookieOptions, _httpContextAccessor, _secretKey);
                    
                    Response.Cookies.Append("RememberMe", true.ToString(), cookieOptions);

                    return RedirectToAction("Index", "Home");
                }
                if(resultUser.Data.SocialMediType is not null)
                    return BadRequest("Invalid Social Media Login request please try another one.");

                return BadRequest(
                    "That email registered before please try manual login.If you forgot to password please reset your password.");
            }

            return BadRequest("Something went wrong please try again.");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}