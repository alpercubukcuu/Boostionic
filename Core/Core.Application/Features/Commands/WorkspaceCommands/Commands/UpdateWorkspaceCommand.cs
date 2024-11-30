using Core.Application.Dtos;
using Core.Application.Interfaces.Dtos;
using MediatR;


namespace Core.Application.Features.Commands.WorkspaceCommands.Commands
{
    public class UpdateWorkspaceCommand : WorkspaceDto, IRequest<IResultDataDto<WorkspaceDto>>
    {
    }
}