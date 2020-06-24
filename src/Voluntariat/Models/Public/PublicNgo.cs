using System;

namespace Voluntariat.Models.Public
{
    public class PublicNgo
    {
        public Guid ID { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Website { get; set; }

        public string CategoryName { get; set; }

        public string ServiceName { get; set; }
    }
}
