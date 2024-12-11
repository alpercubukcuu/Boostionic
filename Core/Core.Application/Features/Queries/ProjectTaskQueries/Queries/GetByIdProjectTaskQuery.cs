using Core.Application.Dtos;
using Core.Application.Interfaces.Dtos;
using MediatR;

namespace Core.Application.Features.Queries.ProjectTaskQueries.Queries;

public class GetByIdProjectTaskQuery : IRequest<IResultDataDto<ProjectTaskDto>>
{
    public Guid Id { get; set; }
}