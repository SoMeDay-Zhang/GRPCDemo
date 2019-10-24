using System;

namespace Utils
{
    public interface IRepository<T> : IPrimitiveRepository<T, Guid>
    {
    }
}