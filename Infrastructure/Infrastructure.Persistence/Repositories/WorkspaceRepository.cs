using Core.Application.Attributes;
using Core.Application.Interfaces.Repositories;
using Core.Domain.Entities;
using Infrastructure.Persistence.Context;


namespace Infrastructure.Persistence.Repositories
{
    [AddScopedService(Interface = "IWorkspaceRepository")]
    public class WorkspaceRepository : GenericRepository<Workspace>, IWorkspaceRepository
    {
        public WorkspaceRepository(DatabaseContext context) : base(context)
        {
            
        }
    }
}
