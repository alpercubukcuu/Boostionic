using Core.Application.Attributes;
using Core.Application.Interfaces.Repositories;
using Core.Domain.Entities;
using Infrastructure.Persistence.Context;


namespace Infrastructure.Persistence.Repositories
{
    [AddScopetService(Interface = "ILogRepository")]
    public class LogRepository : GenericRepository<Log>, ILogRepository
    {
        public LogRepository(DatabaseContext context) : base(context)
        {
            
        }
    }
}
