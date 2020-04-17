using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using Voluntariat.Framework.Identity;
using Voluntariat.Models;

namespace Voluntariat.Controllers
{
    public class HomeController : Controller
    {
        private readonly Data.ApplicationDbContext applicationDbContext;

        public HomeController(Data.ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            Identity identity = ControllerContext.GetIdentity();

            Ong ong = new Ong();

            ong.ID = Guid.NewGuid();
            ong.Name = "Fundatia Comunitara Oradea";
            ong.CreatedByID = identity.ID;
            ong.OngStatus = OngStatus.Verified;


            Doctor doctor = new Doctor();
            doctor.OngID = ong.ID;

            applicationDbContext.Ongs.Add(ong);
            applicationDbContext.Doctors.Add(doctor);

            applicationDbContext.SaveChanges();

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
