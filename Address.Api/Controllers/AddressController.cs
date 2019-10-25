using System;
using System.Threading.Tasks;
using System.Transactions;
using AddressDto;
using AddressService;
using CityService;
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
        private readonly ICityService _cityService;

        public AddressController(IAddressService addressService, IProvinceService provinceService, ICityService cityService)
        {
            _addressService = addressService;
            _provinceService = provinceService;
            _cityService = cityService;
        }

        [HttpPost]
        public async Task Add(AddressCreateDto createDto)
        {
            await _addressService.CreateAddressAsync(createDto);
        }

        [HttpGet]
        public async Task<AddressDto.AddressDto> Retrieve(Guid id)
        {
            return await _addressService.RetrieveAsync(id);
        }

        [HttpGet]
        public async Task<ProvinceDto> RetrieveProvince(Guid id)
        {
            return await _addressService.RetrieveProvinceAsync(id);
        }

        [HttpGet,UoW]
        public async Task CreateAddressAndProvince()
        {
            using var ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            await _provinceService.CreateAsync("四川", "1234");
            await _cityService.CreateAsync("成都市", "1232134");
            await _addressService.CreateAddressAsync(new AddressCreateDto
            {
                City = "成都市",
                Province = "四川省",
                County = "武侯区"
            });
            ts.Complete();
        }
    }
}