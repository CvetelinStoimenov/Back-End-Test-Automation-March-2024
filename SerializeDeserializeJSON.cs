using APITestingWithC_;
using System.Text.Json;
using System.Text.Json.Serialization;

public void SystemTextJson()
{
    //WetherForecast wetherForecast = new WetherForecast()
    //{
    //    Date = DateTime.Now,
    //    TemperatureC = 32,
    //    Suumary = "New Random Test Summary"
    //};

    //// Serialize using System.Text.JSON

    //string wetherForecastJson = JsonSerializer.Serialize(wetherForecast);

    //Console.WriteLine(wetherForecastJson);


    //// Deserialize using System.Text.JSON

    //string jsonString = File.ReadAllText(Path.Combine(Environment.CurrentDirectory + "/../../../WetherForecast.json"));

    //var weatherForecastsObject = JsonSerializer.Deserialize<List<WetherForecast>>(jsonString);

    //foreach (var weatherForecast in weatherForecastsObject)
    //{
    //    Console.WriteLine($"Date: {weatherForecast.Date}, Temperature: {weatherForecast.TemperatureC}, Summary: {weatherForecast.Suumary}");
    //}


    public class WeatherForecastGenerator
{
    public static WeatherForecast CreateRandomWeatherForecast()
    {
        Random random = new Random();

        return new WeatherForecast
        {
            Date = DateTime.Now,
            TemperatureC = random.Next(-20, 40), // Random temperature between -20°C and 40°C
            Summary = "New Random Test Summary"
        };
    }

    public static void PrintWeatherForecastJson(WeatherForecast weatherForecast)
    {
        string weatherForecastJson = JsonSerializer.Serialize(weatherForecast);
        Console.WriteLine(weatherForecastJson);
    }
}
}
