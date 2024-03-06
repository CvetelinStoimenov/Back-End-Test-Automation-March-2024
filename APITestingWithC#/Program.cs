using APITestingWithC_;
using System.Text.Json;
using System.Text.Json.Serialization;

WetherForecast wetherForecast = new WetherForecast()
{
    Date = new DateTime(),
    TemperatureC = 32,
    Suumary = "New Random Test Summary"
};

string wetherForecastJson = JsonSerializer.Serialize(wetherForecast);

Console.WriteLine(wetherForecastJson);