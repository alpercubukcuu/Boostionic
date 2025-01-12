using Core.Application.Dtos.SettingDtos;
using Core.Application.Features.Queries.RelationUserToWorkspaceQueries.Queries;
using Core.Application.Features.Queries.UserQueries.Queries;
using Core.Application.Features.Queries.WorkspaceQueries.Queries;
using Core.Application.Helper;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.UI.PanelUI.ViewComponents
{
    public class Sidebar : ViewComponent
    {
        private readonly IMediator _mediator;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly string _secretKey;
        

        public Sidebar(IHttpContextAccessor httpContextAccessor, IMediator mediator, IConfiguration configuration)
        {
            _httpContextAccessor = httpContextAccessor;
            _mediator = mediator;
            _secretKey = configuration["JwtBearer:ResetPasswordKey"]
                    ?? throw new Exception("ResetPasswordKey is not configured in appsettings.json");
        }
        public IViewComponentResult Invoke(string viewName)
        {
            SettingDto settingDto = new();


            var userId = UserCookieHelper.GetUserIdFromCookie(_httpContextAccessor, _secretKey);

            var resUser = this._mediator.Send(new GetByIdUserQuery() { Id = Guid.Parse(userId) });

            var resWorkspace = this._mediator.Send(new GetAllByOwnerIdWorkspaceQuery() { OwnerId = resUser.Result.Data.OwnerId });

            settingDto.UserDto = resUser.Result.Data;
            settingDto.WorkspaceDtos = resWorkspace.Result.Data;


            return View(viewName, settingDto);
        }
    }
}
