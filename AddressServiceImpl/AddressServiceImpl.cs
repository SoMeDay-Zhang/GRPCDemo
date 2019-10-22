using System;
using Address.Domain.IRepositories;
using AddressDto;
using AddressService;

namespace AddressServiceImpl
{
    public class AddressServiceImpl : IAddressService
    {
        private readonly IAddressRepository _addressRepository;

        public AddressServiceImpl(IAddressRepository addressRepository)
        {
            _addressRepository = addressRepository;
        }

        public void CreateAddress(AddressCreateDto addressCreate)
        {
            _addressRepository.Create(new Address.Domain.Address
            {
                City = addressCreate.City,
                County = addressCreate.County,
                Province = addressCreate.Province,
                ID = Guid.NewGuid()
            });
        }

        public AddressDto.AddressDto Retrieve(Guid id)
        {
            Address.Domain.Address address = _addressRepository.Retrieve(id);
            return new AddressDto.AddressDto
            {
                City = address.City,
                County = address.County,
                ID = address.ID,
                Province = address.Province
            };
        }
    }
}