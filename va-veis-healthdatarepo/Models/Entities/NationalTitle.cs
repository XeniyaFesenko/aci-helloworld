using System.Text.Json.Serialization;

namespace va_veis_healthdatarepo.Models.Entities
{
    public class NationalTitle
    {
        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("vuid")]
        public string Vuid { get; set; }
    }
    public class NationalTitleType
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("vuid")]
        public string Vuid { get; set; }
    }
    public class NationalTitleRole
    {
        [JsonPropertyName("role")]
        public string Role { get; set; }

        [JsonPropertyName("vuid")]
        public string Vuid { get; set; }
    }
}
