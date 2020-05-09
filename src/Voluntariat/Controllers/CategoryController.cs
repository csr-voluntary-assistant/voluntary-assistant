using Microsoft.AspNetCore.Authorization;
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
    [Authorize(Roles = CustomIdentityRole.Admin)]
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext applicationDbContext;

        public CategoryController(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        public async Task<IActionResult> Index(CategoryStatus? status)
        {
            IQueryable<Category> queryCategories = applicationDbContext.Categories;

            if (status.HasValue)
                queryCategories = queryCategories.Where(x => x.CategoryStatus == status);

            List<Category> services = await queryCategories.OrderBy(x => x.CategoryStatus).ToListAsync();

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
        public async Task<IActionResult> Create([Bind("Name,Description")] Category category)
        {
            if (ModelState.IsValid)
            {
                category.ID = Guid.NewGuid();
                category.AddedBy = AddedBy.PlatformAdmin;
                category.CategoryStatus = CategoryStatus.Approved;
                category.CreatedOn = DateTime.Now;

                applicationDbContext.Add(category);
                await applicationDbContext.SaveChangesAsync();
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

            Category category = await applicationDbContext.Categories.FirstOrDefaultAsync(c => c.ID == id);
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
            Category category = await applicationDbContext.Categories.FindAsync(id);
            applicationDbContext.Categories.Remove(category);
            await applicationDbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
