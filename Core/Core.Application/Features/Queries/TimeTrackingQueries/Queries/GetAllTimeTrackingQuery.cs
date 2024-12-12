using Core.Application.Dtos;
using Core.Application.Interfaces.Dtos;
using MediatR;

namespace Core.Application.Features.Queries.TimeTrackingQueries.Queries;

public class GetAllTimeTrackingQuery : IRequest<IResultDataDto<List<TimeTrackingDto>>>
{
}