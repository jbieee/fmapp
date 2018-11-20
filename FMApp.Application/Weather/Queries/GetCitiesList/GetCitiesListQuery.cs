using MediatR;
using System.Collections.Generic;

namespace FMApp.Application.Weather.Queries.GetCitiesList
{
    public class GetCitiesListQuery : IRequest<IList<string>>
    {
    }
}
