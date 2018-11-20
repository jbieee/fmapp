using FMApp.Application.Weather.Interfaces;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace FMApp.Application.Weather.Queries.GetCitiesList
{
    public class GetCitiesListQueryHandler : IRequestHandler<GetCitiesListQuery, IList<string>>
    {
        private readonly IWeatherService _weatherService;

        public GetCitiesListQueryHandler(IWeatherService weatherService)
        {
            _weatherService = weatherService;
        }

        public async Task<IList<string>> Handle(GetCitiesListQuery request, CancellationToken cancellationToken)
        {
            return await _weatherService.GetListCitiesAsync();
        }
    }
}
