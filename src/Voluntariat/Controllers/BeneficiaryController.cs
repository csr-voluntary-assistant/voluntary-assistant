using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Voluntariat.Framework.Identity;

namespace Voluntariat.Controllers
{
    [Authorize(Roles = CustomIdentityRole.Volunteer+ "," + CustomIdentityRole.Beneficiary)]
    public class BeneficiaryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}