using System;
using System.Threading.Tasks;
using AddressDto;

namespace AddressService
{
    /// <summary>
    /// AddressService
    /// </summary>
    public interface IAddressService
    {
        /// <summary>
        /// 地址创建传输模型
        /// </summary>
        /// <param name="addressCreate"></param>
        Task CreateAddressAsync(AddressCreateDto addressCreate);

        /// <summary>
        /// 检索地址
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<AddressDto.AddressDto> RetrieveAsync(Guid id);

        /// <summary>
        /// 获取检索省
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ProvinceDto> RetrieveProvinceAsync(Guid id);
    }
}