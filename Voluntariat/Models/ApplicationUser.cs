using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Voluntariat.Models
{
    public class ApplicationUser: IdentityUser
    {
        [PersonalData]
        public int DialingCode { get; set; }

        [PersonalData]
        public double Latitude { get; set; }
        
        [PersonalData]
        public double Longitude { get; set; }

        [PersonalData]
        public string Address { get; set; }
    }
}
