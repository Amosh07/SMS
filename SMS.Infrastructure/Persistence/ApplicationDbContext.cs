using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SMS.Application.Common.User;
using SMS.Application.Exceptions;
using SMS.Application.Interface.Data;
using SMS.Application.Settings;
using SMS.Domain.Common;
using SMS.Domain.Common.Base;
using SMS.Domain.Entities;
using SMS.Domain.Entities.Identity;
using SMS.Helper;
using System.Data;
using System.Reflection;

namespace SMS.Infrastructure.Persistence
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, ICurrentUserService? currentUserService = null) :
    IdentityDbContext<User, Role, Guid, UserClaims, UserRoles, UserLogin, RoleClaims, UserToken>(options),
    IApplicationDbContext
    {
        #region IdentityTable
        public override DbSet<User> Users { get; set; }

        public override DbSet<Role> Roles { get; set; }

        public override DbSet<RoleClaims> RoleClaims { get; set; }

        public override DbSet<UserClaims> UserClaims { get; set; }

        public override DbSet<UserRoles> UserRoles { get; set; }

        public DbSet<UserToken> UserToken { get; set; }

        public DbSet<UserLogin> UserLogin { get; set; }

        #endregion

        #region ModelTable

        public DbSet<Attendance> Attendances { get; set; }

        public  DbSet<Class> Classes { get; set; }

        public  DbSet<Exam> Exams { get; set;}

        public  DbSet<Result> Results { get; set; }

        public  DbSet<Subject> Subjects { get; set; }

        public DbSet<Menu> Menus { get; set; }

        public DbSet<RoleRights> RoleRights { get; set; }

        public override int SaveChanges()
        {
            UpdateLogs();

            return base.SaveChanges();
        }

        public IDbConnection Connection => Database.GetDbConnection();
        #endregion

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            UpdateLogs();

            return await base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var basePath = AppContext.BaseDirectory;

            var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";

            if (!Directory.Exists(basePath))
            {
                throw new DirectoryNotFoundException($"The directory '{basePath}' does not exist.");
            }

            var configuration = new ConfigurationBuilder()
              .SetBasePath(basePath)
              .AddJsonFile("appsettings.json")
              .AddJsonFile($"appsettings.{environmentName}.json", optional: true)
            .Build();

            var databaseSettings = new DataBaseSettings();

            configuration.GetSection("DatabaseSettings").Bind(databaseSettings);

            var connectionString = databaseSettings.DbProvider == Constants.DbProviderKey.Npgsql
             ? databaseSettings.NpgSqlConnectionString
             : databaseSettings.SqlServerConnectionString;


            optionsBuilder = optionsBuilder.UseDatabase(databaseSettings.DbProvider, connectionString!);

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);

            #region Identity Entites Naming Convension
            builder.Entity<User>().ToTable("Users");

            builder.Entity<Role>().ToTable("Roles");

            builder.Entity<UserClaims>().ToTable("UserClaims");

            builder.Entity<UserRoles>().ToTable("UserRoles");

            builder.Entity<UserLogin>().ToTable("LoginAttempts");

            builder.Entity<RoleClaims>().ToTable("RoleClaims");

            builder.Entity<UserToken>().ToTable("Tokens");
            #endregion

            #region User Model Configuration
            builder.Entity<Attendance>()
                .HasOne(a => a.CreateUser)
                .WithMany()
                .HasForeignKey(a => a.CreatedBy)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Class>()
                .HasOne(c => c.CreateUser)
                .WithMany()
                .HasForeignKey(c => c.CreatedBy)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Exam>()
                .HasOne(e => e.CreateUser)
                .WithMany()
                .HasForeignKey(e => e.CreatedBy)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Result>()
                .HasOne(r => r.CreateUser)
                .WithMany()
                .HasForeignKey(r => r.CreatedBy)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Subject>()
                .HasOne(s => s.CreateUser)
                .WithMany()
                .HasForeignKey(s => s.CreatedBy)
                .OnDelete(DeleteBehavior.Restrict);

                builder.Entity<Menu>()
               .HasOne(q => q.CreateUser)
               .WithMany()
               .HasForeignKey(q => q.CreatedBy)
               .OnDelete(DeleteBehavior.Restrict);

                builder.Entity<RoleRights>()
               .HasOne(q => q.CreateUser)
               .WithMany()
               .HasForeignKey(q => q.CreatedBy)
               .OnDelete(DeleteBehavior.Restrict);
            #endregion


            /*#region Relationship Mapping Configuration

            // Attendance -> Class
            builder.Entity<Attendance>()
                .HasOne(a => a.Class)
                .WithMany(c => c.Attendances)
                .HasForeignKey(a => a.ClassId)
                .OnDelete(DeleteBehavior.Restrict);

            // Class -> Teacher
            builder.Entity<Class>()
                .HasOne(c => c.Teacher)
                .WithMany(t => t.Classes)
                .HasForeignKey(c => c.TeacherId)
                .OnDelete(DeleteBehavior.Restrict);

            // Exam -> Teacher
            builder.Entity<Exam>()
                .HasOne(e => e.Teacher)
                .WithMany(t => t.Exams)
                .HasForeignKey(e => e.TeacherId)
                .OnDelete(DeleteBehavior.Restrict);

            // Student -> Teacher
            builder.Entity<Student>()
                .HasOne(s => s.Teacher)
                .WithMany(t => t.Students)
                .HasForeignKey(s => s.TeacherId)
                .OnDelete(DeleteBehavior.Restrict);

            // Result -> Exam
            builder.Entity<Result>()
                .HasOne(r => r.Exam)
                .WithMany(e => e.Results)
                .HasForeignKey(r => r.ExamId)
                .OnDelete(DeleteBehavior.Restrict);

            #endregion*/
        }

        private void UpdateLogs()
        {
            var entries = ChangeTracker.Entries()
                .Where(e => e is { Entity: BaseEntity<Guid> or BaseEntity<string>, State: EntityState.Added or EntityState.Modified or EntityState.Deleted });

            if (currentUserService == null)
            {
                throw new NotFoundException("The following user has not been registered to our system.");
            }

            var dateTime = ExtensionMethod.GetUtcDate();

            var userId = currentUserService.GetUserId;

            foreach (var entry in entries)
            {
                UpdateEntityLog(entry.Entity, entry.State, userId, dateTime);
            }
        }

        private static void UpdateEntityLog<TEntity>(TEntity entity, EntityState state, Guid userId, DateTime dateTime)
        where TEntity : class
        {
            switch (state)
            {
                case EntityState.Added:
                    {
                        switch (entity)
                        {
                            case BaseEntity<Guid> guidEntity:
                                guidEntity.CreatedAt = dateTime;
                                if (guidEntity.CreatedBy == Guid.Empty) guidEntity.CreatedBy = userId;
                                break;
                            case BaseEntity<string> stringEntity:
                                stringEntity.CreatedAt = dateTime;
                                if (stringEntity.CreatedBy == Guid.Empty) stringEntity.CreatedBy = userId;
                                break;
                        }
                        break;
                    }

                case EntityState.Modified:
                    {
                        switch (entity)
                        {
                            case BaseEntity<Guid> guidEntity:
                                guidEntity.LastModifiedAt = dateTime;
                                if (guidEntity.CreatedBy == Guid.Empty) guidEntity.LastModifiedBy = userId;
                                break;
                            case BaseEntity<string> stringEntity:
                                stringEntity.LastModifiedAt = dateTime;
                                if (stringEntity.CreatedBy == Guid.Empty) stringEntity.LastModifiedBy = userId;
                                break;
                        }
                        break;
                    }

                case EntityState.Deleted:
                    {
                        switch (entity)
                        {
                            case BaseEntity<Guid> guidEntity:
                                guidEntity.DeletedAt = dateTime;
                                if (guidEntity.DeletedBy == Guid.Empty) guidEntity.DeletedBy = userId;
                                break;
                            case BaseEntity<string> stringEntity:
                                stringEntity.DeletedAt = dateTime;
                                if (stringEntity.DeletedBy == Guid.Empty) stringEntity.DeletedBy = userId;
                                break;
                        }
                        break;
                    }

                case EntityState.Detached:
                case EntityState.Unchanged:
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(state), state, null);
            }
        }
    }
}
