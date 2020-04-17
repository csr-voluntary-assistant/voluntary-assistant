using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Volutnariat.Models
{
    public class OngUser
    {
        public int Id { get; set; }

        [ForeignKey("IdentityId")]
        public virtual IdentityUser Identity { get; set; }
        
        [Key]
        public virtual string IdentityId { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        [Key]
        public int OngId { get; set; }

        [ForeignKey("OngId")]
        public ONG Ong { get; set; }
    }
}
