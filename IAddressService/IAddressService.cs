using System;
using System.Collections.Generic;
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
        /// <param name="addressCreate">创建地址传输模型</param>
        Task CreateAddressAsync(AddressCreateDto addressCreate);

        /// <summary>
        /// 检索地址
        /// </summary>
        /// <param name="id">地址全局唯一标识</param>
        /// <returns>地址传输模型</returns>
        Task<AddressDto.AddressDto> RetrieveAsync(Guid id);

        /// <summary>
        /// 获取检索省
        /// </summary>
        /// <param name="id">省全局唯一标识</param>
        /// <returns>省传输模型</returns>
        Task<ProvinceDto> RetrieveProvinceAsync(Guid id);

        /// <summary>
        /// 获取所有地址
        /// </summary>
        /// <returns></returns>
        Task<IList<AddressDto.AddressDto>> GetAllAddressesAsync();
    }
}