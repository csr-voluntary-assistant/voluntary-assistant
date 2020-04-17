using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Volutnariat.Data
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

                if (result.Succeeded)
                {
                    userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, "Admin")).Wait();
                }
            }
        }
    }
}
