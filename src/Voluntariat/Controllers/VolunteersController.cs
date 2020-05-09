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
using Microsoft.AspNetCore.Identity;
using Voluntariat.Services;
using Microsoft.AspNetCore.Authorization;

namespace Voluntariat.Controllers
{
    [Authorize(Roles = CustomIdentityRole.NGOAdmin + "," + CustomIdentityRole.Volunteer)]
    public class VolunteersController : Controller
    {
        private readonly ApplicationDbContext applicationDbContext;

        private readonly UserManager<ApplicationUser> userManager;

        private readonly IVolunteerMatchingService _volunteerMatchingService;

        private readonly IVolunteerService _volunteerService;

        public VolunteersController(ApplicationDbContext applicationDbContext, UserManager<ApplicationUser> userManager, IVolunteerMatchingService volunteerMatchingService, IVolunteerService volunteerService)
        {
            this.applicationDbContext = applicationDbContext;
            this.userManager = userManager;
            _volunteerMatchingService = volunteerMatchingService;
            _volunteerService = volunteerService;
        }

        public IActionResult Index()
        {

            //string userId = userManager.GetUserId(User);
            //Volunteer volunteer = applicationDbContext.Volunteers.Include(v => v.User).FirstOrDefault(v => v.UserId == userId);

            //if (volunteer == null)
            //{
            //    volunteer.UserId = userId;

            //    applicationDbContext.SaveChanges();
            //}

            TestVolunteersInRange();
            //TestVolunteersInRangeUsingDB();

            //TestBeneficiaresInRange();

            return View();
        }

