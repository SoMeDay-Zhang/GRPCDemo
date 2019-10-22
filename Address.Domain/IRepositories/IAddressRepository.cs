using System;

namespace Address.Domain.IRepositories
{
    public interface IAddressRepository
    {
        Address Retrieve(Guid id);

        void Create(Address address);
    }
}