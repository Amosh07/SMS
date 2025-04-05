using Microsoft.EntityFrameworkCore;
using SMS.Application.Common.Service;
using SMS.Domain.Entities;
using SMS.Domain.Entities.Identity;
using System.Data;

namespace SMS.Application.Interface.Data
{
    public interface IApplicationDbContext : IScopeService
    {
        #region Identity
        DbSet<User> Users { get; set; }

        DbSet<Role> Roles { get; set; }

        DbSet<UserRoles> UserRoles { get; set; }

        DbSet<UserClaims> UserClaims { get; set; }

        DbSet<RoleClaims> RoleClaims { get; set; }

        DbSet<UserLogin> UserLogin { get; set; }

        DbSet<UserToken> UserToken { get; set; }
        #endregion

        #region Modules
        DbSet<Attendance> Attendances { get; set; }

        DbSet<Class> Classes { get; set; }

        DbSet<Exam> Exams { get; set; } 

        DbSet<Result> Results { get; set; }

        DbSet<Menu> Menus { get; set; }

        DbSet<RoleRights> RoleRights { get; set; }

        DbSet<Subject> Subjects { get; set; }
        #endregion

        #region Function
        int SaveChanges();

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        #endregion

        IDbConnection Connection { get; }

    }
}
