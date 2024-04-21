﻿using System.Text.Json.Serialization;

namespace _20._Regular_Exam.Models
{
    public class AuthenticationResponse
    {
        [JsonPropertyName("username")]
        public string UserName { get; set; }

        [JsonPropertyName("password")]
        public string Password { get; set; }

        [JsonPropertyName("accessToken")]
        public string AccessToken { get; set; }
    }
}
