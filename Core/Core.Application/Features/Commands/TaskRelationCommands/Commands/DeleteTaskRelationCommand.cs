using Core.Application.Dtos;
using Core.Application.Interfaces.Dtos;
using MediatR;

namespace Core.Application.Features.Commands.TaskRelationCommands.Commands;

public class DeleteTaskRelationCommand : TaskRelationDto, IRequest<IResultDataDto<bool>>
{
    public Guid Id { get; set; }
}