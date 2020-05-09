using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Voluntariat.Controllers;
using Voluntariat.Framework.Identity;

namespace Voluntariat.Framework
{
    public class VolunteerActionFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            Identity.Identity identity = context.HttpContext.GetIdentity();

            if (identity != null && (identity.Role == CustomIdentityRole.Volunteer || identity.Role == CustomIdentityRole.NGOAdmin) && !(context.Controller is VolunteersController controller))
            {
                context.Result = new RedirectToActionResult(nameof(VolunteersController.Index), nameof(VolunteersController)[0..^10], null);
            }
        }
    }
}