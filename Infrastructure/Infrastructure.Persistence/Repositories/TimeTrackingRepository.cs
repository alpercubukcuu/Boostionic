using Core.Application.Attributes;
using Core.Application.Interfaces.Repositories;
using Core.Domain.Entities;
using Infrastructure.Persistence.Context;

namespace Infrastructure.Persistence.Repositories;

[AddScopedService(Interface = "ITimeTrackingRepository")]
public class TimeTrackingRepository : GenericRepository<TimeTracking>, ITimeTrackingRepository
{
    public TimeTrackingRepository(DatabaseContext context) : base(context)
    {
    }
}