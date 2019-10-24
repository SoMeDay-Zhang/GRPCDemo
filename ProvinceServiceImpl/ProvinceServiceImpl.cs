using Address.Domain;
using ProvinceService;
using Utils;

namespace ProvinceServiceImpl
{
    public sealed class ProvinceServiceImpl : IProvinceService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProvinceServiceImpl(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Create(string name, string code)
        {
            _unitOfWork.RegisterNew(new Province
            {
                Name = name,
                Code = code
            });
            _unitOfWork.Commit();
        }
    }
}