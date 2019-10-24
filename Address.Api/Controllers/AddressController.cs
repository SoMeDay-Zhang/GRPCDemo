using System;
using AddressDto;
using AddressService;
using Microsoft.AspNetCore.Mvc;

namespace Address.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IAddressService _addressService;

        public AddressController(IAddressService addressService)
        {
            _addressService = addressService;
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
    }
}