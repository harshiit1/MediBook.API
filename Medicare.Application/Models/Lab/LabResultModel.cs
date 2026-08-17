using System.Text.Json.Serialization;

namespace Medicare.Application.Models.Lab
{
    public class LabResultModel
    {
        public int OrderTestId { get; set; }
        public List<ComponentResultDto> ComponentResults { get; set; }
    }
    public record ComponentResultDto
    {
        [JsonPropertyName("ComponentId")]
        public int ComponentId { get; init; }

        [JsonPropertyName("ResultValue")]
        public string ResultValue { get; init; }

        [JsonPropertyName("Interpretation")]
        public string Interpretation { get; init; }

        [JsonPropertyName("Comments")]
        public string? Comments { get; init; }

        [JsonPropertyName("Status")]
        public string Status { get; init; }
    }

}
