using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Voluntariat.Controllers;
using Voluntariat.Framework.Identity;

namespace Voluntariat.Framework
{
    public class BeneficiaryActionFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            Identity.Identity identity = context.HttpContext.GetIdentity();

            if (identity != null && identity.Role == IdentityRole.Beneficiary && !(context.Controller is BeneficiaryController controller))
            {
                context.Result = new RedirectToActionResult(nameof(BeneficiaryController.Index), nameof(BeneficiaryController)[0..^10], null);
            }
        }
    }
}