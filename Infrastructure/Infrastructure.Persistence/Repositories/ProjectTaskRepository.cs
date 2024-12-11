using Core.Application.Attributes;
using Core.Application.Interfaces.Repositories;
using Core.Domain.Entities;
using Infrastructure.Persistence.Context;

namespace Infrastructure.Persistence.Repositories;

[AddScopedService(Interface = "IProjectTaskRepository")]
public class ProjectTaskRepository : GenericRepository<ProjectTask>, IProjectTaskRepository
{
    public ProjectTaskRepository(DatabaseContext context) : base(context)
    {
    }
}