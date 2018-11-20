using FMApp.Application.Weather.Interfaces;
using FMApp.Application.Weather.Models;
using FMApp.Application.Weather.Queries.GetCityWeather;
using NSubstitute;
using Shouldly;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace FMApp.Application.Tests.Queries
{
    public class GetCityWeatherQueryHandlerTests
    {
        class GetCityWeatherQueryHandlerBuilder
        {
            IWeatherService _weatherService = Substitute.For<IWeatherService>();

            internal GetCityWeatherQueryHandlerBuilder WithWeatherService(IWeatherService weatherService)
            {
                _weatherService = weatherService;
                return this;
            }

            internal GetCityWeatherQueryHandler Build()
            {
                return new GetCityWeatherQueryHandler(_weatherService);
            }
        }


        [Fact]
        public async Task GetCityWeatherTest()
        {
            var builder = new GetCityWeatherQueryHandlerBuilder();
            var weatherService = Substitute.For<IWeatherService>();
            weatherService.GetWeatherByCityAsync("Test")
                .Returns(Task.FromResult(new WeatherInfo
                {
                    Summary = "Sunny",
                    Temperature = 88.88,
                    UVIndex = 5
                }));
            builder.WithWeatherService(weatherService);
            var sut = builder.Build();

            var result = await sut.Handle(new GetCityWeatherQuery { City = "Test" }, CancellationToken.None);

            result.ShouldBeOfType<WeatherInfoViewModel>();
            result.Summary.ShouldBe("Sunny");
        }
    }
}
