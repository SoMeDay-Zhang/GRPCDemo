using System;
using System.Transactions;
using AddressDto;
using AddressService;
using Microsoft.AspNetCore.Mvc;
using ProvinceService;
using Utils;

namespace Address.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IAddressService _addressService;
        private readonly IProvinceService _provinceService;

        public AddressController(IAddressService addressService, IProvinceService provinceService)
        {
            _addressService = addressService;
            _provinceService = provinceService;
        }

        [HttpPost]
        public void Add(AddressCreateDto createDto)
        {
            _addressService.CreateAddress(createDto);
        }

        [HttpGet]
        public AddressDto.AddressDto Retrieve(Guid id)
        {
            return _addressService.Retrieve(id);
        }

        [HttpGet]
        public ProvinceDto RetrieveProvince(Guid id)
        {
            return _addressService.RetrieveProvince(id);
        }

        [HttpGet, UoW]
        public void CreateAddressAndProvince()
        {
            _provinceService.Create("四川", "1234");
            _addressService.CreateAddress(new AddressCreateDto
            {
                City = "成都市",
                Province = "四川省",
                County = "武侯区"
            });
        }
    }
}