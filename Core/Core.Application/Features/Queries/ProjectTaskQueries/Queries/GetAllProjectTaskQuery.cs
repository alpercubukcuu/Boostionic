using Core.Application.Dtos;
using Core.Application.Interfaces.Dtos;
using MediatR;

namespace Core.Application.Features.Queries.ProjectTaskQueries.Queries;

public class GetAllProjectTaskQuery : IRequest<IResultDataDto<List<ProjectTaskDto>>>
{
}