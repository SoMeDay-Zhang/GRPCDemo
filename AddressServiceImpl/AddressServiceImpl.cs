using System;
using Address.Domain;
using AddressDto;
using AddressService;
using Utils;

namespace AddressServiceImpl
{
    public class AddressServiceImpl : IAddressService
    {
        private readonly IRepository<Province> _provinceRepository;
        private readonly IRepository<Address.Domain.Address> _repository;
        private readonly IUnitOfWork _unitOfWork;

        public AddressServiceImpl(IRepository<Address.Domain.Address> repository,
            IRepository<Province> provinceRepository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _provinceRepository = provinceRepository;
            _unitOfWork = unitOfWork;
        }

        public void CreateAddress(AddressCreateDto addressCreate)
        {
            _unitOfWork.RegisterNew(new Address.Domain.Address
            {
                City = addressCreate.City,
                County = addressCreate.County,
                Province = addressCreate.Province
            });
            throw new Exception();
            _unitOfWork.Commit();
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
                UpdateTime = province.UpdateTime
            };
        }
    }
}