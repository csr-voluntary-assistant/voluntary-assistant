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
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = CustomIdentityRole.Admin)]
    public class AdminNGOsAPIController : ControllerBase
    {
        private readonly ApplicationDbContext applicationDbContext;

        public AdminNGOsAPIController(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        // GET: api/AdminNGOsAPI
        [HttpGet]
        public async Task<ActionResult<IEnumerable<NGOModel>>> GetNGOs()
        {
            List<NGOModel> ngos = new List<NGOModel>();
            List<NGO> result = await applicationDbContext.NGOs.ToListAsync();

            if (result.Any())
            {
                foreach (NGO item in result)
                {
                    NGOModel ngo = new NGOModel
                    {
                        Id = item.ID,
                        Name = item.Name,
                        Status = item.NGOStatus.ToString(),
                        CreatedBy = applicationDbContext.Users.Find(item.CreatedByID.ToString()).Email
                    };

                    ngos.Add(ngo);
                }
            }

            return ngos;
        }

        // GET: api/AdminNGOsAPI/5
        [HttpGet("{id}")]
        public async Task<ActionResult<NGO>> GetByID(Guid id)
        {
            var ngo = await applicationDbContext.NGOs.FindAsync(id);
            if (ngo == null)
            {
                return NotFound();
            }

            return ngo;
        }

        // PUT: api/AdminNGOsAPI/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNGO(Guid id, NGO ngo)
        {
            if (id != ngo.ID)
            {
                return BadRequest();
            }

            applicationDbContext.Entry(ngo).State = EntityState.Modified;

            try
            {
                await applicationDbContext.SaveChangesAsync();
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

        private bool NGOExists(Guid id)
        {
            return applicationDbContext.NGOs.Any(e => e.ID == id);
        }

        public class NGOModel
        {
            public Guid Id { get; set; }

            public string Name { get; set; }

            public string Status { get; set; }

            public string CreatedBy { get; set; }
        }
    }
}
