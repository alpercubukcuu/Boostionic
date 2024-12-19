using Core.Application.Dtos;
using Core.Application.Interfaces.Dtos;
using MediatR;

namespace Core.Application.Features.Commands.TaskRelationCommands.Commands;

public class AddTaskRelationCommand : TaskRelationDto, IRequest<IResultDataDto<TaskRelationDto>>
{
}