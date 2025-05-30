using BookCatalog.DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace BookCatalog.DAL.Seeder.Identity;

public static class IdentitySeeder
{
    public static async Task SeedRoles(IServiceProvider serviceProvider)
    {
        var RoleManager = serviceProvider.GetRequiredService<RoleManager<Roles>>();
        List<Roles> Roles = new()
        {
            new Roles{Name = "Administrator", Description ="Administrator of the system" },
            new Roles{Name ="User", Description = "Basic User" },
        };



        foreach (var role in Roles)
        {
            if (!await RoleManager.RoleExistsAsync(role.Name!))
            {
                await RoleManager.CreateAsync(role);
            }
        }
    }

    public static async Task SeedUsers(IServiceProvider serviceProvider, UserManager<User> userManager)
    {
        List<User> DefaultUsers = new()
    {
        new User()
        {
            FirstName = "Ewoud",
            LastName ="Forster",
            Email = "ewoud.forster@cegeka.com",
            UserName = "ewoud.forster@cegeka.com",
            EmailConfirmed = true
        },
        new User()
        {
            FirstName = "Admin",
            LastName = "Cegeka",
            Email = "Admin@cegeka.com",
            UserName = "Admin@cegeka.com",
            EmailConfirmed = true
        },
        new User()
        {
            FirstName = "Salvatore",
            LastName = "Cossu",
            Email = "Salvatore@cegeka.com",
            UserName = "Salvatore@cegeka.com",
            EmailConfirmed = true
        }
    };

        string[] firstNames = { "Emma", "Noah", "Olivia", "Liam", "Ava", "Lucas", "Mila", "Louis", "Marie", "Julian", "Nora", "Finn", "Elise", "Arthur", "Lina" };
        string[] lastNames = { "Peeters", "Dubois", "Langebergen", "Janssens", "Lemoine", "Vermeulen", "Martin", "Declercq", "Maes", "Willems", "De Smet", "Leroy", "Goossens", "Simon", "Coppens" };

        var rand = new Random();

        for (int i = 0; i < 90; i++)
        {
            var firstName = firstNames[rand.Next(firstNames.Length)];
            var lastName = lastNames[rand.Next(lastNames.Length)];
            var email = $"{firstName.ToLower()}.{lastName.ToLower()}{i + 1}@cegeka.com";
            var username = email;

            DefaultUsers.Add(new User()
            {
                FirstName = firstName, 
                LastName = lastName,
                Email = email,
                UserName = username,
                EmailConfirmed = true
            });
        }

        foreach (var user in DefaultUsers)
        {
            var existingUser = await userManager.FindByEmailAsync(user.Email!);

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
            }
        }
    }
}

