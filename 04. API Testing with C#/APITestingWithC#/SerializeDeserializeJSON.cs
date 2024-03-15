using APITestingWithC_;
using System.Text.Json;


public class SerializeDeserializeJSON
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

    // Serialize using System.Text.JSON
    public static void PrintWeatherForecastJson(WeatherForecast weatherForecast)
    {
        string weatherForecastJson = JsonSerializer.Serialize(weatherForecast);
        Console.WriteLine($"Serialize using System.Text.JSON:\n{weatherForecastJson}");
    }


    // Deserialize using System.Text.JSON
    public class WeatherForecastProcessor
    {
        public static List<WeatherForecast> DeserializeAndPrintWeatherForecasts(string filePath)
        {
            string jsonString = File.ReadAllText(filePath);
            var weatherForecasts = JsonSerializer.Deserialize<List<WeatherForecast>>(jsonString);

            foreach (var weatherForecast in weatherForecasts)
            {
                Console.WriteLine($"\nDeserialize using System.Text.JSON:\nDate: {weatherForecast.Date}, Temperature: {weatherForecast.TemperatureC}, Summary: {weatherForecast.Summary}");
            }

            return weatherForecasts;
        }
    }
}
