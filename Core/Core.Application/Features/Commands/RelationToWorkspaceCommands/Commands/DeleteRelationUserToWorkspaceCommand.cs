using Core.Application.Dtos;
using Core.Application.Interfaces.Dtos;
using MediatR;

namespace Core.Application.Features.Commands.RelationToWorkspaceCommands.Commands;

public class DeleteRelationUserToWorkspaceCommand : RelationUserToWorkspaceDto, IRequest<IResultDataDto<bool>>
{
    public Guid Id { get; set; }
}