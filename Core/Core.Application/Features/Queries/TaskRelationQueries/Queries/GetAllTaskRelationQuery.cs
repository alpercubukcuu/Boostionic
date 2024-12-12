using Core.Application.Dtos;
using Core.Application.Interfaces.Dtos;
using MediatR;

namespace Core.Application.Features.Queries.TaskRelationQueries.Queries;

public class GetAllTaskRelationQuery : IRequest<IResultDataDto<List<TaskRelationDto>>>
{
}