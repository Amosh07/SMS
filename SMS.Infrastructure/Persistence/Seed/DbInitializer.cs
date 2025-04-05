using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SMS.Application.Exceptions;
using SMS.Application.Settings;
using SMS.Domain.Common;
using SMS.Domain.Common.Enum;
using SMS.Domain.Entities;
using SMS.Domain.Entities.Identity;
using SMS.Helper;

namespace SMS.Infrastructure.Persistence.Seed
{
    public class DbInitializer : IDbInitilizer
    { 
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly MenuSettings _menuSettings;

        public DbInitializer(
            ApplicationDbContext context,
            UserManager<User> userManager,
            RoleManager<Role> roleManager,
            IOptions<MenuSettings> menuSettings)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _menuSettings = menuSettings.Value;
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
                Constants.Roles.Student,
                Constants.Roles.User
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

        public async Task InitializeMenuData(CancellationToken cancellationToken = default)
        {
            await OnInitializeMenuAndRoleAssignment(cancellationToken);
        }

        private async Task OnInitializeMenuAndRoleAssignment(CancellationToken cancellationToken = default)
        {
            var menus = _menuSettings.Menus;

            foreach (var menu in menus)
            {
                await ProcessMenu(menu, null, cancellationToken);
            }

            await _context.SaveChangesAsync(cancellationToken);
        }

        private async Task ProcessMenu(MenuConfiguration menuConfiguration, Guid? parentMenuId, CancellationToken cancellationToken)
        {
            var admin = await _userManager.FindByIdAsync(Constants.SuperAdmin.Identifier)
                        ?? throw new NotFoundException("An admin user has not been registered to the system.");

            var existingMenu = await _context.Menus.FirstOrDefaultAsync(
                m => m.Id == new Guid(menuConfiguration.Id),
                cancellationToken);

            if (existingMenu != null)
            {
                existingMenu.Title = menuConfiguration.Title;
                existingMenu.Description = menuConfiguration.Description;
                existingMenu.Sequence = menuConfiguration.Sequence;
                existingMenu.Url = menuConfiguration.Url;
                existingMenu.IsActive = menuConfiguration.IsActive;
                existingMenu.ParentMenuId = parentMenuId;

                _context.Menus.Update(existingMenu);
            }
            else
            {
                var newMenu = new Menu
                {
                    Id = new Guid(menuConfiguration.Id),
                    Title = menuConfiguration.Title,
                    Description = menuConfiguration.Description,
                    Sequence = menuConfiguration.Sequence,
                    Url = menuConfiguration.Url,
                    IsActive = menuConfiguration.IsActive,
                    CreatedBy = admin.Id,
                    CreatedAt = DateTime.UtcNow,
                    ParentMenuId = parentMenuId
                };

                await _context.Menus.AddAsync(newMenu, cancellationToken);

                existingMenu = newMenu;
            }

            if (menuConfiguration.ChildMenus != null)
            {
                foreach (var childMenu in menuConfiguration.ChildMenus)
                {
                    await ProcessMenu(childMenu, existingMenu.Id, cancellationToken);
                }
            }
        }
    }  
}
