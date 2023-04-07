using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestWeather.Config
{
    public interface IWeatherClientConfig
    {
        Uri BaseUrl { get; set; }
        int Timeout { get; set; }

    }
}
