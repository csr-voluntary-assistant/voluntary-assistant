using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Volutnariat.Controllers;
using Volutnariat.Framework.Identity;

namespace Volutnariat.Framework
{
    public class GuestActionFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            Identity.Identity identity = context.HttpContext.GetIdentity();

            if (identity != null && identity.Role == IdentityRole.Guest && !(context.Controller is GuestController controller))
            {
                context.Result = new RedirectToActionResult(nameof(GuestController.Index), nameof(GuestController)[0..^10], null);
            }
        }
    }
}