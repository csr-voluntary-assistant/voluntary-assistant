using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using Voluntariat.Data;
using Voluntariat.Models;

namespace Voluntariat.Controllers
{
    public class AdministrationController : Controller
    {
        private readonly ApplicationDbContext _appContext;
        public AdministrationController(ApplicationDbContext appContext)
        {
            _appContext = appContext;
        }

        public IActionResult Index()
        {
            var model = new AdminMetricsModel();

            model.NoOfActiveOngs = _appContext.Ongs.Count(o => o.OngStatus != OngStatus.PendingVerification);
            model.NoOfPendingOngs = _appContext.Ongs.Count(o => o.OngStatus == OngStatus.PendingVerification);

            model.NoOfActiveVolunteers = _appContext.Volunteers.Count(o => o.OngID != Guid.Empty && o.OngID != null);
            model.NoOfUnaffiliatedVolunteer = _appContext.Volunteers.Count(o => o.OngID == Guid.Empty && o.OngID == null);

            //model.NoOfActiveCategories = _appContext.Categories.Count(o => o.OngStatus != OngStatus.PendingVerification);
            //model.NoOfPendingCategories = _appContext.Categories.Count(o => o.Status == BeneficiaryStatus.PendingVerification);

            //model.NoOfActiveServices = _appContext.Services.Count(o => o.OngID != Guid.Empty && o.OngID != null);
            //model.NoOfPendingServices = _appContext.Services.Count(o => o.OngID == Guid.Empty && o.OngID == null);

            model.NoOfActiveBeneficaries = _appContext.Beneficiaries.Count(o => o.Status == BeneficiaryStatus.Verified);

            // projects == orders?
            model.NoOfActiveProjects = _appContext.Orders.Count(o => o.Status == OrderStatus.InProgress);

            return View(model);
        }
    }
}