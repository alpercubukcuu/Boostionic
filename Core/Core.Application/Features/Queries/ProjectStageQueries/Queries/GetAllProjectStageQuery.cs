using Core.Application.Dtos;
using Core.Application.Interfaces.Dtos;
using MediatR;

namespace Core.Application.Features.Queries.ProjectStageQueries.Queries;

public class GetAllProjectStageQuery : IRequest<IResultDataDto<List<ProjectStageDto>>>
{
}