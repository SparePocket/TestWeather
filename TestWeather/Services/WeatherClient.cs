using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Http;
using Microsoft.Extensions.Logging;
using TestWeather.Config;
using TestWeather.Model;
using System.Net.Http.Json;


namespace TestWeather.Services
{
    internal class WeatherClient : IWeatherClient
    {
        private readonly HttpClient _httpClient;
        private readonly IWeatherClientConfig _config;
        private readonly ILogger<WeatherClient> _logger;

        public WeatherClient(HttpClient httpClient, IWeatherClientConfig weatherClientConfig, ILogger<WeatherClient> logger)
        {
            _httpClient = httpClient;
            _config = weatherClientConfig;
            _logger = logger;
        }



        public async Task<Weather> GetCurrentAsync(GeoCoordinate coordinate)
        {
            string uri = $"/v1/forecast?latitude={coordinate.Latitude}&longitude={coordinate.Longitude}&hourly=temperature_2m&current_weather=true&temperature_unit=fahrenheit";


            var response = await _httpClient.GetFromJsonAsync<Weather>(uri);
            

            return response;

        }


    }
}
