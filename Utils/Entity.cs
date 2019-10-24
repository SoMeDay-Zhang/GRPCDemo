using System;

namespace Utils
{
    public abstract class Entity : IEntity<Guid>
    {
        protected Entity()
        {
            ID = Guid.NewGuid();
            CreateTime = DateTime.Now;
            UpdateTime = DateTime.MinValue;
        }

        public Guid ID { get; private set; }
        public DateTime CreateTime { get; private set; }
        public DateTime UpdateTime { get; set; }
    }
}
