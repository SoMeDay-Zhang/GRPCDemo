namespace User.Api.Models
{
    /// <summary>
    /// 地址创建模型
    /// </summary>
    public sealed class AddressCreateModel
    {
        public string Province { get; set; }

        public string City { get; set; }

        public string County { get; set; }
    }
}
