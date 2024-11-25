using Core.Application.Attributes;
using Core.Application.Interfaces.Repositories;
using Core.Domain.Entities;
using Infrastructure.Persistence.Context;


namespace Infrastructure.Persistence.Repositories
{
    [AddScopetService(Interface = "IUserResetPasswordRepository")]
    public class UserResetPasswordRepository : GenericRepository<UserResetPassword>, IUserResetPasswordRepository
    {
        public UserResetPasswordRepository(DatabaseContext context) : base(context)
        {
            
        }
    }
}
