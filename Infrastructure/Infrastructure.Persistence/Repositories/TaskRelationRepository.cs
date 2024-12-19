using Core.Application.Attributes;
using Core.Application.Interfaces.Repositories;
using Core.Domain.Entities;
using Infrastructure.Persistence.Context;

namespace Infrastructure.Persistence.Repositories
{
    [AddScopedService(Interface = "ITaskRelationRepository")]
    public class TaskRelationRepository : GenericRepository<TaskRelation>, ITaskRelationRepository
    {
        public TaskRelationRepository(DatabaseContext context) : base(context)
        {
        }
    }
}