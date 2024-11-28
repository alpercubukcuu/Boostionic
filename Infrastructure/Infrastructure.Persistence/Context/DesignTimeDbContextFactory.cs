using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;



namespace Infrastructure.Persistence.Context
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<DatabaseContext>
    {
        public DatabaseContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DatabaseContext>();
            optionsBuilder.UseSqlServer("Server=212.81.47.153,1433;Database=Boostionic;User Id=dev_user;Password=Sivax.34;TrustServerCertificate=True");

            return new DatabaseContext(optionsBuilder.Options);
        }
    }
}
