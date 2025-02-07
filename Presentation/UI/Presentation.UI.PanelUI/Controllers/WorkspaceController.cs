using Core.Application.Features.Commands.WorkspaceCommands.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Presentation.UI.PanelUI.Controllers
{
    public class WorkspaceController : Controller
    {
        private readonly IMediator _mediator;
        public WorkspaceController(IMediator mediator)
        {
            _mediator = mediator;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteWorkspace(Guid id)
        {
            var workspace = await this._mediator.Send(new DeleteWorkspaceCommand() { Id = id });
            if (!workspace.IsSuccess)
            {
                return NotFound(new { success = false, message = "Workspace not found." });
            }

            return Ok(workspace.Data);
        }
    }
}
