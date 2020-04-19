using Microsoft.AspNetCore.Mvc;

namespace Voluntariat.Controllers
{
    public class DoctorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}