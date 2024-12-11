using Core.Application.Dtos;
using Core.Application.Interfaces.Dtos;
using MediatR;

namespace Core.Application.Features.Queries.ProjectQueries.Queries;

public class GetAllProjectQuery : IRequest<IResultDataDto<List<ProjectDto>>>
{
}