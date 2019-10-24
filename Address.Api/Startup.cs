using System;
using Address.Domain.IRepositories;
using AddressEFRepository;
using AddressEFRepository.RepositoryImpls;
using AddressService;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Utils;

namespace Address.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddDbContext<AddressContext>(options =>
                options.UseMySql(Configuration.GetConnectionString("AddressContext")));
            services.AddScoped<IAddressRepository, AddressRepositoryImpl>();
            services.AddScoped<IAddressService, AddressServiceImpl.AddressServiceImpl>();
            services.AddGrpc();
            services.AddRepositories(new[] { "Address.Domain" }, typeof(AddressContext), ServiceLifetime.Transient);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapGrpcService<Services.AddressService>();
            });
        }
    }
}