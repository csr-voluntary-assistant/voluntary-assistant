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
    public class ServiceController : Controller
    {
        private readonly ApplicationDbContext applicationDbContext;

        public ServiceController(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        public async Task<IActionResult> Index(ServiceStatus? status)
        {
            IQueryable<Service> queryServices = applicationDbContext.Services;

            if (status.HasValue)
                queryServices = queryServices.Where(x => x.ServiceStatus == status);

            List<Service> services = await queryServices.OrderBy(x => x.ServiceStatus).ToListAsync();

            return View(services);
        }

        // GET: Orders/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Description")] Service service)
        {
            if (ModelState.IsValid)
            {
                service.ID = Guid.NewGuid();
                service.AddedBy = AddedBy.PlatformAdmin;
                service.ServiceStatus = ServiceStatus.Approved;
                service.CreatedOn = DateTime.Now;

                applicationDbContext.Add(service);
                await applicationDbContext.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(service);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Service service = await applicationDbContext.Services.FirstOrDefaultAsync(c => c.ID == id);
            if (service == null)
            {
                return NotFound();
            }

            return View(service);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            Service service = await applicationDbContext.Services.FindAsync(id);
            applicationDbContext.Services.Remove(service);
            await applicationDbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
