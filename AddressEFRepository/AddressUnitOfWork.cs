using Address.Tools;
using Microsoft.EntityFrameworkCore;
using Utils;

namespace AddressEFRepository
{
    public sealed class AddressUnitOfWork<TDbContext> : UnitOfWork<TDbContext>, IAddressUnitOfWork where TDbContext : DbContext
    {
        public AddressUnitOfWork(TDbContext dbContext) : base(dbContext)
        {
        }
    }
}