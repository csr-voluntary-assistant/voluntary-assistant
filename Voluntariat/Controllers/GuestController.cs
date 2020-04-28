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

        private readonly UserManager<ApplicationUser> userManager;

        public GuestController(Data.ApplicationDbContext applicationDbContext, UserManager<ApplicationUser> userManager)
        {
            this.applicationDbContext = applicationDbContext;
            this.userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult RegisterAsNGO()
        {
            Identity identity = ControllerContext.GetIdentity();

            NGO ngo = applicationDbContext.NGOs.FirstOrDefault(x => x.NGOStatus == NGOStatus.PendingVerification && x.CreatedByID == identity.ID);

            return View(ngo);
        }


        [HttpPost]
        public async Task<IActionResult> RegisterAsNGO([Bind(nameof(NGO.Name))] NGO ngo)
        {
            if (ModelState.IsValid)
            {
                Identity identity = ControllerContext.GetIdentity();

                ngo.ID = Guid.NewGuid();
                ngo.CreatedByID = identity.ID;
                ngo.NGOStatus = NGOStatus.PendingVerification;

                applicationDbContext.Add(ngo);

                await applicationDbContext.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(ngo);
        }

        [HttpGet]
        public IActionResult RegisterAsBeneficiary()
        {
            Identity identity = ControllerContext.GetIdentity();

            Beneficiary beneficiary = applicationDbContext.Beneficiaries.FirstOrDefault(x => x.Status == BeneficiaryStatus.PendingVerification && x.ID == identity.ID);

            if (beneficiary != null)
            {
                ViewBag.Beneficiary = beneficiary;

                return View(null);
            }

            List<NGO> ngos = applicationDbContext.NGOs.ToList();

            return View(ngos);
        }

        [HttpPost]
        public async Task<IActionResult> RegisterAsBeneficiary(Guid id)
        {
            Identity identity = ControllerContext.GetIdentity();

            Beneficiary beneficiary = new Beneficiary();
            beneficiary.ID = identity.ID;
            beneficiary.NGOID = identity.NGOID;
            beneficiary.Status = BeneficiaryStatus.PendingVerification;

            ApplicationUser identityUser = await userManager.FindByIdAsync(identity.ID.ToString());

            await userManager.RemoveFromRoleAsync(identityUser, Framework.Identity.IdentityRole.Guest);

            await userManager.AddToRoleAsync(identityUser, Framework.Identity.IdentityRole.Beneficiary);

            applicationDbContext.Beneficiaries.Add(beneficiary);

            await applicationDbContext.SaveChangesAsync();

            return RedirectToAction(nameof(RegisterAsBeneficiary));
        }

        public IActionResult RegisterAsVolunteer()
        {
            return View();
        }
    }
}