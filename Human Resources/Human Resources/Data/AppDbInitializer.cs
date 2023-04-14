using Human_Resources.Data.Static;
using Human_Resources.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.DependencyInjection;

namespace Human_Resources.Data
{
    public class AppDbInitializer
    {
        
        public static async Task SeedRole(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
                if (!await roleManager.RoleExistsAsync(UserRoles.User))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.User));
                if (!await roleManager.RoleExistsAsync(UserRoles.HRManager))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.HRManager));
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                string adminUserEmail = "admin@gmail.com";

                var adminUser = await userManager.FindByNameAsync("administrator");
                if (adminUser == null)
                {
                    var newAdminUser = new ApplicationUser()
                    {
                        Name = "HRAdmin",
                        UserName = "administrator",
                        pictureURL = "",
                        Email = adminUserEmail,
                        EmailConfirmed = true,
                        SecurityStamp = Guid.NewGuid().ToString(),
                       
                    };
                    var result = await userManager.CreateAsync(newAdminUser,"Afri@1298!");
                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);

                    }
                   
                   
                }
            }
        }
    }
}