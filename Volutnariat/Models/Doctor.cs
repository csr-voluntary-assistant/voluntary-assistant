using System;
using System.ComponentModel.DataAnnotations;

namespace Voluntariat.Models
{
    public class Doctor
    {
        [Key]
        public Guid ID { get; set; }

        public Guid OngID { get; set; }
    }
}
