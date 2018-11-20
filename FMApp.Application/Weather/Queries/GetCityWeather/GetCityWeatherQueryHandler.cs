using FMApp.Application.Weather.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FMApp.Application.Weather.Queries.GetCityWeather
{
    public class GetCityWeatherQueryHandler : IRequestHandler<GetCityWeatherQuery, WeatherInfoViewModel>
    {
        private readonly IWeatherService _weatherService;

        public GetCityWeatherQueryHandler(IWeatherService weatherService)
        {
            _weatherService = weatherService;
        }

        public async Task<WeatherInfoViewModel> Handle(GetCityWeatherQuery request, CancellationToken cancellationToken)
        {
            var response = await _weatherService.GetWeatherByCityAsync(request.City);
            // In this contrived example the mapping is 1-1 but in real life you usually need to massage the response to fit your view
            // this also provides a wonderful opportunity to apply some view specific business logic so the presentation layer doesn't have to
            return new WeatherInfoViewModel
            {
                Summary = response.Summary,
                Temperature = response.Temperature,
                UVIndex = response.UVIndex
            };
        }
    }
}
