using DeviceManagementAPI;
using DeviceManagementAPI.Controllers;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationTesting
{
    public class IntegrationTest
    {
        protected readonly HttpClient _client;
        
        protected IntegrationTest()
        {
            var appFactory = new WebApplicationFactory<Startup>();
            _client = appFactory.CreateClient();
        }
    }
}
