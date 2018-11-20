using FMApp.Application.Weather.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FMApp.Application.Weather.Interfaces
{
    public interface IWeatherService
    {
        Task<IList<string>> GetListCitiesAsync();
        Task<WeatherInfo> GetWeatherByCityAsync(string city);
    }
}
