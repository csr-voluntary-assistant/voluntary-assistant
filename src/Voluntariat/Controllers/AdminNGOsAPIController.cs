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
            List<NGOModel> ngoModels = new List<NGOModel>();
            List<NGO> ngos = await applicationDbContext.NGOs.ToListAsync();

            if (ngos.Any())
            {
                foreach (NGO ngo in ngos)
                {
                    NGOModel ngoModel = new NGOModel
                    {
                        Id = ngo.ID,
                        Name = ngo.Name,
                        Status = ngo.NGOStatus.ToString(),
                        CreatedBy = applicationDbContext.Users.Find(ngo.CreatedByID.ToString()).Email
                    };

                    ngoModels.Add(ngoModel);
                }
            }

            return ngoModels;
        }

        // GET: api/AdminNGOsAPI/5
        [HttpGet("{id}")]
        public async Task<ActionResult<NGOModel>> GetByID(Guid id)
        {
            NGO ngo = await applicationDbContext.NGOs.FindAsync(id);
            if (ngo == null)
            {
                return NotFound();
            }

            NGOModel ngoModel = new NGOModel
            {
                Id = ngo.ID,
                Name = ngo.Name,
                Status = ngo.NGOStatus.ToString(),
                CreatedBy = applicationDbContext.Users.Find(ngo.CreatedByID.ToString()).Email,
                HeadquartersAddress = ngo.HeadquartersAddress,
                HeadquartersPhoneNumber = ngo.HeadquartersPhoneNumber,
                HeadquartersEmail = ngo.HeadquartersEmail,
                IdentificationNumber = ngo.IdentificationNumber,
                Website = ngo.Website,
                CategoryName = applicationDbContext.Categories.FirstOrDefault(c => c.ID == ngo.CategoryID)?.Name,
                ServiceName = applicationDbContext.Services.FirstOrDefault(c => c.ID == ngo.ServiceID)?.Name
            };

            return ngoModel;
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

            public string HeadquartersAddress { get; set; }

            public string HeadquartersPhoneNumber { get; set; }

            public string HeadquartersEmail { get; set; }

            public string IdentificationNumber { get; set; }

            public string Website { get; set; }

            public string CategoryName { get; set; }

            public string ServiceName { get; set; }
        }
    }
}
