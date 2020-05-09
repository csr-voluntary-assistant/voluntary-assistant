using System;
using System.ComponentModel.DataAnnotations;

namespace Voluntariat.Models
{
    public class Beneficiary
    {
        [Key]
        public Guid ID { get; set; }

        public Guid NGOID { get; set; }

        public BeneficiaryStatus Status { get; set; }
    }

    public enum BeneficiaryStatus
    {
        PendingVerification = 0,
        Verified = 1
    }
}