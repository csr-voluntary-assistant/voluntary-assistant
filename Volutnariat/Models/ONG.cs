using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Volutnariat.Models
{
    public class ONG
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public virtual ICollection<OngUser> Volunteers { get; set; }
    }
}
