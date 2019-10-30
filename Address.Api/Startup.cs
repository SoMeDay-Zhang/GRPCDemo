using Address.Domain.IRepositories;
using Address.Tools;
using AddressEFRepository;
using AddressEFRepository.RepositoryImpls;
using AddressService;
using City.EFRepository;
using City.Tools;
using CityService;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProvinceService;
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
        public virtual void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddDbContextPool<AddressContext>(options => options.UseMySql(Configuration.GetConnectionString("AddressContext")));
            services.AddDbContextPool<CityContext>(options => options.UseMySql(Configuration.GetConnectionString("AddressContext")));
            services.AddTransient<IAddressRepository, AddressRepositoryImpl>();
            services.AddTransient<IAddressUnitOfWork, AddressUnitOfWork<AddressContext>>();
            services.AddTransient<ICityUnitOfWork, CityUnitOfWork<CityContext>>();
            services.AddTransient<IAddressService, AddressServiceImpl.AddressServiceImpl>();
            services.AddTransient<IProvinceService, ProvinceServiceImpl.ProvinceServiceImpl>();
            services.AddTransient<ICityService, CityServiceImpl.CityServiceImpl>();
            services.AddGrpc();
            services.AddMvc(options => options.Filters.Add(typeof(TransactionActionFilter)));
            services.AddRepositories(new[] { "Address.Domain", "City.Domain" }, typeof(AddressContext), ServiceLifetime.Transient);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

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