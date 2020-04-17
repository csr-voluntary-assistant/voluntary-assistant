using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using Voluntariat.Framework.Identity;
using Voluntariat.Models;

namespace Voluntariat.Controllers
{
    public class GuestController : Controller
    {
        private readonly Data.ApplicationDbContext applicationDbContext;

        public GuestController(Data.ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult RegisterAsOng()
        {
            Identity identity = ControllerContext.GetIdentity();

            Ong ong = applicationDbContext.Ongs.FirstOrDefault(x => x.OngStatus == OngStatus.PendingVerification && x.CreatedByID == identity.ID);

            return View(ong);
        }


        [HttpPost]
        public async Task<IActionResult> RegisterAsOng([Bind(nameof(Ong.Name))] Ong ong)
        {
            if (ModelState.IsValid)
            {
                Identity identity = ControllerContext.GetIdentity();

                ong.ID = Guid.NewGuid();
                ong.CreatedByID = identity.ID;
                ong.OngStatus = OngStatus.PendingVerification;

                applicationDbContext.Add(ong);

                await applicationDbContext.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(ong);
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