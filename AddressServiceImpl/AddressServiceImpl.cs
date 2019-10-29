using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Address.Domain;
using Address.Tools;
using AddressDto;
using AddressService;
using Utils;
using System.Linq;

namespace AddressServiceImpl
{
    public sealed class AddressServiceImpl : IAddressService
    {
        private readonly IRepository<Province> _provinceRepository;
        private readonly IRepository<Address.Domain.Address> _addressRepository;
        private readonly IAddressUnitOfWork _unitOfWork;

        public AddressServiceImpl(IRepository<Address.Domain.Address> addressRepository,
            IRepository<Province> provinceRepository, IAddressUnitOfWork unitOfWork)
        {
            _addressRepository = addressRepository;
            _provinceRepository = provinceRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task CreateAddressAsync(AddressCreateDto addressCreate)
        {
            _unitOfWork.RegisterNew(new Address.Domain.Address
            {
                City = addressCreate.City,
                County = addressCreate.County,
                Province = addressCreate.Province
            });
            await _unitOfWork.CommitAsync();
            //throw new Exception();
        }

        public async Task<AddressDto.AddressDto> RetrieveAsync(Guid id)
        {
            Address.Domain.Address address = await _addressRepository.RetrieveAsync(id);
            return ConvertToAddressDto(address);
        }

        public async Task<ProvinceDto> RetrieveProvinceAsync(Guid id)
        {
            Province province = await _provinceRepository.RetrieveAsync(id);
            return new ProvinceDto
            {
                Code = province.Code,
                Name = province.Name,
                ID = province.ID,
                CreateTime = province.CreateTime,
                UpdateTime = province.UpdateTime
            };
        }

        public async Task<IList<AddressDto.AddressDto>> GetAllAddressesAsync()
        {
            IList<Address.Domain.Address> addresses = await _addressRepository.GetAllAsync();
            return addresses.Select(ConvertToAddressDto).ToList();
        }


        #region Private Methods

        private static AddressDto.AddressDto ConvertToAddressDto(Address.Domain.Address address)
        {
            return new AddressDto.AddressDto
            {
                City = address.City,
                County = address.County,
                ID = address.ID,
                Province = address.Province
            };
        }

        #endregion
    }
}