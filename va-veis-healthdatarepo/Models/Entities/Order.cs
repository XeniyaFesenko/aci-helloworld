using System.Text.Json.Serialization;
using va_veis_healthdatarepo.Middleware;

namespace va_veis_healthdatarepo.Models.Entities
{
    public class Order
    {
        [JsonPropertyName("clinicians")]
        public List<Clinician> Clinicians { get; set; }

        [JsonPropertyName("content")]
        public string Content { get; set; }

        [JsonPropertyName("displayGroup")]
        public string DisplayGroup { get; set; }

        [JsonPropertyName("entered")]
        [System.Text.Json.Serialization.JsonConverter(typeof(StringConverter))]
        public string Entered { get; set; }

        [JsonPropertyName("facilityCode")]
        [System.Text.Json.Serialization.JsonConverter(typeof(StringConverter))]
        public string FacilityCode { get; set; }

        [JsonPropertyName("facilityName")]
        public string FacilityName { get; set; }

        [JsonPropertyName("localId")]
        [System.Text.Json.Serialization.JsonConverter(typeof(StringConverter))]
        public string LocalId { get; set; }

        [JsonPropertyName("locationName")]
        public string LocationName { get; set; }

        [JsonPropertyName("locationUid")]
        public string LocationUid { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("oiCode")]
        public string OiCode { get; set; }

        [JsonPropertyName("oiName")]
        public string OiName { get; set; }

        [JsonPropertyName("oiPackageRef")]
        public string OiPackageRef { get; set; }

        [JsonPropertyName("providerName")]
        public string ProviderName { get; set; }

        [JsonPropertyName("providerUid")]
        public string ProviderUid { get; set; }

        [JsonPropertyName("results")]
        public List<Result> Results { get; set; }

        [JsonPropertyName("service")]
        public string Service { get; set; }

        [JsonPropertyName("start")]
        [System.Text.Json.Serialization.JsonConverter(typeof(StringConverter))]
        public string Start { get; set; }

        [JsonPropertyName("statusCode")]
        public string StatusCode { get; set; }

        [JsonPropertyName("statusName")]
        public string StatusName { get; set; }

        [JsonPropertyName("statusVuid")]
        public string StatusVuid { get; set; }

        [JsonPropertyName("stop")]
        [System.Text.Json.Serialization.JsonConverter(typeof(StringConverter))]
        public string Stop { get; set; }

        [JsonPropertyName("uid")]
        public string Uid { get; set; }
    }

    public class Result
    {
        public string Uid { get; set; }
        public string LocalTitle { get; set; }
        public string NationalTitle { get; set; }
        public string ResultUid { get; set; }
    }
}
