using Voluntariat.Models;

namespace Voluntariat.Services.Identity
{
    public class UserRegistrationModel
    {
        public string Email { get; set; }

        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public int DialingCode { get; set; }
        public string Address { get; set; }
        public double? Longitude { get; set; }
        public double? Latitude { get; set; }
        public bool HasDriverLicence { get; set; }
        public TransportationMethod TransportationMethod { get; set; }
        public string OtherTransportationMethod { get; set; }
        public decimal RangeInKm { get; set; }
    }
}