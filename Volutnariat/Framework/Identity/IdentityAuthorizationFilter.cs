using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;
using System.Security.Claims;

namespace Volutnariat.Framework.Identity
{
    public class IdentityAuthorizationFilter : IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (context.HttpContext.User.Identity.IsAuthenticated)
            {
                ClaimsIdentity claimsIdentity = context.HttpContext.User.Identity as ClaimsIdentity;

                Claim nameIdentifierClaim = claimsIdentity.Claims.Last(x => x.Type == ClaimTypes.NameIdentifier);
                //Claim roleClaim = claimsIdentity.Claims.Last(x => x.Type == ClaimTypes.Role);

                context.HttpContext.AddIdentity(new Identity() { ID = Guid.Parse(nameIdentifierClaim.Value), /*Role = roleClaim.Value */});
            }
        }
    }
}