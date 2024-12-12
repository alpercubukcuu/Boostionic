using Core.Application.Dtos;
using Core.Application.Interfaces.Dtos;
using MediatR;

namespace Core.Application.Features.Commands.TimeTrackingCommands.Commands;

public class DeleteTimeTrackingCommand : TimeTrackingDto, IRequest<IResultDataDto<bool>>
{
    public Guid Id { get; set; }
}