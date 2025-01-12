using Core.Application.Dtos;
using Core.Application.Interfaces.Dtos;
using MediatR;

namespace Core.Application.Features.Queries.WorkspaceQueries.Queries;

public class GetAllByOwnerIdWorkspaceQuery : IRequest<IResultDataDto<List<WorkspaceDto>>>
{
    public Guid? OwnerId { get; set; }
}