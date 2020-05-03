using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Voluntariat.Models
{
    public class NGO
    {
        [Key]
        public Guid ID { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Status")]
        public NGOStatus NGOStatus { get; set; }

        public Guid CreatedByID { get; set; }

        [Required]
        [Display(Name = "Headquarters Address")]
        public string HeadquartersAddress { get; set; }

        public double HeadquartersAddressLatitude { get; set; }

        public double HeadquartersAddressLongitude { get; set; }

        [Required]
        [Display(Name = "Headquarters Email")]
        public string HeadquartersEmail { get; set; }

        [Required]
        [Display(Name = "Headquarters Phone Number")]
        public string HeadquartersPhoneNumber { get; set; }

        [Required]
        public int DialingCode { get; set; }

        [Required]
        [Display(Name = "Identification Number")]
        public string IdentificationNumber { get; set; }

        [Required]
        [Display(Name = "Website")]
        public string Website { get; set; }

        [Required]
        public Guid CategoryID { get; set; }

        [Required]
        public Guid ServiceID { get; set; }

        [NotMapped]
        [Display(Name = "Created by")]
        public string CreatedByName { get; set; }

        public string FileIDs { get; set; }
    }

    public enum NGOStatus
    {
        PendingVerification = 0,
        Verified = 1
    }
}