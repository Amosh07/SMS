using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SMS.Application.Common.User;
using SMS.Application.Interface.Data;
using SMS.Application.Settings;
using SMS.Domain.Common;
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

        public override DbSet<UserLogin> UserLogins { get; set; }

        public override DbSet<UserRoles> UserRoles { get; set; }

        public override DbSet<UserToken> UserTokens { get; set; }


        #endregion

        #region ModelTable

        public DbSet<Attendance> Attendances { get; set; }

        public  DbSet<Class> Classes { get; set; }

        public  DbSet<Exam> Exams { get; set;}

        public  DbSet<Result> Results { get; set; }

        public  DbSet<Student> Students { get; set; }

        public  DbSet<Subject> Subjects { get; set; }

        public  DbSet<Teacher> Teachers { get; set; }

        public IDbConnection Connection => Database.GetDbConnection();
        #endregion

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {

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

            builder.Entity<UserLogin>().ToTable("UserLogins");

            builder.Entity<RoleClaims>().ToTable("RoleClaims");

            builder.Entity<UserToken>().ToTable("UserTokens");
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

            builder.Entity<Student>()
                .HasOne(sd => sd.CreateUser)
                .WithMany()
                .HasForeignKey(sd => sd.CreatedBy)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Student>()
                .HasOne(sd => sd.LastModifiedUser)
                .WithMany()
                .HasForeignKey(sd => sd.LastModifiedBy)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Student>()
                .HasOne(sd => sd.DeletedUser)
                .WithMany()
                .HasForeignKey(sd => sd.DeletedBy)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Teacher>()
                .HasOne(t => t.CreateUser)
                .WithMany()
                .HasForeignKey(t => t.CreatedBy)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Teacher>()
                .HasOne(t => t.LastModifiedUser)
                .WithMany()
                .HasForeignKey(t => t.LastModifiedBy)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Teacher>()
                .HasOne(t => t.DeletedUser)
                .WithMany()
                .HasForeignKey(t => t.DeletedBy)
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
    }
}
