using System.Text.Json.Serialization;

namespace _20._Regular_Exam.Models
{
    public class ApiResponseDTO
    {
        [JsonPropertyName("msg")]
        public string Message { get; set; }

        [JsonPropertyName("storyId")]
        public string? StoryId { get; set; }
    }
}
