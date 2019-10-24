using Utils;

namespace Address.Domain
{
    public sealed class Address: Entity
    {  
        public string Province { get; set; }

        public string City { get; set; }

        public string County { get; set; }
    }
}