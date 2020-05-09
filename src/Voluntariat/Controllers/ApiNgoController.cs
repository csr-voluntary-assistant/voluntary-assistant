using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Voluntariat.Data;
using Voluntariat.Models;
using Voluntariat.Models.Public;

namespace Voluntariat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiNgoController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ApiNgoController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<PublicNgo>>> GetPublicNGOs()
        {
            return await _context.NGOs
                .Where(ngo => ngo.NGOStatus == NGOStatus.Verified)
                .Select(ngo => new PublicNgo { ID = ngo.ID, Name = ngo.Name, Description = ngo.Description})
                .ToListAsync();
        }

        // GET: api/ApiNgo/5
        [HttpGet("{id}")]
        public async Task<ActionResult<NGO>> GetNGO(Guid id)
        {
            var nGO = await _context.NGOs.FindAsync(id);

            if (nGO == null)
            {
                return NotFound();
            }

            return nGO;
        }

        // PUT: api/ApiNgo/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNGO(Guid id, NGO nGO)
        {
            if (id != nGO.ID)
            {
                return BadRequest();
            }

            _context.Entry(nGO).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NGOExists(id))
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

        // POST: api/ApiNgo
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<NGO>> PostNGO(NGO nGO)
        {
            _context.NGOs.Add(nGO);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetNGO", new { id = nGO.ID }, nGO);
        }

        // DELETE: api/ApiNgo/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<NGO>> DeleteNGO(Guid id)
        {
            var nGO = await _context.NGOs.FindAsync(id);
            if (nGO == null)
            {
                return NotFound();
            }

            _context.NGOs.Remove(nGO);
            await _context.SaveChangesAsync();

            return nGO;
        }

        private bool NGOExists(Guid id)
        {
            return _context.NGOs.Any(e => e.ID == id);
        }
    }
}
