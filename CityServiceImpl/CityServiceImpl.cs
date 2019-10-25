using System.Threading.Tasks;
using City.Tools;
using CityService;

namespace CityServiceImpl
{
    public sealed class CityServiceImpl : ICityService
    {
        private readonly ICityUnitOfWork _unitOfWork;

        public CityServiceImpl(ICityUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task CreateAsync(string name, string code)
        {
            _unitOfWork.RegisterNew(new City.Domain.City
            {
                Code = code,
                Name = name
            });
            await _unitOfWork.CommitAsync();
        }
    }
}