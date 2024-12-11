using Core.Application.Attributes;
using Core.Application.Interfaces.Repositories;
using Core.Domain.Entities;
using Infrastructure.Persistence.Context;

namespace Infrastructure.Persistence.Repositories;

[AddScopedService(Interface = "IProjectRepository")]
public class ProjectRepository : GenericRepository<Project>, IProjectRepository
{
    public ProjectRepository(DatabaseContext context) : base(context)
    {
    }
}