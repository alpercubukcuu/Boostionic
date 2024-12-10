using Core.Application.Dtos;
using Core.Application.Interfaces.Dtos;
using MediatR;

namespace Core.Application.Features.Queries.IndustryQueries.Queries;

public class GetAllIndustryQuery : IRequest<IResultDataDto<List<IndustryDto>>>
{
}