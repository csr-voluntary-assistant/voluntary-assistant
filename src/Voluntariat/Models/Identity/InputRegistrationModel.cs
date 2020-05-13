using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;

namespace Voluntariat.Models.Identity
{
    public class InputRegistrationModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Phone]
        [Display(Name = "Phone number")]
        [Required]
        public string PhoneNumber { get; set; }

        [Display(Name = "Country Code")]
        [Required]
        public int DialingCode { get; set; }

        [Display(Name = "Address")]
        [Required]
        public string Address { get; set; }

        [HiddenInput]
        [Required(ErrorMessage = "Please fill in the Address field manually and select an address from the autocomplete list")]
        public double? Longitude { get; set; }

        [HiddenInput]
        public double? Latitude { get; set; }

        public RegisterAs RegisterAs { get; set; }

        [Display(Name = "Action limit (km)")]
        [DisplayFormat(DataFormatString = "{0:[C]}", ApplyFormatInEditMode = true)]
        public decimal RangeInKm { get; set; }

        [Display(Name = "Driver licence")]
        public bool HasDriverLicence { get; set; }

        [Display(Name = "Transportation method")]
        [Required]
        public TransportationMethod TransportationMethod { get; set; }

        [Display(Name = "Other")]
        public string OtherTransportationMethod { get; set; }

        public Guid? NGOID { get; set; }

        public NGORegistrationModel NGORegistrationModel { get; set; }

        [Display(Name = "Activate notifications from other NGOs")]
        public bool ActivateNotificationsFromOtherNGOs { get; set; }
    }

    public enum RegisterAs
    {
        NGO = 0,
        Volunteer = 1,
        Beneficiary = 2
    }
}
