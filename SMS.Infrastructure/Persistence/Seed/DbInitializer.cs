using Microsoft.AspNetCore.Identity;
using SMS.Application.Exceptions;
using SMS.Domain.Common;
using SMS.Domain.Common.Enum;
using SMS.Domain.Entities.Identity;
using SMS.Helper;

namespace SMS.Infrastructure.Persistence.Seed
{
    public class DbInitializer : IDbInitilizer
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;

        public DbInitializer(
            ApplicationDbContext context,
            UserManager<User> userManager,
            RoleManager<Role> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task InitializeIdentityData(CancellationToken cancellationToken)
        {
            var user = await InitializeSuperAdmin(cancellationToken);

            await InitilizeRole();

            if (!await _userManager.IsInRoleAsync(user, Constants.Roles.SuperAdmin))
            {
                await _userManager.AddToRoleAsync(user, Constants.Roles.SuperAdmin);
            }

            await _context.SaveChangesAsync(cancellationToken);
        }

        private async Task InitilizeRole()
        {
            var roles = new List<string>()
            {
                Constants.Roles.SuperAdmin,
                Constants.Roles.Teacher,
                Constants.Roles.Student
            };

            var rolesIds = new List<Guid>()
            {
                new Guid("def10000-e86b-52c2-2a9a-08dca196874f"),
                new Guid("def10000-e86b-52c2-42d2-08dca196874f"),
                new Guid("def10000-e86b-52c2-4389-08dca196874f"),
                new Guid("def10000-e86b-52c2-43de-08dca196874f")
            };

            for (var i = 0; i < roles.Count; i++)
            {
                var role = roles[i];

                var roleExists = await _roleManager.RoleExistsAsync(role);

                if (roleExists) continue;

                var newRole = new Role
                {
                    Id = rolesIds[i],
                    Name = role,
                    NormalizedName = role.ToUpper()
                };

                var result = await _roleManager.CreateAsync(newRole);

                if (result.Succeeded) continue;

                var exceptions = result.Errors.Select(x => x.Description);

                throw new BadRequestException("Failed to create super admin role", exceptions.ToArray());
            }
        }

        private async Task<User> InitializeSuperAdmin(CancellationToken cancellationToken = default) 
        {
            var user = await _userManager.FindByIdAsync(Constants.SuperAdmin.Identifier);  

            if (user is null)
            {
                //var teacherId = await dbContext.Teachers.FirstOrDefaultAsync(cancellationToken: cancellationToken);

                var superAdminUser = new User
                {
                    Id = new Guid(Constants.SuperAdmin.Identifier),
                    Name = Constants.SuperAdmin.Name,
                    UserName = Constants.SuperAdmin.Email,
                    Email = Constants.SuperAdmin.Email,
                    Gender = GenderType.male,
                    PhoneNumber = "+977-9813344194",
                    NormalizedUserName = Constants.SuperAdmin.Email.ToUpper(),
                    NormalizedEmail = Constants.SuperAdmin.Email.ToUpper(),
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                    SecurityStamp = Guid.NewGuid().ToString("D"),
                    IsActive = true,
                };
                var result = await _userManager.CreateAsync(superAdminUser, Constants.SuperAdmin.DecryptedPassword);

                if (result.Succeeded) return superAdminUser;

                var exceptions = result.Errors.Select(x => x.Description);
                throw new BadRequestException("failed to create superadmin", exceptions.ToArray());
            }
            user.Name = Constants.SuperAdmin.Name;
            user.UserName = Constants.SuperAdmin.Email;
            user.Email = Constants.SuperAdmin.Email;
            user.NormalizedEmail = Constants.SuperAdmin.Email.ToUpper();
            user.NormalizedUserName = Constants.SuperAdmin.Email.ToUpper();
            user.EmailConfirmed = true;
            user.PhoneNumberConfirmed = true;
            user.PasswordHash = Constants.SuperAdmin.DecryptedPassword.HashPassword();

            await _userManager.UpdateAsync(user);
            return user;
        }
    }
   
}
