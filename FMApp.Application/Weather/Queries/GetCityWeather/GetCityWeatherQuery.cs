using MediatR;

namespace FMApp.Application.Weather.Queries.GetCityWeather
{
    public class GetCityWeatherQuery : IRequest<WeatherInfoViewModel>
    {
        public string City { get; set; }
    }
}
