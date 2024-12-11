using Core.Application.Attributes;
using Core.Application.Interfaces.Repositories;
using Core.Domain.Entities;
using Infrastructure.Persistence.Context;

namespace Infrastructure.Persistence.Repositories;

[AddScopedService(Interface = "IProjectStageRepository")]
public class ProjectStageRepository : GenericRepository<ProjectStage>, IProjectStageRepository
{
    public ProjectStageRepository(DatabaseContext context) : base(context)
    {
    }
}