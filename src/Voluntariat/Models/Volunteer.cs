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

        public Guid? NGOID { get; set; }

        [NotMapped]
        [DisplayName("Nume voluntar")]
        public string Name { get; set; }

        public VolunteerStatus VolunteerStatus { get; set; }

        public bool ActivateNotificationsFromOtherNGOs { get; set; }

        public DateTime? UnaffiliationStartTime { get; set; }
    }

    public enum VolunteerStatus
    {
        PendingVerification = 0,
        Verified = 1,
    }
}