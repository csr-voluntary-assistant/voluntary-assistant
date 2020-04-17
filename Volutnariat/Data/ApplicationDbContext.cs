using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Voluntariat.Models;

namespace Voluntariat.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Ong> Ongs { get; set; }

        public DbSet<Volunteer> Volunteers { get; set; }

        public DbSet<Doctor> Doctors { get; set; }



        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }
    }
}
