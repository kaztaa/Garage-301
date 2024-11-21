using Garage301.Data;
using Garage301.Models;
using Microsoft.AspNetCore.Identity;

namespace Gym.Data
{
    public class SeedData
    {
        private static Garage301Context context = default!;
        private static RoleManager<IdentityRole> roleManager = default!;
        private static UserManager<ApplicationUser> userManager = default!;

        public static async Task Init(Garage301Context _context, IServiceProvider services)
        {
            context = _context;
            if (context.Roles.Any()) return;

            roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
            userManager = services.GetRequiredService<UserManager<ApplicationUser>>();

            var roleNames = new[] { "Member", "Admin" };
            var adminEmail = "admin@admin.com";
            var adminFistname = "Admin";
            var adminLastname = "Adminson";

            var userEmail = "member@member.com";
            var userFirstname = "Member";
            var userLastname = "Memberson";
            var pwd = "12qw#¤ER";


            await AddRolesAsync(roleNames);

            var admin = await addAccountAsync(adminEmail,adminFistname, adminLastname, pwd);
            var user = await addAccountAsync(userEmail, userFirstname, userLastname, pwd);

            await addUserToRoleAsync(admin, "Admin");
            await addUserToRoleAsync(user, "Member");


        }

        private static async Task addUserToRoleAsync(ApplicationUser user, string roleName)
        {
            if (!await userManager.IsInRoleAsync(user, roleName))
            {
                var result = await userManager.AddToRoleAsync(user, roleName);
                if (!result.Succeeded) throw new Exception(string.Join("\n", result.Errors));

            }
        }

        private static async Task<ApplicationUser> addAccountAsync(string accountEmail, string firstName, string lastName, string pwd ) 
        {
            var found = await userManager.FindByNameAsync(accountEmail);

            if (found != null) return null;

            var user = new ApplicationUser
            {
                UserName = accountEmail,
                Email = accountEmail,
                FirstName = firstName,
                LastName = lastName,
                EmailConfirmed = true

            
            };

            var result = await userManager.CreateAsync(user, pwd);

            if (!result.Succeeded) throw new Exception(string.Join("\n", result.Errors));

            return user;
        }

        private static async Task AddRolesAsync(string[] roleNames)
        {
            foreach (var roleName in roleNames)
            {
                if (await roleManager.RoleExistsAsync(roleName)) continue;

                var role = new IdentityRole { Name = roleName };
                var result = await roleManager.CreateAsync(role);

                if (!result.Succeeded) throw new Exception(string.Join("\n", result.Errors));
            }
        }
    }
}
