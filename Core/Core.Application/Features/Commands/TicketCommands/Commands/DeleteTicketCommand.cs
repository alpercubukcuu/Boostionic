using Core.Application.Dtos;
using Core.Application.Interfaces.Dtos;
using MediatR;

namespace Core.Application.Features.Commands.TicketCommands.Commands;

public class DeleteTicketCommand : TicketDto, IRequest<IResultDataDto<bool>>
{
    public Guid Id { get; set; }
}