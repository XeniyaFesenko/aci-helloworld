using System.Text.Json.Serialization;
using va_veis_healthdatarepo.Middleware;

namespace va_veis_healthdatarepo.Models.Entities
{
    public class NonVAMed
    {
        [JsonPropertyName("facilityCode")]
        [JsonConverter(typeof(StringConverter))]
        public string FacilityCode { get; set; }

        [JsonPropertyName("facilityName")]
        public string FacilityName { get; set; }

        [JsonPropertyName("localId")]
        public string LocalId { get; set; }

        [JsonPropertyName("medStatus")]
        public string MedStatus { get; set; }

        [JsonPropertyName("medStatusName")]
        public string MedStatusName { get; set; }

        [JsonPropertyName("medType")]
        public string MedType { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("orders")]
        public List<NVAOrder> Orders { get; set; }

        [JsonPropertyName("productFormName")]
        public string ProductFormName { get; set; }

        [JsonPropertyName("qualifiedName")]
        public string QualifiedName { get; set; }
        
        [JsonPropertyName("sig")]
        public string Sig { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }
        
        [JsonPropertyName("uid")]
        public string Uid { get; set; }
        
        [JsonPropertyName("vaStatus")]
        public string VaStatus { get; set; }
        
        [JsonPropertyName("vaType")]
        public string VaType { get; set; }
    }

    public class NVAOrder
    {

        [JsonPropertyName("locationName")]
        public string LocationName { get; set; }

        [JsonPropertyName("locationUid")]
        public string LocationUid { get; set; }

        [JsonPropertyName("orderUid")]
        public string OrderUid { get; set; }

        [JsonPropertyName("ordered")]
        [JsonConverter(typeof(StringConverter))]
        public string Ordered { get; set; }

        [JsonPropertyName("orderedDate")]
        public DateTime OrderedDate { get; set; }

        [JsonPropertyName("providerName")]
        public string ProviderName { get; set; }

        [JsonPropertyName("providerUid")]
        public string ProviderUid { get; set; }
    }
}