        public async Task<IActionResult> Volunteers()
        {
            Identity identity = ControllerContext.GetIdentity();

            List<Volunteer> volunteers = await applicationDbContext.Volunteers.Where(x => x.NGOID == identity.NGOID).ToListAsync();

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
                ApplicationUser identityUser = await userManager.FindByEmailAsync(registerVolunteerModel.Email);

                if (identityUser == null || (await applicationDbContext.Volunteers.FindAsync(Guid.Parse(identityUser.Id))) == null)
                {
                    Identity identity = ControllerContext.GetIdentity();

                    if (identityUser == null)
                    {
                        identityUser = new ApplicationUser { UserName = registerVolunteerModel.Email, Email = registerVolunteerModel.Email, EmailConfirmed = true };

                        await userManager.CreateAsync(identityUser, "Test.123");

                        await userManager.AddToRoleAsync(identityUser, Framework.Identity.CustomIdentityRole.Volunteer);
                    }
                    else
                    {
                        await userManager.RemoveFromRoleAsync(identityUser, Framework.Identity.CustomIdentityRole.Guest);

                        await userManager.AddToRoleAsync(identityUser, Framework.Identity.CustomIdentityRole.Volunteer);
                    }

                    Volunteer volunteer = new Volunteer();
                    volunteer.ID = Guid.Parse(identityUser.Id);
                    volunteer.NGOID = identity.NGOID;

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

        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Volunteer volunteer = await applicationDbContext.Volunteers.FirstOrDefaultAsync(m => m.ID == id);
            volunteer.Name = (await applicationDbContext.Users.FindAsync(volunteer.ID.ToString())).Email;

            return View(volunteer);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            Identity identity = ControllerContext.GetIdentity();

            Volunteer volunteer = await applicationDbContext.Volunteers.FindAsync(id);

            if (identity.ID == id)
            {
                ModelState.AddModelError(nameof(Volunteer.ID), "Can't delete yourself");

                return View(volunteer);
            }

            ApplicationUser identityUser = await applicationDbContext.Users.FindAsync(volunteer.ID.ToString());

            await userManager.RemoveFromRoleAsync(identityUser, Framework.Identity.CustomIdentityRole.Volunteer);

            await userManager.AddToRoleAsync(identityUser, Framework.Identity.CustomIdentityRole.Guest);

            applicationDbContext.Volunteers.Remove(volunteer);

            await applicationDbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
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
        public async Task<IActionResult> Edit(Guid id, [Bind("ID,Latitude,Longitude,NGOID")] Volunteer volunteer)
        {
            if (id != volunteer.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    volunteer.UnaffiliationStartTime = volunteer.NGOID.HasValue ? (DateTime?)null : DateTime.UtcNow;
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



        [HttpGet]
        public async Task<IActionResult> Verify(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Volunteer volunteer = await applicationDbContext.Volunteers.FindAsync(id);
            if (volunteer == null)
            {
                return NotFound();
            }

            volunteer.Name = applicationDbContext.Users.Find(id.ToString()).Email;

            return View(volunteer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Verify(Guid id)
        {
            Volunteer volunteer = await applicationDbContext.Volunteers.FindAsync(id);
            volunteer.VolunteerStatus = VolunteerStatus.Verified;

            ApplicationUser user = await userManager.FindByIdAsync(id.ToString());

            await userManager.RemoveFromRoleAsync(user, Framework.Identity.CustomIdentityRole.Guest);

            await userManager.AddToRoleAsync(user, Framework.Identity.CustomIdentityRole.Volunteer);

            await applicationDbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Volunteers));
        }

        private bool VolunteerExists(Guid id)
        {
            return applicationDbContext.Volunteers.Any(e => e.ID == id);
        }

        private void TestVolunteersInRange()
        {
            ApplicationUser beneficiary = new ApplicationUser();
            beneficiary.Latitude = 47.034654; //47.034654, 21.946378
            beneficiary.Longitude = 21.946378;

            ApplicationUser user1 = new ApplicationUser();
            user1.Latitude = 47.032416;
            user1.Longitude = 21.950669;
            user1.UserName = "v1";
            user1.RangeInKm = 8; //8000;

            ApplicationUser user2 = new ApplicationUser();
            user2.Latitude = 47.035795;  //47.035795, 21.945959
            user2.Longitude = 21.945959;
            user2.UserName = "v2";
            user2.RangeInKm = 15; //15000;

            ApplicationUser user3 = new ApplicationUser();
            user3.Latitude = 47.111343;  //47.111343, 21.892237
            user3.Longitude = 21.892237;
            user3.UserName = "v3";
            user3.RangeInKm = 8; //8000;

            List<ApplicationUser> volunteers = new List<ApplicationUser>();
            volunteers.Add(user1);
            volunteers.Add(user2);
            volunteers.Add(user3);

            IQueryable<ApplicationUser> qV = volunteers.AsQueryable<ApplicationUser>();

            var result = _volunteerMatchingService.GetVolunteersInRange(qV, beneficiary, 8).ToList(); //8000

            var resultVolunteer1 = _volunteerMatchingService.IsInRange(user1, beneficiary);
            var resultVolunteer2 = _volunteerMatchingService.IsInRange(user2, beneficiary);
            var resultVolunteer3 = _volunteerMatchingService.IsInRange(user3, beneficiary);

        }

        private void TestVolunteersInRangeUsingDB()
        {
            ApplicationUser beneficiary = new ApplicationUser();
            beneficiary.Latitude = 47.034654; //47.034654, 21.946378
            beneficiary.Longitude = 21.946378;

            var result = _volunteerService.GetVolunteersInRangeForBeneficiary(beneficiary, 8000);
        }

        private void TestBeneficiaresInRange()
        {
            ApplicationUser beneficiary1 = new ApplicationUser();
            beneficiary1.Latitude = 47.034654; //47.034654, 21.946378
            beneficiary1.Longitude = 21.946378;
            beneficiary1.UserName = "b1";


            ApplicationUser beneficiary2 = new ApplicationUser();
            beneficiary2.Latitude = 47.109624; //47.109624, 21.895375
            beneficiary2.Longitude = 21.895375;
            beneficiary2.UserName = "b2";

            List<ApplicationUser> beneficiares = new List<ApplicationUser>();
            beneficiares.Add(beneficiary1);
            beneficiares.Add(beneficiary2);

            string userId = userManager.GetUserId(User);
            ApplicationUser currentVolunteer = applicationDbContext.Users.FirstOrDefault(u => u.Id == userId);
            var result = _volunteerMatchingService.GetBeneficiariesInRange(beneficiares, currentVolunteer, 8); //8000
        }
    }
}
