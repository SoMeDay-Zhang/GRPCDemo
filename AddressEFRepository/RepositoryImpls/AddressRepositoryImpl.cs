using System;
using System.Linq;
using Address.Domain.IRepositories;

namespace AddressEFRepository.RepositoryImpls
{
    public sealed class AddressRepositoryImpl : IAddressRepository
    {
        private readonly AddressContext _addressContext;

        public AddressRepositoryImpl(AddressContext addressContext)
        {
            _addressContext = addressContext;
        }

        public Address.Domain.Address Retrieve(Guid id)
        {
            return _addressContext.Addresses.FirstOrDefault(q => q.ID.Equals(id));
        }

        public void Create(Address.Domain.Address address)
        {
            _addressContext.Addresses.Add(address);
            _addressContext.SaveChanges();
        }
    }
}