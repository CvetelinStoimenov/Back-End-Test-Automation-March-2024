using APITestingWithC_;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Text.Json;

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

// var person = SerializeDeserializeJSONNet.DeSerializeAndPrintAnonymousTypeJSONNet(filePathPeople);

var client = new RestClient("https://api.github.com");
var request = new RestRequest("/repos/{user}/{repo}/issues/{id}", Method.Get);
request.AddUrlSegment("user", "testnakov");
request.AddUrlSegment("repo", "test-nakov-repo");
request.AddUrlSegment("id", 1);
var response = client.Execute(request);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Content);

var requestDeserialize = new RestRequest("/users/softuni/repos", Method.Get);
var resp = client.Execute(requestDeserialize);

var repos = JsonSerializer.Deserialize<List<Repo>>(resp.Content);

foreach (var repo in repos)
{
    Console.WriteLine(repo.html_url);
    Console.WriteLine(repo.id + " " + repo.full_name + Environment.NewLine);
}


var clientAut = new RestClient(new RestClientOptions("https://api.github.com")
{
    Authenticator = new HttpBasicAuthenticator("CvetelinStoimenov", "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")
});

var requestPost = new RestRequest("/repos/testnakov/test-nakov-repo/issues", Method.Post);
requestPost.AddHeader("Content-Type", "application/json");
requestPost.AddJsonBody(new { title = "Test Stoimenov", body = "This text is posted from C# using ResSharp!" });
var responsePost = clientAut.Execute(requestPost);
Console.WriteLine(responsePost.StatusCode);