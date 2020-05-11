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

        [HttpPost]
        public async Task<ActionResult<NGOModel>> VerifyByID(NGOModel ngoModel)
        {
            NGO ngo = await applicationDbContext.NGOs.FindAsync(ngoModel.Id);
            if (ngo != null)
            {
                ngo.NGOStatus = NGOStatus.Verified;
                ngoModel.Status = ngo.NGOStatus.ToString();
                await applicationDbContext.SaveChangesAsync();
            }

            return ngoModel;
        }

        [HttpDelete("{id}")]
        public async Task<bool> DeleteByID(Guid id)
        {
            var ngo = await applicationDbContext.NGOs.FindAsync(id);
            if (ngo != null)
            {
                applicationDbContext.NGOs.Remove(ngo);

                var volunteers = await applicationDbContext.Volunteers.Where(v => v.NGOID.HasValue && v.NGOID.Value == id).ToListAsync();
                foreach (var volunteer in volunteers)
                {
                    volunteer.NGOID = null;
                    volunteer.UnaffiliationStartTime = DateTime.UtcNow;

                    applicationDbContext.Volunteers.Update(volunteer);
                }

                await applicationDbContext.SaveChangesAsync();
            }

            return true;
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
