using Microsoft.AspNetCore.Identity;
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
        { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<IdentityRole>().HasData(new IdentityRole() { Id = "9ada98e1-4054-4d9a-a591-0dfd20d4cea3", Name = Framework.Identity.IdentityRole.Admin, NormalizedName = Framework.Identity.IdentityRole.Admin, ConcurrencyStamp = "471befb4-f2ec-434f-a195-b3963a502715" });
            builder.Entity<IdentityRole>().HasData(new IdentityRole() { Id = "731570ea-7c31-462f-bc9c-bcaafba892a1", Name = Framework.Identity.IdentityRole.Volunteer, NormalizedName = Framework.Identity.IdentityRole.Volunteer, ConcurrencyStamp = "471befb4-f2ec-434f-a195-b3963a502717" });
            builder.Entity<IdentityRole>().HasData(new IdentityRole() { Id = "c449f071-e14e-469e-affe-1c8b2269cc3f", Name = Framework.Identity.IdentityRole.Doctor, NormalizedName = Framework.Identity.IdentityRole.Doctor, ConcurrencyStamp = "471befb4-f2ec-434f-a195-b3963a502718" });
            builder.Entity<IdentityRole>().HasData(new IdentityRole() { Id = "b9aedc08-76f8-4017-9156-27180e377dca", Name = Framework.Identity.IdentityRole.Beneficiary, NormalizedName = Framework.Identity.IdentityRole.Beneficiary, ConcurrencyStamp = "471befb4-f2ec-434f-a195-b3963a502719" });
            builder.Entity<IdentityRole>().HasData(new IdentityRole() { Id = "bd0d075f-663a-445f-8902-b555a09b1d2d", Name = Framework.Identity.IdentityRole.Guest, NormalizedName = Framework.Identity.IdentityRole.Guest, ConcurrencyStamp = "471befb4-f2ec-434f-a195-b3963a502716" });
        }
    }
}