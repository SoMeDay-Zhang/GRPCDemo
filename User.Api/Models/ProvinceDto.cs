using System;

namespace User.Api.Models
{
    /// <summary>
    /// 省传输模型
    /// </summary>
    public class ProvinceDto
    {
        public string Code { get; set; }
        public string CreateTime { get; set; }
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string UpdateTime { get; set; }
    }
}