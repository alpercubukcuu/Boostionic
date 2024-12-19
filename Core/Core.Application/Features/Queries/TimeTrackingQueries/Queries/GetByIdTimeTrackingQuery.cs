using Core.Application.Dtos;
using Core.Application.Interfaces.Dtos;
using MediatR;

namespace Core.Application.Features.Queries.TimeTrackingQueries.Queries;

public class GetByIdTimeTrackingQuery : IRequest<IResultDataDto<TimeTrackingDto>>
{
    public Guid Id { get; set; }
}