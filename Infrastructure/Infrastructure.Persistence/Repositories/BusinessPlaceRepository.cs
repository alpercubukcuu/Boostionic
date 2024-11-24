using Core.Application.Attributes;
using Core.Application.Interfaces.Repositories;
using Core.Domain.Entities;
using Infrastructure.Persistence.Context;


namespace Infrastructure.Persistence.Repositories
{
    [AddScopetService(Interface = "IBusinessPlaceRepository")]
    public class BusinessPlaceRepository : GenericRepository<BusinessPlace>, IBusinessPlaceRepository
    {
        public BusinessPlaceRepository(DatabaseContext context) : base(context)
        {
            
        }
    }
}
