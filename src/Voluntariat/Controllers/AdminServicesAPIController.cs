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
    public class AdminServicesAPIController : ControllerBase
    {
        private readonly ApplicationDbContext applicationDbContext;

        public AdminServicesAPIController(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        // GET: api/ApiAdminServices
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Service>>> GetServices()
        {
            return await applicationDbContext.Services.OrderBy(x => x.Name).ToListAsync();
        }

        // GET: api/ApiAdminServices/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Service>> GetService(Guid id)
        {
            var service = await applicationDbContext.Services.FindAsync(id);

            if (service == null)
            {
                return NotFound();
            }

            return service;
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutService(Guid id, Service service)
        {
            if (id != service.ID)
            {
                return BadRequest();
            }

            applicationDbContext.Entry(service).State = EntityState.Modified;

            try
            {
                await applicationDbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ServiceExists(id))
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
        public async Task<ActionResult<Service>> PostService(Service service)
        {
            applicationDbContext.Services.Add(service);
            await applicationDbContext.SaveChangesAsync();

            return CreatedAtAction("GetService", new { id = service.ID }, service);
        }

        // DELETE: api/ApiAdminServices/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Service>> DeleteService(Guid id)
        {
            var service = await applicationDbContext.Services.FindAsync(id);
            if (service == null)
            {
                return NotFound();
            }

            applicationDbContext.Services.Remove(service);
            await applicationDbContext.SaveChangesAsync();

            return service;
        }

        private bool ServiceExists(Guid id)
        {
            return applicationDbContext.Services.Any(e => e.ID == id);
        }
    }
}
