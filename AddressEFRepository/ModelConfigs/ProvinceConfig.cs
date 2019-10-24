using Address.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AddressEFRepository.ModelConfigs
{
    internal sealed class ProvinceConfig : IEntityTypeConfiguration<Province>
    {
        public void Configure(EntityTypeBuilder<Province> builder)
        {
            builder.HasKey(q => q.ID);
            builder.Property(q => q.Name).IsRequired().HasMaxLength(30);
            builder.Property(q => q.Code).IsRequired().HasMaxLength(30);
            builder.Property(q => q.CreateTime).IsRequired();
            builder.Property(q => q.UpdateTime).IsRequired();
        }
    }
}