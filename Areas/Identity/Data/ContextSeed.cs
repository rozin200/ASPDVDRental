using RopeyDVDRental.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;

public static class ContextSeed
{
    public static async Task SeedRolesAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        //Seed Roles
        await roleManager.CreateAsync(new IdentityRole(Roles.User.ToString()));
        await roleManager.CreateAsync(new IdentityRole(Roles.Manager.ToString()));
        await roleManager.CreateAsync(new IdentityRole(Roles.Assistant.ToString()));
    }
    public static async Task SeedSuperAdminAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        //Seed Default User setting
        var defaultUser = new ApplicationUser
        {
            UserName = "Admin",
            FirstName = "Admin",
            LastName = "Admin",
            Email = "Admin@gmail.com",
            EmailConfirmed = true,
            PhoneNumberConfirmed = true,
            LockoutEnabled = false
        };
        if (userManager.Users.All(u => u.Id != defaultUser.Id))
        {
            var user = await userManager.FindByEmailAsync(defaultUser.Email);
            if (user == null)
            {
                await userManager.CreateAsync(defaultUser, "Admin@123");
                await userManager.AddToRoleAsync(defaultUser, Roles.User.ToString());
                await userManager.AddToRoleAsync(defaultUser, Roles.Manager.ToString());
                await userManager.AddToRoleAsync(defaultUser, Roles.Assistant.ToString());
            }

        }
    }
}