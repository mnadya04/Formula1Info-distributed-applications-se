using System;
using System.Threading.Tasks;
using Formula1Info.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Formula1Info.Data
{
    public class SeedData
    {
        public static async Task SeedRolesAndAdmin(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole<string>>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            string[] roleNames = { "Admin" };

            foreach (var role in roleNames)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    var newRole = new IdentityRole<string>
                    {
                        Id = Guid.NewGuid().ToString(),
                        Name = role,
                        NormalizedName = role.ToUpper()
                    };

                    await roleManager.CreateAsync(newRole);
                }
            }

            var adminEmail = "admin@f1.com";
            var adminUser = await userManager.FindByEmailAsync(adminEmail);

            if (adminUser == null)
            {
                adminUser = new ApplicationUser
                {
                    Id = Guid.NewGuid().ToString(), 
                    UserName = adminEmail,
                    Email = adminEmail,
                    FirstName = "F1",
                    LastName = "Admin",
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(adminUser, "Admin123!");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }
        }

        private static async Task SeedDriverAsync(AppDbContext dbContext)
        {
            if (!dbContext.Drivers.Any())
            {
                dbContext.Drivers.Add(new Driver
                {
                    DriverId = "1",
                    FirstName = "No",
                    LastName = "winner yet",
                    UniqueNumber = 1000,
                    DateOfBirth = DateTime.UtcNow.AddYears(1000),
                    Nationality = "no nationality",
                    DriverImageUrl = "nourl",
                    NumberOfChampionships=0,
                    TeamId="no"
                });
               

                await dbContext.SaveChangesAsync();
            }
        }
    }
}
