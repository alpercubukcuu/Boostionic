using Core.Application.Attributes;
using Core.Application.Interfaces.Repositories;
using Core.Domain.Entities;
using Infrastructure.Persistence.Context;

namespace Infrastructure.Persistence.Repositories
{
    [AddScopedService(Interface = "IClientRepository")]
    public class ClientRepository : GenericRepository<Client>, IClientRepository
    {
        public ClientRepository(DatabaseContext context) : base(context)
        {
            
        }
    }
}
