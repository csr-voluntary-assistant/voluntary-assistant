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
        None = 1,
        Bicycle = 2,
        Walk = 3,
        Car = 4,
        PublicTransportation = 5,
        Scooter = 6,
        Motorbike = 7,
        Other = 8
    }
}
