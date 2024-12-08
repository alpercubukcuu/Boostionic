using Core.Application.Attributes;
using Core.Application.Interfaces.Repositories;
using Core.Domain.Entities;
using Infrastructure.Persistence.Context;


namespace Infrastructure.Persistence.Repositories
{
    [AddScopedService(Interface = "IUserRegisterCodeRepository")]
    public class UserRegisterCodeRepository : GenericRepository<UserRegisterCode>, IUserRegisterCodeRepository
    {
        public UserRegisterCodeRepository(DatabaseContext context) : base(context)
        {
            
        }
    }
}
