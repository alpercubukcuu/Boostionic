using Core.Application.Attributes;
using Core.Application.Interfaces.Repositories;
using Core.Domain.Entities;
using Infrastructure.Persistence.Context;


namespace Infrastructure.Persistence.Repositories
{
    [AddScopedService(Interface = "ISetupSettingRepository")]
    public class SetupSettingRepository : GenericRepository<SetupSetting>, ISetupSettingRepository
    {
        public SetupSettingRepository(DatabaseContext context) : base(context)
        {
            
        }
    }
}
