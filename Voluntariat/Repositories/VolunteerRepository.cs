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
        IQueryable<Volunteer> GetVolunteers();
    }

    public class VolunteerRepository : IVolunteerRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public VolunteerRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public IQueryable<Volunteer> GetVolunteers() 
        {
            return _applicationDbContext.Volunteers.Include(v => v.User).AsQueryable<Volunteer>();
        }
    }
}
