using Core.Application.Attributes;
using Core.Application.Interfaces.Repositories;
using Core.Domain.Entities;
using Infrastructure.Persistence.Context;

namespace Infrastructure.Persistence.Repositories;

[AddScopedService(Interface = "IProjectRelationRepository")]
public class ProjectRelationRepository : GenericRepository<ProjectRelation>, IProjectRelationRepository
{
    public ProjectRelationRepository(DatabaseContext context) : base(context)
    {
    }
}