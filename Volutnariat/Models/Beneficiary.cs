﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Volutnariat.Models
{
    public class Beneficiary
    {
        [Key]
        public Guid ID { get; set; }

        public Guid OngID { get; set; }


        public virtual Ong Ong { get; set; }
    }
}
