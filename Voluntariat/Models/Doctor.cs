using System;
using System.ComponentModel.DataAnnotations;

namespace Voluntariat.Models
{
    public class Doctor
    {
        [Key]
        public Guid ID { get; set; }

        public Guid OngID { get; set; }

        public DoctorStatus Status { get; set; }
    }

    public enum DoctorStatus
    {
        PendingVerification = 0,
        Verified = 1
    }
}
