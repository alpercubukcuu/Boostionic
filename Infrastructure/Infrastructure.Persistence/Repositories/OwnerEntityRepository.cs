using Core.Application.Attributes;
using Core.Application.Interfaces.Repositories;
using Core.Domain.Entities;
using Infrastructure.Persistence.Context;


namespace Infrastructure.Persistence.Repositories
{
    [AddScopedService(Interface = "IOwnerEntityRepository")]
    public class OwnerEntityRepository : GenericRepository<OwnersEntity>, IOwnerEntityRepository
    {
        public OwnerEntityRepository(DatabaseContext context) : base(context)
        {
            
        }
    }
}
