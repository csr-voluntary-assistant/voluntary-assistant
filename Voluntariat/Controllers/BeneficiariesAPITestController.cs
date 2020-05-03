using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Voluntariat.Data;
using Voluntariat.Models;

namespace Voluntariat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BeneficiariesAPITestController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public BeneficiariesAPITestController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/BeneficiariesAPITest
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Beneficiary>>> GetBeneficiaries()
        {
            return await _context.Beneficiaries.ToListAsync();
        }

        // GET: api/BeneficiariesAPITest/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Beneficiary>> GetBeneficiary(Guid id)
        {
            var beneficiary = await _context.Beneficiaries.FindAsync(id);

            if (beneficiary == null)
            {
                return NotFound();
            }

            return beneficiary;
        }

        // PUT: api/BeneficiariesAPITest/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBeneficiary(Guid id, Beneficiary beneficiary)
        {
            if (id != beneficiary.ID)
            {
                return BadRequest();
            }

            _context.Entry(beneficiary).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BeneficiaryExists(id))
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

        // POST: api/BeneficiariesAPITest
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Beneficiary>> PostBeneficiary(Beneficiary beneficiary)
        {
            _context.Beneficiaries.Add(beneficiary);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBeneficiary", new { id = beneficiary.ID }, beneficiary);
        }

        // DELETE: api/BeneficiariesAPITest/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Beneficiary>> DeleteBeneficiary(Guid id)
        {
            var beneficiary = await _context.Beneficiaries.FindAsync(id);
            if (beneficiary == null)
            {
                return NotFound();
            }

            _context.Beneficiaries.Remove(beneficiary);
            await _context.SaveChangesAsync();

            return beneficiary;
        }

        private bool BeneficiaryExists(Guid id)
        {
            return _context.Beneficiaries.Any(e => e.ID == id);
        }
    }
}
