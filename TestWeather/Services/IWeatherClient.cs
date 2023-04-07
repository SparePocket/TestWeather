using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWeather.Model;

namespace TestWeather.Services
{
    public interface IWeatherClient
    {
        Task<Weather> GetCurrentAsync(GeoCoordinate coordinate) ;
    }
}
