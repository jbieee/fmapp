namespace FMApp.Weather
{
    public interface ILocationService
    {
        (string Lat, string Long) GetLatLongFromCity(string city);
    }
}
