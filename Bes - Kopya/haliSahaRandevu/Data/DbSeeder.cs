using haliSahaRandevu.Models;
using Microsoft.AspNetCore.Identity;

namespace haliSahaRandevu.Data
{
    public static class DbSeeder
    {
        public static async Task SeedRolesAndAdminAsync(IServiceProvider service)
        {
            // Seed Roles
            var userManager = service.GetService<UserManager<AppUser>>();
            var roleManager = service.GetService<RoleManager<IdentityRole>>();

            if (roleManager != null && userManager != null)
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
                await roleManager.CreateAsync(new IdentityRole("SahaSahibi"));
                await roleManager.CreateAsync(new IdentityRole("User"));

                // Seed Default Admin
                var admin = new AppUser
                {
                    UserName = "admin@halisaha.com",
                    Email = "admin@halisaha.com",
                    FullName = "System Admin",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true
                };

                var userInDb = await userManager.FindByEmailAsync(admin.Email);
                if (userInDb == null)
                {
                    await userManager.CreateAsync(admin, "Sau123!");
                    await userManager.AddToRoleAsync(admin, "Admin");
                }
            }
        }
    }
}
