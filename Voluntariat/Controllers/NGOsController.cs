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
    public class NGOsController : Controller
    {
        private readonly ApplicationDbContext applicationDbContext;

        private readonly UserManager<ApplicationUser> userManager;

        public NGOsController(ApplicationDbContext applicationDbContext, UserManager<ApplicationUser> userManager)
        {
            this.applicationDbContext = applicationDbContext;
            this.userManager = userManager;
        }

        public async Task<IActionResult> Index(NGOStatus? status)
        {
            IQueryable<NGO> queryNGOs = applicationDbContext.NGOs;

            if (status.HasValue)
                queryNGOs = queryNGOs.Where(x => x.NGOStatus == status);

            List<NGO> ngos = await queryNGOs.OrderBy(x => x.NGOStatus).ToListAsync();

            foreach (NGO ngo in ngos)
            {
                ngo.CreatedByName = applicationDbContext.Users.Find(ngo.CreatedByID.ToString()).Email;
            }

            return View(ngos);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            NGO ngo = await applicationDbContext.NGOs.FindAsync(id);
            if (ngo == null)
            {
                return NotFound();
            }

            ngo.CreatedByName = applicationDbContext.Users.Find(ngo.CreatedByID.ToString()).Email;
            ViewBag.CategoryName = applicationDbContext.Categories.FirstOrDefault(c => c.ID == ngo.CategoryID)?.Name;
            ViewBag.ServiceName = applicationDbContext.Services.FirstOrDefault(s => s.ID == ngo.ServiceID)?.Name;

            return View(ngo);
        }

        // POST: NOGs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id)
        {
            NGO ngo = applicationDbContext.NGOs.Find(id);
            ngo.NGOStatus = NGOStatus.Verified;

            ApplicationUser user = await userManager.FindByIdAsync(ngo.CreatedByID.ToString());

            await userManager.RemoveFromRoleAsync(user, Framework.Identity.IdentityRole.Guest);
            await userManager.AddToRoleAsync(user, Framework.Identity.IdentityRole.NGOAdmin);

            Volunteer volunteer = new Volunteer();
            volunteer.ID = ngo.CreatedByID;
            volunteer.NGOID = ngo.ID;
            volunteer.VolunteerStatus = VolunteerStatus.Verified;

            applicationDbContext.Volunteers.Add(volunteer);

            await applicationDbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET: NGOs/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            NGO ngo = await applicationDbContext.NGOs.FirstOrDefaultAsync(m => m.ID == id);
            if (ngo == null)
            {
                return NotFound();
            }

            return View(ngo);
        }

        // POST: NGOs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var ngo = await applicationDbContext.NGOs.FindAsync(id);
            applicationDbContext.NGOs.Remove(ngo);

            // unaffiliate volunteers after NGO is deleted
            var volunteers = await applicationDbContext.Volunteers.Where(v => v.NGOID.HasValue && v.NGOID.Value == id).ToListAsync();
            foreach (var volunteer in volunteers)
            {
                volunteer.NGOID = null;
                volunteer.UnaffiliationStartTime = DateTime.UtcNow;

                applicationDbContext.Volunteers.Update(volunteer);
            }

            await applicationDbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NGOExists(Guid id)
        {
            return applicationDbContext.NGOs.Any(e => e.ID == id);
        }
    }
}