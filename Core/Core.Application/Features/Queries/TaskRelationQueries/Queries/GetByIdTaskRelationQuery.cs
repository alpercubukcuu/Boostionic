using Core.Application.Dtos;
using Core.Application.Interfaces.Dtos;
using MediatR;

namespace Core.Application.Features.Queries.TaskRelationQueries.Queries;

public class GetByIdTaskRelationQuery : IRequest<IResultDataDto<TaskRelationDto>>
{
    public Guid Id { get; set; }
}