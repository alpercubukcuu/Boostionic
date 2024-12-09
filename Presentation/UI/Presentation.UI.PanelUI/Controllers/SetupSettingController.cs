using AutoMapper;
using Core.Application.Dtos;
using Core.Application.Features.Queries.SetupSettingQueries.Queries;
using Core.Application.Features.Queries.UserQueries.Queries;
using Core.Application.Features.Queries.UserRegisterCodeQueries.Queries;
using Core.Application.Features.Queries.UserResetPasswordQueries.Queries;
using Core.Application.Helper;
using Core.Application.Interfaces.Dtos;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.UI.PanelUI.Controllers
{
    [AllowAnonymous]
    public class SetupSettingController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly string _secretKey;
        private readonly IHttpClientFactory _httpClientFactory;
        public SetupSettingController(IMediator mediator, IMapper mapper, IConfiguration configuration, IHttpClientFactory httpClientFactory)
        {
            _mediator = mediator;
            _mapper = mapper;
            _secretKey = configuration["JwtBearer:ResetPasswordKey"]
                     ?? throw new Exception("ResetPasswordKey is not configured in appsettings.json");
            _httpClientFactory = httpClientFactory;
        }


        public async Task<IActionResult> RegisterCodePage([FromQuery] string userId)
        {
            var transferEncode = TransferHelper.DecodeUserId(userId);

            IResultDataDto<UserDto> result = await this._mediator.Send(new GetByIdUserQuery() { Id = Guid.Parse(transferEncode.DecodedUserId) });

            if (!result.IsSuccess) { return View(transferEncode); }

            return View(transferEncode);
        }

        [HttpPost]
        public async Task<IActionResult> CheckRegisterCode([FromForm] string registerCode, [FromForm] string userId)
        {
            var transferEncode = TransferHelper.DecodeUserId(userId);

            IResultDataDto<UserRegisterCodeDto> res = await this._mediator.Send(new CheckRegisterCodeQuery() { RegisterCode = registerCode, UserId = Guid.Parse(transferEncode.DecodedUserId) });

            if (res.IsSuccess) { return Ok(userId); }

            return BadRequest("Code doesn't match!");
        }

        public async Task<IActionResult> SetupPage([FromQuery] string userId, bool IsDecoded = false)
        {
            TransferEntryInfoDto transferEncode = new();

            if (!IsDecoded) { transferEncode = TransferHelper.DecodeUserId(userId); }
            else { transferEncode.DecodedUserId = userId;  }            

            IResultDataDto<UserDto> result = await this._mediator.Send(new GetByIdUserQuery() { Id = Guid.Parse(transferEncode.DecodedUserId) });
            if (!result.IsSuccess) { return RedirectToAction("Error"); }

            if (result.Data.IsSetup)
            {
                IResultDataDto<SetupSettingDto> setResult = await this._mediator.Send(new GetByUserIdSetupSettingQuery() { UserId = result.Data.Id });
                if (!setResult.IsSuccess) { return RedirectToAction("Error"); }
                return RedirectToAction("Index","Home");
            }

            return View(transferEncode);
        }

        public IActionResult SetupPageDesign()
        {
            return View();
        }

        
    }
}
