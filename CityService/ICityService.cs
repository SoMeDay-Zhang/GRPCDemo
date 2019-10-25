using System.Threading.Tasks;

namespace CityService
{
    public interface ICityService
    {
        /// <summary>
        /// 创建城市
        /// </summary>
        /// <param name="name"></param>
        /// <param name="code"></param>
        Task CreateAsync(string name, string code);
    }
}