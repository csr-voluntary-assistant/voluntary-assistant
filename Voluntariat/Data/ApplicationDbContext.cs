using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using Voluntariat.Models;

namespace Voluntariat.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Ong> Ongs { get; set; }

        public DbSet<Volunteer> Volunteers { get; set; }

        public DbSet<Beneficiary> Beneficiaries { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Service> Services { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<IdentityRole>().HasData(new IdentityRole() { Id = "9ada98e1-4054-4d9a-a591-0dfd20d4cea3", Name = Framework.Identity.IdentityRole.Admin, NormalizedName = Framework.Identity.IdentityRole.Admin, ConcurrencyStamp = "471befb4-f2ec-434f-a195-b3963a502715" });
            builder.Entity<IdentityRole>().HasData(new IdentityRole() { Id = "691190d2-05e1-4dcc-a8bc-a378dc518e29", Name = Framework.Identity.IdentityRole.NGOAdmin, NormalizedName = Framework.Identity.IdentityRole.NGOAdmin, ConcurrencyStamp = "614b9ffd-8694-4610-bbf0-8c4fe82b6799" });
            builder.Entity<IdentityRole>().HasData(new IdentityRole() { Id = "731570ea-7c31-462f-bc9c-bcaafba892a1", Name = Framework.Identity.IdentityRole.Volunteer, NormalizedName = Framework.Identity.IdentityRole.Volunteer, ConcurrencyStamp = "471befb4-f2ec-434f-a195-b3963a502717" });
            builder.Entity<IdentityRole>().HasData(new IdentityRole() { Id = "b9aedc08-76f8-4017-9156-27180e377dca", Name = Framework.Identity.IdentityRole.Beneficiary, NormalizedName = Framework.Identity.IdentityRole.Beneficiary, ConcurrencyStamp = "471befb4-f2ec-434f-a195-b3963a502719" });
            builder.Entity<IdentityRole>().HasData(new IdentityRole() { Id = "bd0d075f-663a-445f-8902-b555a09b1d2d", Name = Framework.Identity.IdentityRole.Guest, NormalizedName = Framework.Identity.IdentityRole.Guest, ConcurrencyStamp = "471befb4-f2ec-434f-a195-b3963a502716" });

            builder.Entity<Category>().HasData(new Category() { ID = new Guid("4817ee60-a647-400f-bf3a-265fe184fe81"), Name = "Seniori 65+", Description = "Seniori 65+", CategoryStatus = CategoryStatus.Approved, AddedBy = AddedBy.PlatformAdmin, CreatedOn = new DateTime(2020, 04, 26) });
            builder.Entity<Category>().HasData(new Category() { ID = new Guid("b6619b80-4fb7-48a8-8a2e-9383afbdff93"), Name = "Persoane cu dizabilitați", Description = "Persoane cu dizabilitați", CategoryStatus = CategoryStatus.Approved, AddedBy = AddedBy.PlatformAdmin, CreatedOn = new DateTime(2020, 04, 26) });
            builder.Entity<Category>().HasData(new Category() { ID = new Guid("887e9957-4f9b-4b13-878d-643c5040c19e"), Name = "Persoane autoizolate", Description = "Persoane autoizolate", CategoryStatus = CategoryStatus.Approved, AddedBy = AddedBy.PlatformAdmin, CreatedOn = new DateTime(2020, 04, 26) });
            builder.Entity<Category>().HasData(new Category() { ID = new Guid("b285dbbc-6696-4dbd-80ff-795c83c13c42"), Name = "Cazuri sociale", Description = "Cazuri sociale", CategoryStatus = CategoryStatus.Approved, AddedBy = AddedBy.PlatformAdmin, CreatedOn = new DateTime(2020, 04, 26) });

            builder.Entity<Service>().HasData(new Service() { ID = new Guid("1261ee94-293e-4a4d-99f6-5e4f42451093"), Name = "Hrana si stricta necesitate", Description = "Hrana si stricta necesitate", ServiceStatus = ServiceStatus.Approved, AddedBy = AddedBy.PlatformAdmin, CreatedOn = new DateTime(2020, 04, 26) });
            builder.Entity<Service>().HasData(new Service() { ID = new Guid("b7fca1ba-0899-41f0-950a-9bfe2673c931"), Name = "Medicamente", Description = "Medicamente", ServiceStatus = ServiceStatus.Approved, AddedBy = AddedBy.PlatformAdmin, CreatedOn = new DateTime(2020, 04, 26) });
            builder.Entity<Service>().HasData(new Service() { ID = new Guid("e71af559-176d-43ff-821e-760290d62dd6"), Name = "Plata facturi", Description = "Plata facturi", ServiceStatus = ServiceStatus.Approved, AddedBy = AddedBy.PlatformAdmin, CreatedOn = new DateTime(2020, 04, 26) });
        }
    }
}