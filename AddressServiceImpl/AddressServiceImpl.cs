using System;
using System.Threading.Tasks;
using Address.Domain;
using Address.Tools;
using AddressDto;
using AddressService;
using Utils;

namespace AddressServiceImpl
{
    public class AddressServiceImpl : IAddressService
    {
        private readonly IRepository<Province> _provinceRepository;
        private readonly IRepository<Address.Domain.Address> _repository;
        private readonly IAddressUnitOfWork _unitOfWork;

        public AddressServiceImpl(IRepository<Address.Domain.Address> repository,
            IRepository<Province> provinceRepository, IAddressUnitOfWork unitOfWork)
        {
            _repository = repository;
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
            Address.Domain.Address address = await _repository.RetrieveAsync(id);
            return new AddressDto.AddressDto
            {
                City = address.City,
                County = address.County,
                ID = address.ID,
                Province = address.Province
            };
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
    }
}