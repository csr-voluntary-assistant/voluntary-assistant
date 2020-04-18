using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Voluntariat.Framework.Identity;
using Voluntariat.Models;

namespace Voluntariat.Controllers
{
    public class GuestController : Controller
    {
        private readonly Data.ApplicationDbContext applicationDbContext;

        private readonly UserManager<IdentityUser> userManager;

        public GuestController(Data.ApplicationDbContext applicationDbContext, UserManager<IdentityUser> userManager)
        {
            this.applicationDbContext = applicationDbContext;
            this.userManager = userManager;
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

        [HttpGet]
        public IActionResult RegisterAsDoctor()
        {
            Identity identity = ControllerContext.GetIdentity();

            Doctor doctor = applicationDbContext.Doctors.FirstOrDefault(x => x.Status == DoctorStatus.PendingVerification && x.ID == identity.ID);

            if (doctor != null)
            {
                ViewBag.Doctor = doctor;

                return View(null);
            }

            List<Ong> ongs = applicationDbContext.Ongs.ToList();

            return View(ongs);
        }

        [HttpPost]
        public async Task<IActionResult> RegisterAsDoctor(Guid id)
        {
            Identity identity = ControllerContext.GetIdentity();

            Doctor doctor = new Doctor();
            doctor.ID = identity.ID;
            doctor.OngID = identity.OngID;
            doctor.Status = DoctorStatus.PendingVerification;

            IdentityUser identityUser = await userManager.FindByIdAsync(identity.ID.ToString());

            await userManager.RemoveFromRoleAsync(identityUser, Framework.Identity.IdentityRole.Guest);

            await userManager.AddToRoleAsync(identityUser, Framework.Identity.IdentityRole.Doctor);

            applicationDbContext.Doctors.Add(doctor);

            await applicationDbContext.SaveChangesAsync();

            return RedirectToAction(nameof(RegisterAsDoctor));
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