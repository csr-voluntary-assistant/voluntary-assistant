using Microsoft.AspNetCore.Identity;

namespace Voluntariat.Data
{
    public static class ApplicationDbInitializer
    {
        public static void SeedUsers(UserManager<IdentityUser> userManager)
        {
            var username = "a@a.a";
            var password = "Test.123";

            if (userManager.FindByEmailAsync(username).Result == null)
            {
                IdentityUser user = new IdentityUser
                {
                    UserName = username,
                    Email = username,
                    EmailConfirmed = true
                };

                IdentityResult result = userManager.CreateAsync(user, password).Result;

                userManager.AddToRoleAsync(user, Framework.Identity.IdentityRole.Admin).Wait();
            }
        }
    }
}