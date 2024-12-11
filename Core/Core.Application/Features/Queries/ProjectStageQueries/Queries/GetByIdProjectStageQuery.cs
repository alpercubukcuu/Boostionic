using Core.Application.Dtos;
using Core.Application.Interfaces.Dtos;
using MediatR;

namespace Core.Application.Features.Queries.ProjectStageQueries.Queries;

public class GetByIdProjectStageQuery : IRequest<IResultDataDto<ProjectStageDto>>
{
    public Guid Id { get; set; }
}