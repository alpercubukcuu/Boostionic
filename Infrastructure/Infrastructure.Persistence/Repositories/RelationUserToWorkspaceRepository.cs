using Core.Application.Attributes;
using Core.Application.Interfaces.Repositories;
using Core.Domain.Entities;
using Infrastructure.Persistence.Context;

namespace Infrastructure.Persistence.Repositories;

[AddScopedService(Interface = "IRelationUserToWorkspaceRepository")]
public class RelationUserToWorkspaceRepository : GenericRepository<RelationUserToWorkspace>,
    IRelationUserToWorkspaceRepository
{
    public RelationUserToWorkspaceRepository(DatabaseContext context) : base(context)
    {
    }
}