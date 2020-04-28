using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using Voluntariat.Models;

namespace Voluntariat.Data.Repositories
{
    public interface IVolunteerRepository
    {
        IQueryable<ApplicationUser> GetVolunteers();
        IQueryable<Volunteer> GetUnaffiliatedVolunteers();
        void RemoveVolunteers(Guid[] ids);
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

        public IQueryable<Volunteer> GetUnaffiliatedVolunteers()
        {
            return _applicationDbContext.Volunteers.Where(v => v.NGOID == Guid.Empty || v.NGOID == null).AsQueryable<Volunteer>();
        }

        public void RemoveVolunteers(Guid[] ids)
        {
            var stringIds = ids.Select(i => i.ToString()).ToArray();
            var users = _applicationDbContext.Users.Where(u => stringIds.Contains(u.Id)).ToArray();
            var volunteers = _applicationDbContext.Volunteers.Where(u => ids.Contains(u.ID)).ToArray();

            for (int i = 0; i < users.Length; i++)
            {
                _applicationDbContext.Remove(users[i]);
                _applicationDbContext.Remove(volunteers[i]);
            }

            _applicationDbContext.SaveChanges();
        }
    }
}
