﻿using System;
using System.Linq;
using AddressEFRepository;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Address.IntegrationTest
{
    public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                // Remove the app's ApplicationDbContext registration.
                ServiceDescriptor descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<AddressContext>));

                if (descriptor != null)
                {
                    services.Remove(descriptor);
                }

                // Add ApplicationDbContext using an in-memory database for testing.
                services.AddDbContextPool<AddressContext>((options, context) => { context.UseInMemoryDatabase("InMemoryDbForTesting"); });

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
                    Utilities.InitializeDbForTests(db);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "An error occurred seeding the " + "database with test messages. Error: {Message}", ex.Message);
                }
            });
        }
    }
}