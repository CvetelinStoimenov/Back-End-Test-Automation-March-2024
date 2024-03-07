using APITestingWithC_;

// Call the method to generate a WeatherForecast object
WeatherForecast weatherForecast = SerializeDeserializeJSON.CreateRandomWeatherForecast();

// Print the WeatherForecast object as JSON using System.Text.JSON
SerializeDeserializeJSON.PrintWeatherForecastJson(weatherForecast);

// Combine the file path for the JSON file
string filePath = Path.Combine(Environment.CurrentDirectory, "../../../WeatherForecast.json");

// Deserialize the weather forecasts from the JSON file and print them using System.Text.JSON
List<WeatherForecast> weatherForecasts = SerializeDeserializeJSON.WeatherForecastProcessor.DeserializeAndPrintWeatherForecasts(filePath);


// Print the WeatherForecast object as JSON using JSON.Net
SerializeDeserializeJSONNet.SerializeDeSerializeJSONNet(weatherForecast);