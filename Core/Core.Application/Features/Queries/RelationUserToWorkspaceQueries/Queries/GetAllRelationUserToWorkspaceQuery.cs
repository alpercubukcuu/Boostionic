using Core.Application.Dtos;
using Core.Application.Interfaces.Dtos;
using MediatR;

namespace Core.Application.Features.Queries.RelationUserToWorkspaceQueries.Queries;

public class GetAllRelationUserToWorkspaceQuery : IRequest<IResultDataDto<List<RelationUserToWorkspaceDto>>>
{
}