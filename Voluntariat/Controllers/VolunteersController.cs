using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Voluntariat.Data;
using Voluntariat.Framework.Identity;
using Voluntariat.Models;

namespace Voluntariat.Controllers
{
    public class VolunteersController : Controller
    {
        private readonly ApplicationDbContext applicationDbContext;

        private readonly UserManager<IdentityUser> userManager;

        public VolunteersController(ApplicationDbContext applicationDbContext, UserManager<IdentityUser> userManager)
        {
            this.applicationDbContext = applicationDbContext;
            this.userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Volunteers()
        {
            Identity identity = ControllerContext.GetIdentity();

            List<Volunteer> volunteers = await applicationDbContext.Volunteers.Where(x => x.OngID == identity.OngID).ToListAsync();

            foreach (Volunteer volunteer in volunteers)
            {
                volunteer.Name = applicationDbContext.Users.Find(volunteer.ID.ToString()).Email;
            }

            return View(volunteers);
        }

        public IActionResult Create()
        {
            return View();
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind(nameof(RegisterVolunteerModel.Email))] RegisterVolunteerModel registerVolunteerModel)
        {
            if (ModelState.IsValid)
            {
                IdentityUser identityUser = await userManager.FindByEmailAsync(registerVolunteerModel.Email);

                if (identityUser == null)
                {
                    Identity identity = ControllerContext.GetIdentity();

                    identityUser = new IdentityUser { UserName = registerVolunteerModel.Email, Email = registerVolunteerModel.Email, EmailConfirmed = true };

                    await userManager.CreateAsync(identityUser, "Test.123");

                    await userManager.AddToRoleAsync(identityUser, Framework.Identity.IdentityRole.Volunteer);

                    Volunteer volunteer = new Volunteer();
                    volunteer.ID = Guid.Parse(identityUser.Id);
                    volunteer.OngID = identity.OngID;

                    applicationDbContext.Volunteers.Add(volunteer);

                    await applicationDbContext.SaveChangesAsync();

                    return RedirectToAction(nameof(Volunteers));
                }

                ModelState.AddModelError(nameof(RegisterVolunteerModel.Email), "Email duplicat");
            }

            return View(registerVolunteerModel);
        }

        public async Task<IActionResult> Orders()
        {
            return View(await applicationDbContext.Orders.ToListAsync());
        }








        // GET: Volunteers/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var volunteer = await applicationDbContext.Volunteers
                .FirstOrDefaultAsync(m => m.ID == id);
            if (volunteer == null)
            {
                return NotFound();
            }

            return View(volunteer);
        }

        

        // GET: Volunteers/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var volunteer = await applicationDbContext.Volunteers.FindAsync(id);
            if (volunteer == null)
            {
                return NotFound();
            }
            return View(volunteer);
        }

        // POST: Volunteers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ID,Latitude,Longitude,OngID")] Volunteer volunteer)
        {
            if (id != volunteer.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    applicationDbContext.Update(volunteer);
                    await applicationDbContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VolunteerExists(volunteer.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(volunteer);
        }

        // GET: Volunteers/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var volunteer = await applicationDbContext.Volunteers
                .FirstOrDefaultAsync(m => m.ID == id);
            if (volunteer == null)
            {
                return NotFound();
            }

            return View(volunteer);
        }

        // POST: Volunteers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var volunteer = await applicationDbContext.Volunteers.FindAsync(id);
            applicationDbContext.Volunteers.Remove(volunteer);
            await applicationDbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VolunteerExists(Guid id)
        {
            return applicationDbContext.Volunteers.Any(e => e.ID == id);
        }
    }
}
