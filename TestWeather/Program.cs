using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TestWeather.Services;
using TestWeather.Config;
using Microsoft.Extensions.Configuration;

namespace TestWeather
{
    internal class Program
    {

        static async Task Main(string[] args)
        {


            IHost host = Host.CreateDefaultBuilder(args)
            .ConfigureHostConfiguration(configuration =>
            {
                configuration.AddJsonFile("appSettings.json", optional: false);
                configuration.Build();
            }
            )
            .ConfigureServices(services =>
            {
                

                services.AddHostedService<Worker>();
                services.AddSingleton<IWeatherClientConfig, WeatherClientConfig>();
                services.AddHttpClient<IWeatherClient,WeatherClient>()
                    .ConfigureHttpClient((serviceProvider,httpClient) =>
                    {
                        var clientConfig = serviceProvider.GetRequiredService<IWeatherClientConfig>();
                        httpClient.BaseAddress = clientConfig.BaseUrl;
                        httpClient.Timeout = TimeSpan.FromSeconds(clientConfig.Timeout);
                       
                    });
            })
            .Build();

            await host.RunAsync();

        }
    }
}
