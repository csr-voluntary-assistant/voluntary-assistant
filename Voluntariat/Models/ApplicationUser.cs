using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Voluntariat.Models
{
    public class ApplicationUser : IdentityUser
    {
        [PersonalData]
        public int DialingCode { get; set; }

        [PersonalData]
        public double Latitude { get; set; }

        [PersonalData]
        public double Longitude { get; set; }

        [PersonalData]
        public string Address { get; set; }

        [Column(TypeName = "decimal(16,2)")]
        public decimal ActionLimit { get; set; }

        [PersonalData]
        public bool HasDriverLicence { get; set; }

        public TransportationMethod TransportationMethod { get; set; }

        public string OtherTransportationMethod { get; set; }
    }

    public enum TransportationMethod
    {
        None = 0,
        Bicycle = 1,
        Walk = 2,
        Car = 3,
        PublicTransportation = 4,
        Scooter = 5,
        Motorbike = 6,
        Other = 7
    }
}
