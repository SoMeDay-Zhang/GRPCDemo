using System;
using System.IO;
using System.Linq;
using AddressEFRepository;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Address.IntegrationTest
{
    public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            string projectDir = Directory.GetCurrentDirectory();
            string configPath = Path.Combine(projectDir, "appsettings.json");
            builder.ConfigureAppConfiguration((context, conf) =>
            {
                conf.AddJsonFile(configPath);
            });

            builder.ConfigureServices(services =>
            {
                ServiceDescriptor descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<AddressContext>));

                if (descriptor != null)
                {
                    services.Remove(descriptor);
                }

                services.AddDbContextPool<AddressContext>((options, context) =>
                {
                    //var configuration = options.GetRequiredService<IConfiguration>();
                    //string connectionString = configuration.GetConnectionString("TestAddressDb");
                    //context.UseMySql(connectionString);
                    context.UseInMemoryDatabase("InMemoryDbForTesting");

                });

                // Build the service provider.
                ServiceProvider sp = services.BuildServiceProvider();
                // Create a scope to obtain a reference to the database
                // context (ApplicationDbContext).
                using IServiceScope scope = sp.CreateScope();
                IServiceProvider scopedServices = scope.ServiceProvider;
                var db = scopedServices.GetRequiredService<AddressContext>();
                var logger = scopedServices.GetRequiredService<ILogger<CustomWebApplicationFactory<TStartup>>>();

                // Ensure the database is created.
                db.Database.EnsureCreated();

                try
                {
                    // Seed the database with test data.
                    Utilities.ReinitializeDbForTests(db);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "An error occurred seeding the " + "database with test messages. Error: {Message}", ex.Message);
                }
            });
        }
    }
}