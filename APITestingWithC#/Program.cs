using APITestingWithC_;
using System.Text.Json;
using System.Text.Json.Serialization;

WetherForecast wetherForecast = new WetherForecast()
{
    Date = DateTime.Now,
    TemperatureC = 32,
    Suumary = "New Random Test Summary"
};

// Serialize

string wetherForecastJson = JsonSerializer.Serialize(wetherForecast);

Console.WriteLine(wetherForecastJson);


// Deserialize

string jsonString = File.ReadAllText(Path.Combine(Environment.CurrentDirectory + "/../../../WetherForecast.json"));

var weatherForecastsObject = JsonSerializer.Deserialize<List<WetherForecast>>(jsonString);

foreach (var weatherForecast in weatherForecastsObject)
{
    Console.WriteLine($"Date: {weatherForecast.Date}, Temperature: {weatherForecast.TemperatureC}, Summary: {weatherForecast.Suumary}");
}