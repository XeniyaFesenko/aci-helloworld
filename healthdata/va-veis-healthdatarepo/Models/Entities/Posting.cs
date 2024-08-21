using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using va_veis_healthdatarepo.Middleware;

namespace va_veis_healthdatarepo.Models.Entities
{
    public class Posting
    {
        [JsonPropertyName("documentClass")]
        public string DocumentClass { get; set; }

        [JsonPropertyName("documentTypeCode")]
        public string DocumentTypeCode { get; set; }

        [JsonPropertyName("documentTypeName")]
        public string DocumentTypeName { get; set; }

        [JsonPropertyName("encounterName")]
        public string EncounterName { get; set; }

        [JsonPropertyName("encounterUid")]
        public string EncounterUid { get; set; }

        [JsonPropertyName("entered")]
        [JsonConverter(typeof(StringConverter))]
        public string Entered { get; set; }

        [JsonPropertyName("facilityCode")]
        [JsonConverter(typeof(StringConverter))]
        public string FacilityCode { get; set; }

        [JsonPropertyName("facilityName")]
        public string FacilityName { get; set; }

        [JsonPropertyName("localId")]
        [JsonConverter(typeof(StringConverter))]
        public string LocalId { get; set; }

        [JsonPropertyName("localTitle")]
        public string LocalTitle { get; set; }

        [JsonPropertyName("nationalTitle")]
        public NationalTitle NationalTitle { get; set; }

        [JsonPropertyName("nationalTitleRole")]
        public NationalTitleRole NationalTitleRole { get; set; }

        [JsonPropertyName("nationalTitleType")]
        public NationalTitleType NationalTitleType { get; set; }

        [JsonPropertyName("referenceDateTime")]
        [JsonConverter(typeof(StringConverter))]
        public string ReferenceDateTime { get; set; }

        [JsonPropertyName("statusName")]
        public string StatusName { get; set; }

        [JsonPropertyName("text")]
        public List<Text> Text { get; set; }

        [JsonPropertyName("uid")]
        public string Uid { get; set; }
    }
}