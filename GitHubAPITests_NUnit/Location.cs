using System.Text.Json.Serialization;

namespace GitHubAPITests_NUnit
{
    public class Location
    {
        [JsonPropertyName("post code")]
        public string PostCode { get; set; }

        [JsonPropertyName("country")]
        public string Country { get; set; }

        [JsonPropertyName("country abbreviation")]
        public string CountryAbbreviation { get; set; }

        [JsonPropertyName("place")]
        public List<Place> Place { get; set; }
    }
}