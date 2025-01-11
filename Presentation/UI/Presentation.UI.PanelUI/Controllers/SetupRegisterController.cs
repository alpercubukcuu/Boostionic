using AutoMapper;
using Core.Application.Dtos;
using Core.Application.Features.Commands.SetupSettingCommands.Commands;
using Core.Application.Features.Commands.UserCommands.Commands;
using Core.Application.Features.Commands.WorkspaceCommands.Commands;
using Core.Application.Features.Queries.UserQueries.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Presentation.UI.PanelUI.Controllers
{
    public class SetupRegisterController : Controller
    {

        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        public SetupRegisterController(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }
        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> SetupSettingSave([FromBody] SetupWorkplaceDtos setupWorkplaceDtos)
        {
            try
            {
                var settingData = JsonSerializer.Serialize(new
                {
                    setupWorkplaceDtos.Usage,
                    setupWorkplaceDtos.Features,
                    setupWorkplaceDtos.WorkspaceName
                });

                var setupSetting = new SetupSettingDto
                {
                    SettingData = settingData,
                    UserId = Guid.Parse(setupWorkplaceDtos.UserId)
                };

                var mapSetupSetting = _mapper.Map<AddSetupSettingCommand>(setupSetting);
                var resSetupSetting = await this._mediator.Send(mapSetupSetting);
                if (!resSetupSetting.IsSuccess) { return Json(new { success = false, message = $"An error occurred: {resSetupSetting.Error}" }); }

                WorkspaceDto workspaceDto = new();
                workspaceDto.IsGrowth = true;

                if (setupWorkplaceDtos.Features.Contains("Media")) { workspaceDto.IsMedia = true; }
                if (setupWorkplaceDtos.Features.Contains("Growth")) { workspaceDto.IsGrowth = true; }

                var user = await this._mediator.Send(new GetByIdUserQuery() { Id = Guid.Parse(setupWorkplaceDtos.UserId) });
                if (!user.IsSuccess) { return Json(new { success = false, message = $"An error occurred: {user.Error}" }); }

                workspaceDto.OwnerId = user.Data.OwnerId;
                workspaceDto.Name = setupWorkplaceDtos.WorkspaceName;

                var mapWorkspace = _mapper.Map<AddWorkspaceCommand>(workspaceDto);

                var resWorkspace = await this._mediator.Send(mapWorkspace);
                if (!resWorkspace.IsSuccess) { return Json(new { success = false, message = $"An error occurred: {resWorkspace.Error}" }); }


                var mapUpdateUserCommand = _mapper.Map<UpdateUserCommand>(user.Data);
                mapUpdateUserCommand.IsSetup = true;
                var resUpdateUser = await this._mediator.Send(mapUpdateUserCommand);
                if (!resUpdateUser.IsSuccess) { return Json(new { success = false, message = $"An error occurred: {resUpdateUser.Error}" }); }


                return Json(new { success = true, message = "Workspace saved successfully." });
            }
            catch (Exception ex)
            {
                
                return Json(new { success = false, message = $"An error occurred: {ex.Message}" });
            }
        }

    }
}
