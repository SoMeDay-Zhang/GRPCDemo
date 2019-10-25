using System.Threading.Tasks;
using Address.Domain;
using Address.Tools;
using ProvinceService;

namespace ProvinceServiceImpl
{
    public sealed class ProvinceServiceImpl : IProvinceService
    {
        private readonly IAddressUnitOfWork _unitOfWork;

        public ProvinceServiceImpl(IAddressUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task CreateAsync(string name, string code)
        {
            _unitOfWork.RegisterNew(new Province
            {
                Name = name,
                Code = code
            });
            await _unitOfWork.CommitAsync();
        }
    }
}