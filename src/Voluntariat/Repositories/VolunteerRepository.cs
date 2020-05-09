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
        private readonly ApplicationDbContext applicationDbContext;

        public VolunteerRepository(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        public IQueryable<ApplicationUser> GetVolunteers()
        {
            return applicationDbContext.Users.AsQueryable<ApplicationUser>();
        }

        public IQueryable<Volunteer> GetUnaffiliatedVolunteers()
        {
            return applicationDbContext.Volunteers.Where(v => v.NGOID == Guid.Empty || v.NGOID == null).AsQueryable<Volunteer>();
        }

        public void RemoveVolunteers(Guid[] ids)
        {
            var stringIds = ids.Select(i => i.ToString()).ToArray();
            var users = applicationDbContext.Users.Where(u => stringIds.Contains(u.Id)).ToArray();
            var volunteers = applicationDbContext.Volunteers.Where(u => ids.Contains(u.ID)).ToArray();

            for (int i = 0; i < users.Length; i++)
            {
                applicationDbContext.Remove(users[i]);
                applicationDbContext.Remove(volunteers[i]);
            }

            applicationDbContext.SaveChanges();
        }
    }
}
