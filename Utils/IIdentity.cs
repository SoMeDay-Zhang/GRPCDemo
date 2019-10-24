namespace Utils
{
    public interface IIdentity<out TIdentity>
    {
        /// <summary>
        /// 实体全局唯一标识
        /// </summary>
        TIdentity ID { get; }
    }
}
