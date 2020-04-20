using Microsoft.AspNetCore.Mvc;

namespace Voluntariat.Controllers
{
    public class BeneficiaryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}