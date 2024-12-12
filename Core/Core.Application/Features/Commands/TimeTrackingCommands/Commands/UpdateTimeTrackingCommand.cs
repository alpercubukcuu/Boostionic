using Core.Application.Dtos;
using Core.Application.Interfaces.Dtos;
using MediatR;

namespace Core.Application.Features.Commands.TimeTrackingCommands.Commands;

public class UpdateTimeTrackingCommand : TimeTrackingDto, IRequest<IResultDataDto<TimeTrackingDto>>
{
}