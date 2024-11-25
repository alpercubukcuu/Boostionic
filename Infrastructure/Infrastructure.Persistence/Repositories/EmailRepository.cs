using Core.Application.Attributes;
using Core.Application.Interfaces.Repositories;
using Core.Domain.Entities;
using Infrastructure.Persistence.Context;



namespace Infrastructure.Persistence.Repositories
{
    [AddScopedService(Interface = "IEmailRepository")]
    public class EmailRepository : GenericRepository<Email>, IEmailRepository
    {
        public EmailRepository(DatabaseContext context) : base(context)
        {

        }
    }
}
