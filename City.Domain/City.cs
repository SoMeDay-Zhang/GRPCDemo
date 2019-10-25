using Utils;

namespace City.Domain
{
    public sealed class City : Entity
    {
        public string Name { get; set; }

        public string Code { get; set; }
    }
}