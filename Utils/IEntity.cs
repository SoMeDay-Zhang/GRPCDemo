using System;

namespace Utils
{
    public interface IEntity<out TIdentity> : IIdentity<TIdentity>
    {
        /// <summary>
        /// 创建时间
        /// </summary>
        DateTime CreateTime { get; }

        /// <summary>
        /// 更新时间
        /// </summary>
        DateTime UpdateTime { get; set; }
    }
}
