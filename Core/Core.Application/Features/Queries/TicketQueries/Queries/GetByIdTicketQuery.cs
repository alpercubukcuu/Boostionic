using Core.Application.Dtos;
using Core.Application.Interfaces.Dtos;
using MediatR;

namespace Core.Application.Features.Queries.TicketQueries.Queries;

public class GetByIdTicketQuery : IRequest<IResultDataDto<TicketDto>>
{
    public Guid Id { get; set; }
}