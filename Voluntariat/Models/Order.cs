using System;

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
    }


    public enum OrderStatus
    {
        Ordered = 0,
        InProgress = 1,
        Done = 2
    }
}