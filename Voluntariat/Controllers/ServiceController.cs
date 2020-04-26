using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Voluntariat.Data;
using Voluntariat.Models;

namespace Voluntariat.Controllers
{
    public class ServiceController : Controller
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public ServiceController(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _applicationDbContext.Services.ToListAsync());
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

                _applicationDbContext.Add(service);
                await _applicationDbContext.SaveChangesAsync();
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

            Service service = await _applicationDbContext.Services.FirstOrDefaultAsync(c => c.ID == id);
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
            Service service = await _applicationDbContext.Services.FindAsync(id);
            _applicationDbContext.Services.Remove(service);
            await _applicationDbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
