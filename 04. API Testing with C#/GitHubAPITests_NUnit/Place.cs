using System.Text.Json.Serialization;

namespace GitHubAPITests_NUnit
{
    public class Place
    {
        [JsonPropertyName("place name")]
        public string placeName { get; set; }
        public string longitude { get; set; }
        public string state { get; set; }

        [JsonPropertyName("state abbreviation")]
        public string stateAbbreviation { get; set; }
        public string latitude { get; set; }
    }
}