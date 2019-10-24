using System;

namespace AddressDto
{
    public class ProvinceDto
    {
        public Guid ID { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        public DateTime CreateTime { get; set; }

        public DateTime UpdateTime { get; set; }
    }
}