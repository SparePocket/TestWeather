using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Device.Location;
using TestWeather.Model;

namespace TestWeather.Services
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IWeatherClient _weatherClient;

        public Worker(ILogger<Worker> logger, IWeatherClient weatherClient)
        {
            _logger = logger;
            _weatherClient = weatherClient;     
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            GeoCoordinateWatcher geoWatcher = new GeoCoordinateWatcher();
            geoWatcher.Start();
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

                if (geoWatcher.Status == GeoPositionStatus.Ready)
                {
                    _logger.LogInformation($"{geoWatcher.Position.Location.Latitude} {geoWatcher.Position.Location.Longitude}");
                    Weather weather = await _weatherClient.GetCurrentAsync(geoWatcher.Position.Location);
                    _logger.LogInformation($"weather.current_weather.tempature: {weather.current_weather.temperature}{weather.hourly_units.temperature_2m}");
                }
                await Task.Delay(10000, stoppingToken);
            }
        }
    }
}
