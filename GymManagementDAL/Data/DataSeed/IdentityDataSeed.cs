using System;
using GymManagementDAL.Entities;
using Microsoft.AspNetCore.Identity;

namespace GymManagementDAL.Data.DataSeed;

public static class IdentityDataSeed
{
    public static bool SeedData(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
    {
        try
        {
            if (!roleManager.Roles.Any())
            {
                var roles = new List<IdentityRole>()
            {
                new IdentityRole() { Name = "SuperAdmin" },
                new IdentityRole() { Name = "Admin" }
            };

                foreach (var role in roles)
                {
                    if (!roleManager.RoleExistsAsync(role.Name).Result)
                    {
                        roleManager.CreateAsync(role).Wait();
                    }
                }
            }

            if (!userManager.Users.Any())
            {
                var superAdminUser = new ApplicationUser()
                {
                    FirstName = "Youssef",
                    LastName = "Desoky",
                    UserName = "YoussefDesoky",
                    Email = "youssefDesoky@admin.com",
                    PhoneNumber = "01012131415"
                };

                userManager.CreateAsync(superAdminUser, "P@ssw0rd").Wait();
                userManager.AddToRoleAsync(superAdminUser, "SuperAdmin").Wait();

                var adminUser = new ApplicationUser()
                {
                    FirstName = "Ahmed",
                    LastName = "Ali",
                    UserName = "AhmedAli",
                    Email = "ahmedAli@admin.com",
                    PhoneNumber = "01012131416"
                };

                userManager.CreateAsync(adminUser, "P@ssw0rd").Wait();
                userManager.AddToRoleAsync(adminUser, "Admin").Wait();
            }

            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Identity Seeding Failed: {ex.Message}");
            return false;
        }
    }
}
