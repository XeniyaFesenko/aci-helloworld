using System.Globalization;
using System.Text.Json.Serialization;
using va_veis_healthdatarepo.Middleware;

namespace va_veis_healthdatarepo.Models.Entities
{
    public class Visit
    {
        [JsonPropertyName("categoryCode")] 
        public string CategoryCode { get; set; }

        [JsonPropertyName("categoryName")] 
        public string CategoryName { get; set; }


        [JsonPropertyName("dateTime")]
        [JsonConverter(typeof(StringConverter))]
        public string DateTime { get; set; }

        [JsonPropertyName("dateTimeDate")]
        public DateTime DateTimeDate { get; set; }


        [JsonPropertyName("facilityCode")]
        [JsonConverter(typeof(StringConverter))]
        public string FacilityCode { get; set; }
        [JsonPropertyName("facilityName")] 
        public string FacilityName { get; set; }


        [JsonPropertyName("localId")]
        [JsonConverter(typeof(StringConverter))]
        public string LocalId { get; set; }
        
        [JsonPropertyName("locationName")] 
        public string LocationName { get; set; }

        [JsonPropertyName("locationUid")] 
        public string LocationUid { get; set; }

        [JsonPropertyName("patientClassCode")] 
        public string PatientClassCode { get; set; }

        [JsonPropertyName("patientClassName")] 
        public string PatientClassName { get; set; }

        [JsonPropertyName("providers")] 
        public List<Provider> Providers { get; set; }

        [JsonPropertyName("stopCodeName")] 
        public string StopCodeName { get; set; }
        
        [JsonPropertyName("stopCodeUid")] 
        public string StopCodeUid { get; set; }
        
        [JsonPropertyName("typeName")] 
        public string TypeName { get; set; }

        [JsonPropertyName("uid")] 
        public string Uid { get; set; }


        [JsonPropertyName("current")]
        [JsonConverter(typeof(StringConverter))]
        public string Current { get; set; }

        [JsonPropertyName("movements")] 
        public List<Movement> Movements { get; set; }

        [JsonPropertyName("reasonName")]
        public string ReasonName { get; set; }

        [JsonPropertyName("service")] 
        public string Service { get; set; }

        [JsonPropertyName("specialty")] 
        public string Specialty { get; set; }

        [JsonPropertyName("stay")] 
        public Stay Stay { get; set; }

        [JsonPropertyName("summary")] 
        public string Summary { get; set; }

        [JsonPropertyName("documents")] 
        public List<Document> Documents { get; set; }
        
        [JsonPropertyName("reasonUid")] 
        public string ReasonUid { get; set; }
        
        [JsonPropertyName("checkOut")] 
        public string CheckOut { get; set; }
    }
    public class Provider
    {
        [JsonPropertyName("primary")]
        [JsonConverter(typeof(StringConverter))]
        public string Primary { get; set; }

        [JsonPropertyName("providerName")] 
        public string ProviderName { get; set; }

        [JsonPropertyName("providerUid")] 
        public string ProviderUid { get; set; }
        
        [JsonPropertyName("role")] 
        public string Role { get; set; }
    }
    public class Movement
    {
        [JsonPropertyName("dateTime")]
        [JsonConverter(typeof(StringConverter))]
        public string DateTime { get; set; }

        [JsonPropertyName("dateTimeDate")]
        public DateTime DateTimeDate { get; set; }

        [JsonPropertyName("localId")]
        [JsonConverter(typeof(StringConverter))]
        public string LocalId { get; set; }

        [JsonPropertyName("locationName")] 
        public string LocationName { get; set; }
        
        [JsonPropertyName("locationUid")] 
        public string LocationUid { get; set; }

        [JsonPropertyName("movementType")] 
        public string MovementType { get; set; }
    }
    public class Stay
    {
        [JsonPropertyName("arrivalDateTime")]
        [JsonConverter(typeof(StringConverter))]
        public string ArrivalDateTime { get; set; }

        [JsonPropertyName("arrivalDateTimeDate")] 
        public DateTime ArrivalDateTimeDate { get; set; }

        [JsonPropertyName("dischargeDateTime")]
        [JsonConverter(typeof(StringConverter))]
        public string DischargeDateTime { get; set; }

        [JsonPropertyName("dischargeDateTimeDate")] 
        public DateTime DischargeDateTimeDate { get; set; }
    }
    public class Document
    {
        [JsonPropertyName("localTitle")] 
        public string LocalTitle { get; set; }

        [JsonPropertyName("uid")] 
        public string Uid { get; set; }
        
        [JsonPropertyName("nationalTitle")] 
        public string NationalTitle { get; set; }
    }
}
