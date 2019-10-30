using System;
using System.Diagnostics;
using System.Threading.Tasks;
using GrpcAddress;
using Microsoft.AspNetCore.Mvc;
using User.Api.Models;

namespace User.Api.Controllers
{
    [Route("api/[controller]/[action]"), ApiController]
    public class AddressController : ControllerBase
    {
        private readonly Addresses.AddressesClient _addressesClient;
        private readonly WeatherForecastDbContext _weatherForecastDbContext;

        public AddressController(Addresses.AddressesClient addressesClient, WeatherForecastDbContext weatherForecastDbContext)
        {
            _addressesClient = addressesClient;
            _weatherForecastDbContext = weatherForecastDbContext;
        }

        /// <summary>
        /// 创建地址
        /// </summary>
        /// <param name="createModel"></param>
        [HttpPost]
        public void Create(AddressCreateModel createModel)
        {
            _addressesClient.CreateAddressAsync(new CreateAddressRequest
            {
                Province = createModel.Province,
                City = createModel.City,
                County = createModel.County
            });

            _weatherForecastDbContext.Add(new WeatherForecast
            {
                ID = Guid.NewGuid(),
                Date = DateTime.Now,
                Summary = "Test",
                TemperatureC = 123
            });
            _weatherForecastDbContext.SaveChanges();
        }

        /// <summary>
        /// 检索
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public AddressDto.AddressDto Retrieve(Guid id)
        {
            try
            {
                RetrieveAddressResponse result = _addressesClient.RetrieveAddressAsync(new RetrieveAddressRequest
                {
                    ID = id.ToString()
                });
                return new AddressDto.AddressDto
                {
                    ID = new Guid(result.ID),
                    City = result.City,
                    Province = result.Province,
                    County = result.County
                };
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
                return null;
            }
        }
    }
}