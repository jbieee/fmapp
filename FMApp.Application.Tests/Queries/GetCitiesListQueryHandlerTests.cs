using FMApp.Application.Weather.Interfaces;
using FMApp.Application.Weather.Queries.GetCitiesList;
using NSubstitute;
using Shouldly;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace FMApp.Application.Tests.Queries
{
    public class GetCitiesListQueryHandlerTests
    {
        class GetCitiesListQueryHandlerBuilder
        {
            IWeatherService _weatherService = Substitute.For<IWeatherService>();

            internal GetCitiesListQueryHandlerBuilder WithWeatherService(IWeatherService weatherService)
            {
                _weatherService = weatherService;
                return this;
            }

            internal GetCitiesListQueryHandler Build()
            {
                return new GetCitiesListQueryHandler(_weatherService);
            }
        }


        [Fact]
        public async Task GetCitiesListTest()
        {
            var builder = new GetCitiesListQueryHandlerBuilder();
            var weatherService = Substitute.For<IWeatherService>();
            // Cannot implicitly convert from Task<List<string>> to Task<IList<string>> so doing it explicitly
            IList<string> resultList = new List<string>
                {
                    "Test1",
                    "Test2"
                };
            weatherService.GetListCitiesAsync()
                .Returns(Task.FromResult(resultList));
            var sut = builder.WithWeatherService(weatherService).Build();

            var result = await sut.Handle(new GetCitiesListQuery(), CancellationToken.None);
            
            result.Count.ShouldBe(2);
        }
    }
}
