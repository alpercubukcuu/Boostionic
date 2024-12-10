using Core.Application.Attributes;
using Core.Application.Interfaces.Repositories;
using Core.Domain.Entities;
using Infrastructure.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repositories
{
    [AddScopedService(Interface = "IIndustryRepository")]
    public class IndustryRepository : GenericRepository<Industry>, IIndustryRepository
    {
        public IndustryRepository(DatabaseContext context) : base(context)
        {
            
        }
    }
}
