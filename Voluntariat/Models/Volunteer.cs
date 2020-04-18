using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Voluntariat.Models
{
    public class Volunteer
    {
        [Key]
        public Guid ID { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public Guid OngID { get; set; }


        [NotMapped]
        [DisplayName("Nume voluntar")]
        public string Name { get; set; }
    }
}