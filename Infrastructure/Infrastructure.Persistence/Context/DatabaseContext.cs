using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;



namespace Infrastructure.Persistence.Context
{
    public class DatabaseContext : DbContext
    {

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }


        public DbSet<Email> Emails { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<RolePermission> RolesPermissions { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<UserResetPassword> UserResetPasswords { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Industry> Industries { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        }
    }
}
