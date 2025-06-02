using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;



namespace Infrastructure.Persistence.Context
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<DatabaseContext>
    {
        public DatabaseContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DatabaseContext>();
            optionsBuilder.UseSqlServer("your-connettion");

            return new DatabaseContext(optionsBuilder.Options);
        }
    }
}
