using System.Text.Json.Serialization;

namespace va_veis_healthdatarepo.Models.Common
{
    public class User
    {
        [JsonPropertyName("assigningFacility")]
        public string AssigningFacility { get; set; }

        [JsonPropertyName("identity")]
        public string Identity { get; set; }
    }
}
