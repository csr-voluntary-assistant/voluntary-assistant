using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Voluntariat.Data;
using Voluntariat.Framework.Identity;
using Voluntariat.Models;

namespace Voluntariat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = CustomIdentityRole.Admin)]
    public class AdminCategoriesAPIController : ControllerBase
    {
        private readonly ApplicationDbContext applicationDbContext;

        public AdminCategoriesAPIController(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
        {
            return await applicationDbContext.Categories.OrderBy(x => x.Name).ToListAsync();
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetCategory(Guid id)
        {
            var category = await applicationDbContext.Categories.FindAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            return category;
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategory(Guid id, Category category)
        {
            if (id != category.ID)
            {
                return BadRequest();
            }

            applicationDbContext.Entry(category).State = EntityState.Modified;

            try
            {
                await applicationDbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }


        [HttpPost]
        public async Task<ActionResult<Category>> PostCategory(Category category)
        {
            applicationDbContext.Categories.Add(category);
            await applicationDbContext.SaveChangesAsync();

            return CreatedAtAction("GetCategory", new { id = category.ID }, category);
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<Category>> DeleteCategory(Guid id)
        {
            var category = await applicationDbContext.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            applicationDbContext.Categories.Remove(category);
            await applicationDbContext.SaveChangesAsync();

            return category;
        }

        private bool CategoryExists(Guid id)
        {
            return applicationDbContext.Categories.Any(e => e.ID == id);
        }
    }
}
