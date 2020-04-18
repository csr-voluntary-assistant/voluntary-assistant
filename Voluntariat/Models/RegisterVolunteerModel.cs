using System.ComponentModel.DataAnnotations;

namespace Voluntariat.Models
{
    public class RegisterVolunteerModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}