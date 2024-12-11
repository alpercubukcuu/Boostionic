using Core.Application.Dtos;
using Core.Application.Interfaces.Dtos;
using MediatR;

namespace Core.Application.Features.Commands.RelationToWorkspaceCommands.Commands;

public class AddRelationUserToWorkspaceCommand : RelationUserToWorkspaceDto, IRequest<IResultDataDto<RelationUserToWorkspaceDto>>
{
}