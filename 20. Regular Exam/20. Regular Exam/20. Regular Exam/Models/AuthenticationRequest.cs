using System.Text.Json.Serialization;


namespace _20._Regular_Exam.Models
{
    public class AuthenticationRequest
    {
            [JsonPropertyName("userName")]
            public string UserName { get; set; }

            [JsonPropertyName("password")]
            public string Password { get; set; }
    }
}
