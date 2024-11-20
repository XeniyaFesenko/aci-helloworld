using System.Text.Json.Serialization;
using va_veis_healthdatarepo.Middleware;

namespace va_veis_healthdatarepo.Models.Entities
{
    public class Problem
    {
        [JsonPropertyName("entered")]
        [JsonConverter(typeof(StringConverter))]
        public string Entered { get; set; }

        [JsonPropertyName("enteredDate")]
        public DateTime EnteredDate { get; set; }

        [JsonPropertyName("facilityCode")]
        [JsonConverter(typeof(StringConverter))]
        public string FacilityCode { get; set; }

        [JsonPropertyName("facilityName")]
        public string FacilityName { get; set; }

        [JsonPropertyName("icdCode")]
        public string ICDCode { get; set; }

        [JsonPropertyName("icdName")]
        public string ICDName { get; set; }

        [JsonPropertyName("localId")]
        [JsonConverter(typeof(StringConverter))]
        public string LocalId { get; set; }

        [JsonPropertyName("locationName")]
        public string LocationName { get; set; }

        [JsonPropertyName("locationUid")]
        public string LocationUid { get; set; }

        [JsonPropertyName("problemText")]
        public string ProblemText { get; set; }

        [JsonPropertyName("providerName")]
        public string ProviderName { get; set; }

        [JsonPropertyName("providerUid")]
        public string ProviderUid { get; set; }

        [JsonPropertyName("removed")]
        public bool Removed { get; set; }

        [JsonPropertyName("service")]
        public string Service { get; set; }

        [JsonPropertyName("statusCode")]
        public string StatusCode { get; set; }

        [JsonPropertyName("statusName")]
        public string StatusName { get; set; }

        [JsonPropertyName("uid")]
        public string Uid { get; set; }

        [JsonPropertyName("unverified")]
        public bool Unverified { get; set; }

        [JsonPropertyName("updated")]
        [JsonConverter(typeof(StringConverter))]
        public string Updated { get; set; }
    }
}
