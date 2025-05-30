using BookCatalog.DAL.Services.Logging;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookCatalog.DAL.Storage.DataBase.Seeder.Identity
{
    public static class IdentitySeeder
    {
        public static async Task SeedRoles(IServiceProvider serviceProvider)
        {
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            List<IdentityRole> Roles = new()
            {
                new IdentityRole("Administrator"),
                new IdentityRole("User"),
            };



            foreach (var role in Roles)
            {
                if (!await RoleManager.RoleExistsAsync(role.Name))
                {
                    await RoleManager.CreateAsync(role);
                }
            }
        }

        public static async Task SeedUsers(IServiceProvider serviceProvider, UserManager<IdentityUser> userManager)
        {
            List<IdentityUser> DefaultUsers = new()
            {
                new IdentityUser()
                {
                    Email = "ewoud.forster@cegeka.com",
                    UserName = "ewoud.forster@cegeka.com",
                    EmailConfirmed = true
                },
                new IdentityUser()

                {
                    Email = "Admin@cegeka.com",
                    UserName = "Admin@cegeka.com",
                    EmailConfirmed = true
                },
               new IdentityUser()

                {
                    Email = "Salvatore@cegeka.com",
                    UserName = "Salvatore@cegeka.com",
                    EmailConfirmed = true
                }
            };

            foreach (var user in DefaultUsers)
            {
                var existingUser = await userManager.FindByEmailAsync(user.Email);
       
                    if (existingUser == null)
                    {
                        var result = await userManager.CreateAsync(user, "Password123!");
                        if (result.Succeeded)
                        {
                            if (user.UserName == "Admin@cegeka.com")
                            {
                                await userManager.AddToRoleAsync(user, "Administrator");
                            }
                            else
                            {
                                await userManager.AddToRoleAsync(user, "User");
                            }
                        }
                        else
                        {
                            foreach (var error in result.Errors)
                            {
                                // Log the error or handle it as needed
                                Console.WriteLine($"Error creating user {user.UserName}: {error.Description}");
                            }
                        }
                    }


            }
        }
    }
}
