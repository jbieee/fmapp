using System.Collections.Generic;
using System.Threading.Tasks;
using FMApp.Application.Weather.Queries.GetCitiesList;
using FMApp.Application.Weather.Queries.GetCityWeather;
using FMApp.WebUI.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace FMApp.Controllers
{
    public class WeatherController : BaseController
    {
        // GET api/weather/GetCities
        [HttpGet]
        public async Task<ActionResult<IEnumerable<string>>> GetCities()
        {
            return Ok(await Mediator.Send(new GetCitiesListQuery()));
        }

        // GET api/weather/GetWeather/Phoenix, AZ
        [HttpGet("{city}")]
        public async Task<ActionResult<string>> GetWeather(string city)
        {
            return Ok(await Mediator.Send(new GetCityWeatherQuery { City = city }));
        }
    }
}
