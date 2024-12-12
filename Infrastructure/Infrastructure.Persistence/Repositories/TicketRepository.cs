using Core.Application.Attributes;
using Core.Application.Interfaces.Repositories;
using Core.Domain.Entities;
using Infrastructure.Persistence.Context;

namespace Infrastructure.Persistence.Repositories;

[AddScopedService(Interface = "ITicketRepository")]
public class TicketRepository : GenericRepository<Ticket>, ITicketRepository
{
    public TicketRepository(DatabaseContext context) : base(context)
    {
    }
}