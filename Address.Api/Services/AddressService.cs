using System;
using System.Globalization;
using System.Threading.Tasks;
using AddressDto;
using AddressService;
using Grpc.Core;
using GrpcAddress;

namespace Address.Api.Services
{
    public sealed class AddressService : Addresses.AddressesBase
    {
        private readonly IAddressService _addressService;

        public AddressService(IAddressService addressService)
        {
            _addressService = addressService;
        }

        public override async Task<CreateAddressResponse> CreateAddressAsync(CreateAddressRequest request,
            ServerCallContext context)
        {
            await _addressService.CreateAddressAsync(new AddressCreateDto
            {
                Province = request.Province,
                City = request.City,
                County = request.County
            });

            return new CreateAddressResponse
            {
                Succeed = true,
                Message = "创建成功"
            };
        }

        public override async Task<RetrieveAddressResponse> RetrieveAddressAsync(RetrieveAddressRequest request, ServerCallContext context)
        {
            AddressDto.AddressDto addressDto = await _addressService.RetrieveAsync(new Guid(request.ID));
            return new RetrieveAddressResponse
            {
                ID = addressDto.ID.ToString(),
                Province = addressDto.Province,
                City = addressDto.City,
                County = addressDto.County
            };
        }

        public override Task<RetrieveProvinceResponse> RetrieveProvince(RetrieveProvinceReqeust request, ServerCallContext context)
        {
            return Task.FromResult(new RetrieveProvinceResponse
            {
                Code = "123123",
                CreateTime = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                ID = Guid.NewGuid().ToString(),
                Name = "云南省",
                UpdateTime = DateTime.Now.ToString(CultureInfo.InvariantCulture)
            });
        }
    }
}