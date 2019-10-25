using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace City.EFRepository
{
    public sealed class CityContext : DbContext
    {
        public CityContext(DbContextOptions<CityContext> options)
            : base(options)
        {
        }

        public DbSet<Domain.City> Cities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            IEnumerable<Type> typesToRegister = Assembly.GetExecutingAssembly().GetTypes()
                .Where(q => q.GetInterface(typeof(IEntityTypeConfiguration<>).FullName) != null);

            foreach (Type type in typesToRegister)
            {
                dynamic configurationInstance = Activator.CreateInstance(type);
                modelBuilder.ApplyConfiguration(configurationInstance);
            }
        }
    }
}