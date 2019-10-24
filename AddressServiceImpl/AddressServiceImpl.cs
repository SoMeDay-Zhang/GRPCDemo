using System;
using Address.Domain;
using Address.Domain.IRepositories;
using AddressDto;
using AddressService;
using Utils;

namespace AddressServiceImpl
{
    public class AddressServiceImpl : IAddressService
    {
        private readonly IAddressRepository _addressRepository;
        private readonly IRepository<Address.Domain.Address> _repository;
        private readonly IRepository<Province> _provinceRepository;

        public AddressServiceImpl(IAddressRepository addressRepository, IRepository<Address.Domain.Address> repository, IRepository<Province> provinceRepository)
        {
            _addressRepository = addressRepository;
            _repository = repository;
            _provinceRepository = provinceRepository;
        }

        public void CreateAddress(AddressCreateDto addressCreate)
        {
            _addressRepository.Create(new Address.Domain.Address
            {
                City = addressCreate.City,
                County = addressCreate.County,
                Province = addressCreate.Province
            });
        }

        public AddressDto.AddressDto Retrieve(Guid id)
        {
            Address.Domain.Address address = _repository.Retrieve(id);
            return new AddressDto.AddressDto
            {
                City = address.City,
                County = address.County,
                ID = address.ID,
                Province = address.Province
            };
        }

        public ProvinceDto RetrieveProvince(Guid id)
        {
            Province province = _provinceRepository.Retrieve(id);
            return new ProvinceDto
            {
                Code = province.Code,
                Name = province.Name,
                ID = province.ID,
                CreateTime = province.CreateTime,
                UpdateTime = province.UpdateTime,
            };
        }
    }
}