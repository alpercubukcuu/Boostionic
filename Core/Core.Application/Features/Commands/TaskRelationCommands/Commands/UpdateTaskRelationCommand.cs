using Core.Application.Dtos;
using Core.Application.Interfaces.Dtos;
using MediatR;

namespace Core.Application.Features.Commands.TaskRelationCommands.Commands;

public class UpdateTaskRelationCommand : TaskRelationDto, IRequest<IResultDataDto<TaskRelationDto>>
{
}