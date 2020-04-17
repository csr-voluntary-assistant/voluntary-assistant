using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Voluntariat.Data;
using Voluntariat.Models;

namespace Voluntariat.Controllers
{
    public class OngsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OngsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Ongs
        public async Task<IActionResult> Index()
        {
            return View(await _context.Ongs.ToListAsync());
        }

        // GET: Ongs/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ong = await _context.Ongs
                .FirstOrDefaultAsync(m => m.ID == id);
            if (ong == null)
            {
                return NotFound();
            }

            return View(ong);
        }

        // GET: Ongs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Ongs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind(nameof(Ong.ID), nameof(Ong.Name), nameof(Ong.OngStatus), nameof(Ong.CreatedByID))] Ong ong)
        {
            if (ModelState.IsValid)
            {
                ong.ID = Guid.NewGuid();
                _context.Add(ong);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ong);
        }

        // GET: Ongs/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ong = await _context.Ongs.FindAsync(id);
            if (ong == null)
            {
                return NotFound();
            }
            return View(ong);
        }

        // POST: Ongs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind(nameof(Ong.ID), nameof(Ong.Name), nameof(Ong.OngStatus), nameof(Ong.CreatedByID))] Ong ong)
        {
            if (id != ong.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ong);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OngExists(ong.ID))
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
            return View(ong);
        }

        // GET: Ongs/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ong = await _context.Ongs
                .FirstOrDefaultAsync(m => m.ID == id);
            if (ong == null)
            {
                return NotFound();
            }

            return View(ong);
        }

        // POST: Ongs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var ong = await _context.Ongs.FindAsync(id);
            _context.Ongs.Remove(ong);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OngExists(Guid id)
        {
            return _context.Ongs.Any(e => e.ID == id);
        }
    }
}
