using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Voluntariat.Controllers;
using Voluntariat.Framework.Identity;

namespace Voluntariat.Framework
{
    public class GuestActionFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            Identity.Identity identity = context.HttpContext.GetIdentity();

            if (identity != null && identity.Role == CustomIdentityRole.Guest && !(context.Controller is GuestController controller))
            {
                context.Result = new RedirectToActionResult(nameof(GuestController.Index), nameof(GuestController)[0..^10], null);
            }
        }
    }
}