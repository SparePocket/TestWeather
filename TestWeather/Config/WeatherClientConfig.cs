using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestWeather.Config
{
    public class WeatherClientConfig : IWeatherClientConfig
    {
        public WeatherClientConfig(IConfiguration configuration)
        {
            if (configuration == null) throw new ArgumentNullException(nameof(configuration));
            configuration.Bind("WeatherClient", this);
        }
        public Uri BaseUrl { get ; set ;  }
        public int Timeout { get ; set ; }

    }
}
