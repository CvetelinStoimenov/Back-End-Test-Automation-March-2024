using APITestingWithC_;
using Newtonsoft.Json;


public class SerializeDeserializeJSONNet
{
    // Serialize using JSON.Net
    public static void SerializeDeSerializeJSONNet(WeatherForecast weatherForecast)
    {
        string weatherForecastJson = JsonConvert.SerializeObject(weatherForecast, Formatting.Indented);
        Console.WriteLine($"\nSerialize using JSON.Net formated:\n{weatherForecastJson}");
    }


    // Deserialize using JSON.Net
    public class WeatherForecastProcessorJSONNet
    {
        public static List<WeatherForecast> DeserializeAndPrintWeatherForecastsJSONNet(string filePathWeather)
        {
            string jsonString = File.ReadAllText(filePathWeather);
            var weatherForecastsJSONNet = JsonConvert.DeserializeObject<List<WeatherForecast>>(jsonString);

            foreach (var weatherForecast in weatherForecastsJSONNet)
            {
                Console.WriteLine($"\nDeserialize using JSON.Net:\nDate: {weatherForecast.Date}, Temperature: {weatherForecast.TemperatureC}, Summary: {weatherForecast.Summary}");
            }

            return weatherForecastsJSONNet;
        }
    }

    //public static List<object> DeSerializeAndPrintAnonymousTypeJSONNet(string filePath)
    //{
    //    // Read the JSON content from the file
    //    string jsonString = File.ReadAllText(filePath);

    //    // Deserialize the JSON into a list of objects
    //    List<object> persons = JsonConvert.DeserializeObject<List<object>>(jsonString);

    //    // Print the deserialized data
    //    foreach (var person in persons)
    //    {
    //        var properties = person as Dictionary<string, object>;
    //        Console.WriteLine($"\nDeserialize using JSON.Net:");
    //        foreach (var property in properties)
    //        {
    //            Console.WriteLine($"{property.Key}: {property.Value}");
    //        }
    //    }

    //    return persons;
    //}
}

