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
    }
}