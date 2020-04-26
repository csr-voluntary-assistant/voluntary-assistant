using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Voluntariat.Data;
using Voluntariat.Models;

namespace Voluntariat.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public CategoryController(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _applicationDbContext.Categories.ToListAsync());
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
        public async Task<IActionResult> Create([Bind("Name,Description")] Category category)
        {
            if (ModelState.IsValid)
            {
                category.ID = Guid.NewGuid();
                category.AddedBy = AddedBy.PlatformAdmin;
                category.CategoryStatus = CategoryStatus.Approved;
                category.CreatedOn = DateTime.Now;

                _applicationDbContext.Add(category);
                await _applicationDbContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(category);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Category category = await _applicationDbContext.Categories.FirstOrDefaultAsync(c => c.ID == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            Category category = await _applicationDbContext.Categories.FindAsync(id);
            _applicationDbContext.Categories.Remove(category);
            await _applicationDbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
