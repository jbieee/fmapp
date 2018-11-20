using FMApp.Application.Weather.Interfaces;
using FMApp.Application.Weather.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace FMApp.Weather
{
    public class WeatherService : IWeatherService
    {
        private readonly WeatherApiConfig _apiConfig;
        private readonly IHttpClientFactory _clientFactory;
        private readonly ILocationService _locationService;

        public WeatherService(IOptions<WeatherApiConfig> options, IHttpClientFactory clientFactory, ILocationService locationService)
        {
            _apiConfig = options.Value;
            _clientFactory = clientFactory;
            _locationService = locationService;
        }

        public async Task<IList<string>> GetListCitiesAsync()
        {
            await Task.Delay(0);
            return new List<string> { "Phoenix, AZ", "Raleigh, NC", "Saint John, NB (Canada)", "San Diego, CA" };
        }

        public async Task<WeatherInfo> GetWeatherByCityAsync(string city)
        {
            var (Lat, Long) = _locationService.GetLatLongFromCity(city);
            var request = new HttpRequestMessage(HttpMethod.Get,
                $"https://api.darksky.net/forecast/{_apiConfig.ApiKey}/{Lat},{Long},1530720000");
            var client = _clientFactory.CreateClient();
            var response = await client.SendAsync(request);
            if(response.IsSuccessStatusCode)
            {
                // Typically I would actually create a model and desiarilize into that but for simplicity
                dynamic obj = JsonConvert.DeserializeObject(await response.Content.ReadAsStringAsync());
                var currently = obj.currently;
                return new WeatherInfo
                {
                    Summary = currently.summary,
                    Temperature = currently.temperature,
                    UVIndex = currently.uvIndex
                };
            }
            else
            {
                // in real app would likely create a custom exception in the application layer and throw that here
                throw new Exception($"Request failed: {response.StatusCode} - {response.ReasonPhrase}");
            }
        }
    }
}
