﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Address.Api;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Xunit;
using System.Linq;

namespace Address.IntegrationTest
{
    public class AddressControllerIntegrationTest : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        public AddressControllerIntegrationTest(CustomWebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });
        }

        private readonly HttpClient _client;

        [Fact]
        public async Task Get_AllAddressAndRetrieveAddress()
        {
            string allAddressUri = $"/api/Address/GetAll";
            HttpResponseMessage allAddressesHttpResponse = await _client.GetAsync(allAddressUri);

            allAddressesHttpResponse.EnsureSuccessStatusCode();

            string allAddressStringResponse = await allAddressesHttpResponse.Content.ReadAsStringAsync();
            var addresses = JsonConvert.DeserializeObject<IList<Domain.Address>>(allAddressStringResponse);
            Assert.Equal(3, addresses.Count);

            //TODO:RetieveAddress
            Domain.Address address = addresses.First();
            string retrieveUri = $"/api/Address/Retrieve?id={address.ID}";
            HttpResponseMessage addressHttpResponse = await _client.GetAsync(retrieveUri);

            // Must be successful.
            addressHttpResponse.EnsureSuccessStatusCode();

            // Deserialize and examine results.
            string addressStringResponse = await addressHttpResponse.Content.ReadAsStringAsync();
            var addressResult = JsonConvert.DeserializeObject<Domain.Address>(addressStringResponse);
            Assert.Equal(address.ID, addressResult.ID);
            Assert.Equal(address.Province, addressResult.Province);
            Assert.Equal(address.City, addressResult.City);
            Assert.Equal(address.County, addressResult.County);
        }
    }
}