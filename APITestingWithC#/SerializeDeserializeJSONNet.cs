using Newtonsoft.Json;

namespace APITestingWithC_
{
    internal class SerializeDeserializeJSONNet
    {
        // Serialize using JSON.Net
        public static void SerializeDeSerializeJSONNet(WeatherForecast weatherForecast) 
        {
                string weatherForecastJson = JsonConvert.SerializeObject(weatherForecast);
                Console.WriteLine($"\nSerialize using JSON.Net:\n{weatherForecastJson}");
        }
    }
}
