using Microsoft.AspNetCore.Identity;
using Voluntariat.Models;

namespace Voluntariat.Data
{
    public static class ApplicationDbInitializer
    {
        public static void SeedUsers(UserManager<ApplicationUser> userManager)
        {
            var username = "a@a.a";
            var password = "Test.123";

            if (userManager.FindByEmailAsync(username).Result == null)
            {
                ApplicationUser user = new ApplicationUser
                {
                    UserName = username,
                    Email = username,
                    EmailConfirmed = true
                };

                IdentityResult result = userManager.CreateAsync(user, password).Result;

                userManager.AddToRoleAsync(user, Framework.Identity.CustomIdentityRole.Admin).Wait();
            }
        }
    }
}