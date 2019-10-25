using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace City.EFRepository.ModeConfigs
{
    internal sealed class CityConfig : IEntityTypeConfiguration<Domain.City>
    {
        public void Configure(EntityTypeBuilder<Domain.City> builder)
        {
            builder.HasKey(q => q.ID);
            builder.Property(q => q.Name).IsRequired().HasMaxLength(30);
            builder.Property(q => q.Code).IsRequired().HasMaxLength(30);
            builder.Property(q => q.CreateTime).IsRequired();
            builder.Property(q => q.UpdateTime).IsRequired();
        }
    }
}