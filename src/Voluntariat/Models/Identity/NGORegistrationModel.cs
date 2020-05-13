using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;

namespace Voluntariat.Models.Identity
{
    public class NGORegistrationModel
    {
        [Required]
        [Display(Name = "NGO name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Country code")]
        public int DialingCode { get; set; }

        [Required]
        [Display(Name = "Headquarters phone number")]
        public string HeadquartersPhoneNumber { get; set; }

        [Required]
        [Display(Name = "Headquarters email")]
        public string HeadquartersEmail { get; set; }

        [Required]
        [Display(Name = "Headquarters address")]
        public string HeadquartersAddress { get; set; } // sediul social

        [HiddenInput]
        public double HeadquartersAddressLatitude { get; set; }

        [HiddenInput]
        public double HeadquartersAddressLongitude { get; set; }

        [Required]
        [Display(Name = "Identification number")]
        public string IdentificationNumber { get; set; } // CUI

        [Required]
        [Display(Name = nameof(Website))]
        public string Website { get; set; }

        [Required]
        public Guid CategoryID { get; set; }

        [Required]
        public Guid ServiceID { get; set; }
    }
}
