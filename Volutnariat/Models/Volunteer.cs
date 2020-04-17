using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace Voluntariat.Models
{
    public class Volunteer
    {
        [Key]
        public Guid ID { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }





        public Guid OngID { get; set; }

        public virtual Ong Ong { get; set; }

        /*
         * 
         * 
         * Le facem mapping dupa ce aveam partea in care adminul de ONG adauga voluntari in platforma
        public virtual IdentityUser Identity { get; set; }

        public virtual string IdentityId { get; set; }
        */
    }
}