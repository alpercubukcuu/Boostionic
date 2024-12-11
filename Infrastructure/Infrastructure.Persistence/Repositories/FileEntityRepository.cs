using Core.Application.Attributes;
using Core.Application.Interfaces.Repositories;
using Core.Domain.Entities;
using Infrastructure.Persistence.Context;


namespace Infrastructure.Persistence.Repositories
{
    [AddScopedService(Interface = "IFileEntityRepository")]
    public class FileEntityRepository : GenericRepository<FileEntity>, IFileEntityRepository
    {
        public FileEntityRepository(DatabaseContext context) : base(context)
        {
            
        }
    }
}
