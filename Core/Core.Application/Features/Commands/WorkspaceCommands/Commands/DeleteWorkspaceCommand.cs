using Core.Application.Dtos;
using Core.Application.Interfaces.Dtos;
using MediatR;


namespace Core.Application.Features.Commands.WorkspaceCommands.Commands
{
    public class DeleteWorkspaceCommand : WorkspaceDto, IRequest<IResultDataDto<bool>>
    {
        public Guid Id { get; set; }
    }
}