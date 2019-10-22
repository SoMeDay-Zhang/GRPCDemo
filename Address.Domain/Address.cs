using System;

namespace Address.Domain
{
    public sealed class Address
    {
        public Guid ID { get; set; }

        public string Province { get; set; }

        public string City { get; set; }

        public string County { get; set; }
    }
}