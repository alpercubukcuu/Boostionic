using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;



namespace Infrastructure.Persistence.Context
{
    public class DatabaseContext : DbContext
    {

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }


        public DbSet<Email> Emails { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<RolePermission> RolesPermissions { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<UserResetPassword> UserResetPasswords { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Industry> Industries { get; set; }
        public DbSet<Log> Logs { get; set; }
        public DbSet<BusinessPlace> BusinessPlaces { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectCategory> ProjectCategories { get; set; }
        public DbSet<ProjectStage> ProjectStages { get; set; }
        public DbSet<ProjectTask> ProjectTasks { get; set; }
        public DbSet<OwnerEntity> OwnersEntities { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<FileEntity> Files { get; set; }
        public DbSet<ProjectRelation> ProjectRelations { get; set; }
        public DbSet<TaskRelation> TaskRelations { get; set; }
        public DbSet<TimeTracking> TimeTrackings { get; set; }
        public DbSet<Workspace> Workspaces { get; set; }
        public DbSet<SetupSetting> SetupSettings { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {   
            base.OnModelCreating(modelBuilder);
        }
    }
}
