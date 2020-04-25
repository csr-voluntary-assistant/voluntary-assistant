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

        public Guid OngID { get; set; }

        public int? RangeInMeters { get; set; }

        [NotMapped]
        [DisplayName("Nume voluntar")]
        public string Name { get; set; }
    }
}