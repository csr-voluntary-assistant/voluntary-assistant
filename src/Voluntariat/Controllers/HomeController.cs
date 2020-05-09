using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Voluntariat.Framework.Identity;
using Voluntariat.Models;

namespace Voluntariat.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                if (User.IsInRole(CustomIdentityRole.Admin))
                    return new RedirectToActionResult(nameof(AdministrationController.Index), nameof(AdministrationController)[0..^10], null);
                if (User.IsInRole(CustomIdentityRole.Beneficiary))
                    return new RedirectToActionResult(nameof(BeneficiaryController.Index), nameof(BeneficiaryController)[0..^10], null);
                if (User.IsInRole(CustomIdentityRole.Guest))
                    return new RedirectToActionResult(nameof(GuestController.Index), nameof(GuestController)[0..^10], null);
                if (User.IsInRole(CustomIdentityRole.Volunteer) || User.IsInRole(CustomIdentityRole.NGOAdmin))
                    return new RedirectToActionResult(nameof(VolunteersController.Index), nameof(VolunteersController)[0..^10], null);
            }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}