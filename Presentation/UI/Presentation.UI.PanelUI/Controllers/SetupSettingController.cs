using AutoMapper;
using Core.Application.Dtos;
using Core.Application.Features.Queries.SetupSettingQueries.Queries;
using Core.Application.Features.Queries.UserQueries.Queries;
using Core.Application.Helper;
using Core.Application.Interfaces.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.UI.PanelUI.Controllers
{
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


        public async Task<IActionResult> CheckCode()
        {
            return Ok();
        }

        public async Task<IActionResult> SetupPage()
        {
            string userIdValue = HttpContext.Request.Cookies["XXXLogin"];
            if (string.IsNullOrEmpty(userIdValue)) { return RedirectToAction("Error"); }

            var userId = Cipher.DecryptUserId(userIdValue, _secretKey);

            IResultDataDto<UserDto> result = await this._mediator.Send(new GetByIdUserQuery() { Id = Guid.Parse(userId) });
            if (!result.IsSuccess) { return RedirectToAction("Error"); }

            if (result.Data.IsSetup)
            {
                IResultDataDto<SetupSettingDto> setResult = await this._mediator.Send(new GetByUserIdSetupSettingQuery() { UserId = result.Data.Id });
                if (!setResult.IsSuccess) { return RedirectToAction("Error"); }
            }

            return View();
        }

        
    }
}
