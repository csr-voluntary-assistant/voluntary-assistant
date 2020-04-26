using System;
using System.ComponentModel.DataAnnotations;

namespace Voluntariat.Models
{
    public class Service
    {
        [Key]
        public Guid ID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        public ServiceStatus ServiceStatus { get; set; }

        public AddedBy AddedBy { get; set; }

        public int VolunteersCount { get; set; }

        public int BeneficiariesCount { get; set; }

        public int NGOsCount { get; set; }

        public int ProjectsCount { get; set; }

        public DateTime CreatedOn { get; set; }
    }

    public enum ServiceStatus
    {
        Pending = 0,
        Approved = 1,
        Declined = 2
    }
}
