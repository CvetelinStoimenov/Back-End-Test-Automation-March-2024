using System.Text.Json.Serialization;

namespace GitHubAPITests_NUnit
{
    public class Location
    {
        [JsonPropertyName("post code")]
        public string postCode { get; set; }

        [JsonPropertyName("country")]
        public string country { get; set; }

        [JsonPropertyName("country abbreviation")]
        public string countryAbbreviation { get; set; }

        [JsonPropertyName("places")]
        public List<Place> places { get; set; }
    }
}