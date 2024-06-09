using Microsoft.AspNetCore.Identity;
using PharmacyApp.Domain.Entities;
using System.Threading.Tasks;
using System;

public class SeedData
{
    public static async Task Initialize(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
    {
        string[] roleNames = { "Admin", "User" };
        IdentityResult roleResult;

        foreach (var roleName in roleNames)
        {
            var roleExist = await roleManager.RoleExistsAsync(roleName);
            if (!roleExist)
            {
                roleResult = await roleManager.CreateAsync(new IdentityRole(roleName));
                Console.WriteLine($"Role {roleName} created.");
            }
        }

        var user = new User
        {
            UserName = "admin",
            Email = "admin@admin.com",
            EmailConfirmed = true
        };

        string userPassword = "Admin@123";

        var userExist = await userManager.FindByEmailAsync(user.Email);

        if (userExist == null)
        {
            var createUser = await userManager.CreateAsync(user, userPassword);
            if (createUser.Succeeded)
            {
                await userManager.AddToRoleAsync(user, "Admin");
                Console.WriteLine("Admin user created.");
            }
        }
        else
        {
            Console.WriteLine("Admin user already exists.");
        }
    }
}
