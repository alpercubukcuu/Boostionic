using Core.Application.Dtos;
using Core.Application.Interfaces.Dtos;
using MediatR;

namespace Core.Application.Features.Queries.ProjectQueries.Queries;

public class GetByIdProjectQuery : IRequest<IResultDataDto<ProjectDto>>
{
    public Guid Id { get; set; }
}