using System;
using System.ComponentModel.DataAnnotations;

namespace Voluntariat.Models
{
    public class Category
    {
        [Key]
        public Guid ID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        public CategoryStatus CategoryStatus { get; set; }

        public AddedBy AddedBy { get; set; }

        public DateTime CreatedOn { get; set; }
    }

    public enum CategoryStatus
    {
        Pending = 0,
        Approved = 1,
        Declined = 2
    }

    public enum AddedBy
    {
        PlatformAdmin = 0,
        NGO = 1
    }
}