using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Voluntariat.Data;
using Voluntariat.Models;

namespace Voluntariat.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext applicationDbContext;

        private readonly UserManager<IdentityUser> userManager;

        public HomeController(ApplicationDbContext applicationDbContext, UserManager<IdentityUser> userManager)
        {
            this.applicationDbContext = applicationDbContext;
            this.userManager = userManager;
        }

        public IActionResult Index()
        {
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



        [HttpGet]
        public async Task<IActionResult> OngList()
        {
            List<Ong> ongs = await applicationDbContext.Ongs.OrderBy(x => x.OngStatus).ToListAsync();

            foreach (Ong ong in ongs)
            {
                ong.CreatedByName = applicationDbContext.Users.Find(ong.CreatedByID.ToString()).Email;
            }

            return View(ongs);
        }

        [HttpGet]
        public async Task<IActionResult> OngVerify(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Ong ong = await applicationDbContext.Ongs.FindAsync(id);
            if (ong == null)
            {
                return NotFound();
            }
            return View(ong);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OngVerify(Guid id)
        {
            Ong ong = applicationDbContext.Ongs.Find(id);
            ong.OngStatus = OngStatus.Verified;

            IdentityUser user = await userManager.FindByIdAsync(ong.CreatedByID.ToString());

            await userManager.RemoveFromRoleAsync(user, Framework.Identity.IdentityRole.Guest);

            await userManager.AddToRoleAsync(user, Framework.Identity.IdentityRole.Volunteer);

            Volunteer volunteer = new Volunteer();
            volunteer.ID = ong.CreatedByID;
            volunteer.OngID = ong.ID;


            applicationDbContext.Volunteers.Add(volunteer);

            await applicationDbContext.SaveChangesAsync();

            return RedirectToAction(nameof(OngList));
        }

        [HttpGet]
        public async Task<IActionResult> OngDelete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Ong ong = await applicationDbContext.Ongs.FirstOrDefaultAsync(m => m.ID == id);
            if (ong == null)
            {
                return NotFound();
            }

            return View(ong);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OngDelete(Guid id)
        {
            Ong ong = await applicationDbContext.Ongs.FindAsync(id);

            applicationDbContext.Ongs.Remove(ong);

            await applicationDbContext.SaveChangesAsync();

            return RedirectToAction(nameof(OngList));
        }
    }
}