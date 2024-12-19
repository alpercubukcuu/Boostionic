using Core.Application.Dtos;
using Core.Application.Interfaces.Dtos;
using MediatR;

namespace Core.Application.Features.Queries.RelationUserToWorkspaceQueries.Queries;

public class GetByIdRelationUserToWorkspaceQuery : IRequest<IResultDataDto<RelationUserToWorkspaceDto>>
{
    public Guid Id { get; set; }
}