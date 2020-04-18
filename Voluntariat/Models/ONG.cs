using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Voluntariat.Models
{
    public class Ong
    {
        [Key]
        public Guid ID { get; set; }

        [Required]
        public string Name { get; set; }

        public OngStatus OngStatus { get; set; }

        public Guid CreatedByID { get; set; }



        [NotMapped]
        public string CreatedByName { get; set; }
    }


    public enum OngStatus
    {
        PendingVerification = 0,
        Verified = 1
    }
}