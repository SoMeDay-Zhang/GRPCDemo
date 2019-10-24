using System;
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
        void CreateAddress(AddressCreateDto addressCreate);

        /// <summary>
        /// 检索地址
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        AddressDto.AddressDto Retrieve(Guid id);

        /// <summary>
        /// 获取检索省
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ProvinceDto RetrieveProvince(Guid id);
    }
}