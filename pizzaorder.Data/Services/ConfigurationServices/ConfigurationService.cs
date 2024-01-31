using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace PizzaOrderAPI.Data.Services.ConfigurationServices
{


    public class ConfigurationService : IConfigurationService
    {
        private readonly IConfiguration _configuration;

        public ConfigurationService()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath("C:\\Users\\Dell\\Desktop\\PizzaOrderAPI\\PizzaOrderAPI\\pizzaorder.Data")
                .AddJsonFile("appsettings.json");

            _configuration = builder.Build();
        }

        public string? GetMyConnectionString()
        {
            string? connection = _configuration["Connection"];
            if(connection != null)
            {
                return connection;
            }
            return null;
        }
    }
}
