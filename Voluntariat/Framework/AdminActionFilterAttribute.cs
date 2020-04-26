using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Voluntariat.Controllers;
using Voluntariat.Framework.Identity;

namespace Voluntariat.Framework
{
    public class AdminActionFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            Identity.Identity identity = context.HttpContext.GetIdentity();

            if (identity != null && (identity.Role == IdentityRole.Admin) && (!(context.Controller is AdministrationController) && !(context.Controller is ServiceController) && !(context.Controller is CategoryController) && !(context.Controller is OngsController)))
            {
                context.Result = new RedirectToActionResult(nameof(AdministrationController.Index), nameof(AdministrationController)[0..^10], null);
            }
        }
    }
}