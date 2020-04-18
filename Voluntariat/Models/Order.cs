using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Voluntariat.Models
{
    public class Order
    {
        public Guid ID { get; set; }

        public Guid OngID { get; set; }

        public Guid DoctorID { get; set; }

        public Guid BeneficiaryID { get; set; }

        public string Description { get; set; }

        public OrderStatus Status { get; set; }


        public Guid? VolunteerID { get; set; }

        public Guid? CaretakerID { get; set; }


        public DateTime CreationDate { get; set; }

        public DateTime? CompletionDate { get; set; }



        [NotMapped]
        public string DoctorName { get; set; }

        [NotMapped]
        [DisplayName("Beneficiar")]
        public string BeneficiaryName { get; set; }

        [NotMapped]
        [DisplayName("Telefon beneficiar")]
        public string BeneficiaryPhoneNumber { get; set; }

        [NotMapped]
        [DisplayName("Voluntar")]
        public string VolunteerName { get; set; }

        [NotMapped]
        [DisplayName("Ingrijitor")]
        public string CaretakerName { get; set; }
    }


    public enum OrderStatus
    {
        Ordered = 0,
        InProgress = 1,
        Done = 2
    }
}