using Microsoft.AspNetCore.Mvc;

namespace Volutnariat.Controllers
{
    public class GuestController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }


        public IActionResult RegisterAsOng()
        {
            return View();
        }

        public IActionResult RegisterAsDoctor()
        {
            return View();
        }

        public IActionResult RegisterAsBeneficiary()
        {
            return View();
        }

        public IActionResult RegisterAsVolunteer()
        {
            return View();
        }
    }
}