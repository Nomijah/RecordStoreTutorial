using Microsoft.AspNetCore.Identity;

public static class IdentityDataSeeder
{
    public static async Task SeedAsync(IServiceProvider services)
    {
        using var scope = services.CreateScope();

        var config = scope.ServiceProvider.GetRequiredService<IConfiguration>();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

        // 1) Ensure roles exist
        string[] roles = { "Admin", "User" };
        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
                await roleManager.CreateAsync(new IdentityRole(role));
        }

        // 2) Ensure an admin user exists (values can come from config)
        var adminEmail = config.GetSection("SeedUser:email").Get<string>();
        var adminUser = await userManager.FindByEmailAsync(adminEmail);

        if (adminUser == null)
        {
            adminUser = new IdentityUser
            {
                UserName = "admin",
                Email = adminEmail,
                EmailConfirmed = true
            };

            // Use a password that satisfies your Identity options
            var create = await userManager.CreateAsync(adminUser, config.GetSection("SeedUser:password").Get<string>());
            if (!create.Succeeded)
            {
                var errors = string.Join("; ", create.Errors.Select(e => $"{e.Code}:{e.Description}"));
                throw new Exception($"Failed to create seed admin: {errors}");
            }
        }

        // 3) Ensure admin is in Admin role
        if (!await userManager.IsInRoleAsync(adminUser, "Admin"))
        {
            var addRole = await userManager.AddToRoleAsync(adminUser, "Admin");
            if (!addRole.Succeeded)
            {
                var errors = string.Join("; ", addRole.Errors.Select(e => $"{e.Code}:{e.Description}"));
                throw new Exception($"Failed to add admin to role: {errors}");
            }
        }
    }
}
