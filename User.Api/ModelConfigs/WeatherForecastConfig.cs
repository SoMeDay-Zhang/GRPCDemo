using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace User.Api.ModelConfigs
{
    internal sealed class WeatherForecastConfig : IEntityTypeConfiguration<WeatherForecast>
    {
        public void Configure(EntityTypeBuilder<WeatherForecast> builder)
        {
            builder.HasKey(q => q.ID);
            builder.Property(q => q.Summary).IsRequired().HasMaxLength(30);
            builder.Property(q => q.TemperatureC).IsRequired();
            builder.Property(q => q.Date).IsRequired();
        }
    }
}