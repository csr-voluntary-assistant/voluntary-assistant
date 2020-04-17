using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Voluntariat.Models
{
    public class Ong
    {
        [Key]
        public Guid ID { get; set; }

        public string Name { get; set; }

        public OngStatus OngStatus { get; set; }

        public Guid CreatedByID { get; set; }


        public virtual List<Volunteer> Volunteers { get; set; }

        public virtual List<Doctor> Doctors { get; set; }
    }


    public enum OngStatus
    {
        PendingVerification = 0,
        Verified = 1
    }
}