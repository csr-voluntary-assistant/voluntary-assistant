using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Voluntariat.Models;

namespace Voluntariat.Data.Repositories
{
    public interface IVolunteerRepository
    {
        IQueryable<ApplicationUser> GetVolunteers();
    }

    public class VolunteerRepository : IVolunteerRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public VolunteerRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public IQueryable<ApplicationUser> GetVolunteers() 
        {
            return _applicationDbContext.Users.AsQueryable<ApplicationUser>();
        }
    }
}
