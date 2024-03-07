using APITestingWithC_;
using System;

// Call the method to generate a WeatherForecast object
WeatherForecast weatherForecast = SerializeDeserializeJSON.CreateRandomWeatherForecast();

// Print the WeatherForecast object as JSON using System.Text.JSON
SerializeDeserializeJSON.PrintWeatherForecastJson(weatherForecast);

// Combine the file path for the JSON file
string filePathWeather = Path.Combine(Environment.CurrentDirectory, "../../../WeatherForecast.json");
string filePathPeople = Path.Combine(Environment.CurrentDirectory, "../../../People.json");

// Deserialize the weather forecasts from the JSON file and print them using System.Text.JSON
List<WeatherForecast> weatherForecasts = SerializeDeserializeJSON.WeatherForecastProcessor.DeserializeAndPrintWeatherForecasts(filePathWeather);


// Print the WeatherForecast object as JSON using JSON.Net
SerializeDeserializeJSONNet.SerializeDeSerializeJSONNet(weatherForecast);

// Deserialize the weather forecasts from the JSON file and print them using JSON.Net
List<WeatherForecast> weatherForecastsJSONNet = SerializeDeserializeJSONNet.WeatherForecastProcessorJSONNet.DeserializeAndPrintWeatherForecastsJSONNet(filePathWeather);

//var person = SerializeDeserializeJSONNet.DeSerializeAndPrintAnonymousTypeJSONNet(filePathPeople);