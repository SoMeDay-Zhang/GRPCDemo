using Utils;

namespace Address.Domain
{
    /// <summary>
    /// 省
    /// </summary>
    public sealed class Province : Entity
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 代码
        /// </summary>
        public string Code { get; set; }
    }
}