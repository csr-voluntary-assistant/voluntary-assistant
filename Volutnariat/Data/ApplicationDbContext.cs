using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Volutnariat.Models;

namespace Volutnariat.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<IdentityRole>().HasData(new IdentityRole { Name = "Admin", NormalizedName = "Admin".ToUpper() });
        }

        //public DbSet<ONG> ONGs { get; set; }

        //public DbSet<OngUser> OngUsers { get; set; }
    }
}
