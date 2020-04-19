using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Voluntariat.Controllers;
using Voluntariat.Framework.Identity;

namespace Voluntariat.Framework
{
    public class DoctorActionFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            Identity.Identity identity = context.HttpContext.GetIdentity();

            if (identity != null && identity.Role == IdentityRole.Doctor && !(context.Controller is DoctorController controller))
            {
                context.Result = new RedirectToActionResult(nameof(DoctorController.Index), nameof(DoctorController)[0..^10], null);
            }
        }
    }
}