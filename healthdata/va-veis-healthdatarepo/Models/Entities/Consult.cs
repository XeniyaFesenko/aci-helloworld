using System.Text.Json.Serialization;
using va_veis_healthdatarepo.Middleware;

namespace va_veis_healthdatarepo.Models.Entities
{
    public class Consult
    {
        [JsonPropertyName("dateTimeDate")]
        public DateTime DateTimeDate { get; set; }

        [JsonPropertyName("dateTime")]
        [JsonConverter(typeof(StringConverter))]
        public string DateTime { get; set; }

        [JsonPropertyName("providerName")] 
        public string ProviderName { get; set; }

        [JsonPropertyName("providerUid")] 
        public string ProviderUid { get; set; }

        [JsonPropertyName("category")] 
        public string Category { get; set; }

        [JsonPropertyName("encounterUid")] 
        public string EncounterUid { get; set; }

        [JsonPropertyName("interpretation")] 
        public string Interpretation { get; set; }

        [JsonPropertyName("facilityCode")]
        [JsonConverter(typeof(StringConverter))]
        public string FacilityCode { get; set; }

        [JsonPropertyName("facilityName")] 
        public string FacilityName { get; set; }

        [JsonPropertyName("localId")] 
        public int LocalId { get; set; }
        
        [JsonPropertyName("orderName")] 
        public string OrderName { get; set; }
        
        [JsonPropertyName("orderUid")] 
        public string OrderUid { get; set; }
        
        [JsonPropertyName("service")] 
        public string Service { get; set; }

        [JsonPropertyName("uid")] 
        public string Uid { get; set; }

        [JsonPropertyName("statusName")] 
        public string StatusName { get; set; }

        [JsonPropertyName("typeName")] 
        public string TypeName { get; set; }

        [JsonPropertyName("consultProcedure")] 
        public string ConsultProcedure { get; set; }

        [JsonPropertyName("localTitle")] 
        public string LocalTitle { get; set; }

        [JsonPropertyName("nationalTitle")] 
        public string NationalTitle { get; set; }
        
        [JsonPropertyName("results")] 
        public List<ConsultResult> Results { get; set; }

        [JsonPropertyName("provisionalDx")]
        public ProvisionalDx ProvisionalDx { get; set; }
    }

    public class ConsultResult
    {
        [JsonPropertyName("localTitle")] 
        public string LocalTitle { get; set; }

        [JsonPropertyName("uid")] 
        public string Uid { get; set; }
    }

    public class ProvisionalDx
    {
        [JsonPropertyName("code")]
        [JsonConverter(typeof(StringConverter))]
        public string Code { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("system")]
        public string System { get; set; }
    }
}
