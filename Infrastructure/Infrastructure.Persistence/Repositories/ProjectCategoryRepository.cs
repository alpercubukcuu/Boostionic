using Core.Application.Attributes;
using Core.Application.Dtos;
using Core.Application.Interfaces.Repositories;
using Core.Domain.Entities;
using Infrastructure.Persistence.Context;


namespace Infrastructure.Persistence.Repositories
{
    [AddScopedService(Interface = "IProjectCategoryRepository")]
    public class ProjectCategoryRepository : GenericRepository<ProjectCategory>, IProjectCategoryRepository
    {
        public ProjectCategoryRepository(DatabaseContext context) : base(context)
        {
            
        }
    }
}
