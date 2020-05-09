using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Voluntariat.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}