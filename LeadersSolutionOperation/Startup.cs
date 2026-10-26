using LeadersSolutionOperation.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using System.Linq;

[assembly: OwinStartupAttribute(typeof(LeadersSolutionOperation.Startup))]
namespace LeadersSolutionOperation
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateRolesAndAdminUser();
        }

        private void CreateRolesAndAdminUser()
        {
            using (var context = new ApplicationDbContext())
            {
                var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
                var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

                // Ensure Admin role exists
                if (!roleManager.RoleExists("Admin"))
                {
                    roleManager.Create(new IdentityRole("Admin"));
                }

                // Ensure Employee role exists
                if (!roleManager.RoleExists("Employee"))
                {
                    roleManager.Create(new IdentityRole("Employee"));
                }

                // Get Admin role ID safely
                var adminRoleId = roleManager.FindByName("Admin").Id;

                // Check if any user already has Admin role
                var adminUser = userManager.Users
                    .FirstOrDefault(u => u.Roles.Any(r => r.RoleId == adminRoleId));

                // If no Admin exists, create one
                if (adminUser == null)
                {
                    var newAdmin = new ApplicationUser
                    {
                        UserName = "info@leaderssolutions.sa",
                        Email = "info@leaderssolutions.sa",
                        EmployeeName = "Admin",
                        EmailConfirmed = true
                    };

                    var result = userManager.Create(newAdmin, "V9!tQm#42zLp@8Rw");

                    if (result.Succeeded)
                    {
                        userManager.AddToRole(newAdmin.Id, "Admin");
                    }
                }
            }
        }
    }
}
