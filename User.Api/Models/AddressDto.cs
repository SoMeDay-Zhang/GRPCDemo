using System;

namespace User.Api.Models
{
    /// <summary>
    /// 地址传输模型
    /// </summary>
    public sealed class AddressDto
    {
        public Guid ID { get; set; }

        public string Province { get; set; }

        public string City { get; set; }

        public string County { get; set; }
    }
}