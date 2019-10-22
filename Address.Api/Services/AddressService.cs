using System;
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

        public override Task<CreateAddressResponse> CreateAddress(CreateAddressRequest request,
            ServerCallContext context)
        {
            _addressService.CreateAddress(new AddressCreateDto
            {
                Province = request.Province,
                City = request.City,
                County = request.County
            });

            return Task.FromResult(new CreateAddressResponse
            {
                Succeed = true,
                Message = "创建成功"
            });
        }

        public override Task<RetrieveAddressResponse> RetrieveAddress(RetrieveAddressRequest request, ServerCallContext context)
        {
            AddressDto.AddressDto addressDto = _addressService.Retrieve(new Guid(request.ID));
            return Task.FromResult(new RetrieveAddressResponse
            {
                ID = addressDto.ID.ToString(),
                Province = addressDto.Province,
                City = addressDto.City,
                County = addressDto.County
            });
        }
    }
}