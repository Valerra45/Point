using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Point.SharedKernel.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Point.Identity.Infrastructure.Data
{
    public class DbInitializer : IDbInitializer
    {
        private readonly IdentityContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;

        public DbInitializer(IdentityContext context,
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _context = context;

            _roleManager = roleManager;

            _userManager = userManager;
        }

        public void InitializeDb()
        {
            _context.Database.EnsureCreated();

            var adminRole = new IdentityRole("Admin");

            var userRole = new IdentityRole("User");

            if (!_context.Roles.Any())
            {
                _roleManager.CreateAsync(adminRole).GetAwaiter().GetResult();

                _roleManager.CreateAsync(userRole).GetAwaiter().GetResult();
            }

            if (!_context.Users.Any(x => x.UserName == "admin"))
            {
                var adminUser = new IdentityUser
                {
                    UserName = "admin",
                    Email = "admin@test.com"
                };

                _userManager.CreateAsync(adminUser, "password").GetAwaiter().GetResult();

                _userManager.AddToRoleAsync(adminUser, adminRole.Name).GetAwaiter().GetResult();
            }
        }
    }
}
