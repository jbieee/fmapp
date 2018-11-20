using System;

namespace FMApp.Weather
{
    public class LocationServiceDummyImpl : ILocationService
    {
        public (string Lat, string Long) GetLatLongFromCity(string city)
        {
            switch (city)
            {
                case "Phoenix, AZ":
                    return ("33.6056711", "-112.4052323");
                case "Raleigh, NC":
                    return ("35.8438868", "-78.7150956");
                case "Saint John, NB (Canada)":
                    return ("45.1104341", "-66.3220243");
                case "San Diego, CA":
                    return ("32.8248175", "-117.38916");
            }
            throw new NotSupportedException("Invalid city provided");
        }
    }
}
