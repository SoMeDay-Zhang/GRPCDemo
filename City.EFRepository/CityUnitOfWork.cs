using City.Tools;
using Microsoft.EntityFrameworkCore;
using Utils;

namespace City.EFRepository
{
    public sealed class CityUnitOfWork<TDbContext> : UnitOfWork<TDbContext>, ICityUnitOfWork where TDbContext : DbContext
    {
        public CityUnitOfWork(TDbContext dbContext) : base(dbContext)
        {
        }
    }
}