using Core.Application.Dtos;
using Core.Application.Interfaces.Dtos;
using MediatR;

namespace Core.Application.Features.Queries.ProjectCategoryQueries.Queries;

public class GetByIdProjectCategoryQuery : IRequest<IResultDataDto<ProjectCategoryDto>>
{
    public Guid Id { get; set; }
}