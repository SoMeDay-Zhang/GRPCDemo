using System;

namespace AddressDto
{
    public class AddressDto
    {
        public Guid ID { get; set; }

        public string Province { get; set; }

        public string City { get; set; }

        public string County { get; set; }
    }
}