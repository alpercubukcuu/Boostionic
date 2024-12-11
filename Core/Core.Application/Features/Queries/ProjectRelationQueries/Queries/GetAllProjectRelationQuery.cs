using Core.Application.Dtos;
using Core.Application.Interfaces.Dtos;
using MediatR;

namespace Core.Application.Features.Queries.ProjectRelationQueries.Queries;

public class GetAllProjectRelationQuery : IRequest<IResultDataDto<List<ProjectRelationDto>>>
{
}