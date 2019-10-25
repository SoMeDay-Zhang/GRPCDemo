using System.Threading.Tasks;

namespace ProvinceService
{
    public interface IProvinceService
    {
        Task CreateAsync(string name, string code);
    }
}