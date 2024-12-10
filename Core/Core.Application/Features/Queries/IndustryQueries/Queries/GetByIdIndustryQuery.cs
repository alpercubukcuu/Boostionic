using Core.Application.Dtos;
using Core.Application.Interfaces.Dtos;
using MediatR;

namespace Core.Application.Features.Queries.IndustryQueries.Queries;

public class GetByIdIndustryQuery : IRequest<IResultDataDto<IndustryDto>>
{
    public Guid Id { get; set; }
}