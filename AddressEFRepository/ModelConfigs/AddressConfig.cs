using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AddressEFRepository.ModelConfigs
{
    internal sealed class AddressConfig : IEntityTypeConfiguration<Address.Domain.Address>
    {
        public void Configure(EntityTypeBuilder<Address.Domain.Address> builder)
        {
            builder.HasKey(q => q.ID);
            builder.Property(q => q.City).IsRequired().HasMaxLength(30);
            builder.Property(q => q.Province).IsRequired().HasMaxLength(30);
            builder.Property(q => q.County).IsRequired().HasMaxLength(30);
        }
    }
}