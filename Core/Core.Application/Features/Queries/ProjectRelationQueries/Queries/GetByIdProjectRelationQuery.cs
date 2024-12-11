using Core.Application.Dtos;
using Core.Application.Interfaces.Dtos;
using MediatR;

namespace Core.Application.Features.Queries.ProjectRelationQueries.Queries;

public class GetByIdProjectRelationQuery : IRequest<IResultDataDto<ProjectRelationDto>>
{
    public Guid Id { get; set; }
}