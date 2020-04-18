using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Voluntariat.Data;
using Voluntariat.Models;

namespace Voluntariat.Controllers
{
    public class OngsController : Controller
    {
        private readonly ApplicationDbContext applicationDbContext;

        private readonly UserManager<IdentityUser> userManager;

        public OngsController(ApplicationDbContext applicationDbContext, UserManager<IdentityUser> userManager)
        {
            this.applicationDbContext = applicationDbContext;
            this.userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            List<Ong> ongs = await applicationDbContext.Ongs.ToListAsync();

            foreach (Ong ong in ongs)
            {
                ong.CreatedByName = applicationDbContext.Users.Find(ong.CreatedByID.ToString()).Email;
            }

            return View(ongs);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid? id)
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

        // POST: Ongs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id)
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

            return RedirectToAction(nameof(Index));
        }

        // GET: Ongs/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
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

        // POST: Ongs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var ong = await applicationDbContext.Ongs.FindAsync(id);
            applicationDbContext.Ongs.Remove(ong);
            await applicationDbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OngExists(Guid id)
        {
            return applicationDbContext.Ongs.Any(e => e.ID == id);
        }
    }
}