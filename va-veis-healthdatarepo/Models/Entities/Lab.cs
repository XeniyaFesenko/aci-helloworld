using System.Text.Json.Serialization;
using va_veis_healthdatarepo.Middleware;

namespace va_veis_healthdatarepo.Models.Entities
{
    public class Lab
    {
        public Lab()
        {
            //Results = new List<Result>();
        }

        [JsonPropertyName("categoryCode")]
        public string CategoryCode { get; set; }

        [JsonPropertyName("categoryName")]
        public string CategoryName { get; set; }

        [JsonPropertyName("comment")]
        public string Comment { get; set; }

        [JsonPropertyName("displayName")]
        [JsonConverter(typeof(StringConverter))]
        public string DisplayName { get; set; }

        [JsonPropertyName("displayOrder")]
        [JsonConverter(typeof(StringConverter))]
        public string DisplayOrder { get; set; }

        [JsonPropertyName("facilityCode")]
        [JsonConverter(typeof(StringConverter))]
        public string FacilityCode { get; set; }

        [JsonPropertyName("facilityName")]
        public string FacilityName { get; set; }

        [JsonPropertyName("groupName")]
        public string GroupName { get; set; }

        [JsonPropertyName("groupUid")]
        public string GroupUid { get; set; }

        [JsonPropertyName("high")]
        [JsonConverter(typeof(StringConverter))]
        public string High { get; set; }

        [JsonPropertyName("interpretationCode")]
        public string InterpretationCode { get; set; }

        [JsonPropertyName("interpretationName")]
        public string InterpretationName { get; set; }

        [JsonPropertyName("labOrderId")]
        [JsonConverter(typeof(StringConverter))]
        public string LabOrderId { get; set; }

        [JsonPropertyName("localId")]
        [JsonConverter(typeof(StringConverter))]
        public string LocalId { get; set; }

        [JsonPropertyName("low")]
        [JsonConverter(typeof(StringConverter))]
        public string Low { get; set; }

        [JsonPropertyName("observed")]
        [JsonConverter(typeof(StringConverter))]
        public string Observed { get; set; }

        [JsonPropertyName("orderUid")]
        public string OrderUid { get; set; }

        [JsonPropertyName("result")]
        [JsonConverter(typeof(StringConverter))]
        public string Result { get; set; }

        [JsonPropertyName("resulted")]
        [JsonConverter(typeof(StringConverter))]
        public string Resulted { get; set; }

        [JsonPropertyName("sample")]
        public string Sample { get; set; }

        [JsonPropertyName("specimen")]
        public string Specimen { get; set; }

        [JsonPropertyName("statusCode")]
        public string StatusCode { get; set; }

        [JsonPropertyName("statusName")]
        public string StatusName { get; set; }

        [JsonPropertyName("typeCode")]
        public string TypeCode { get; set; }

        [JsonPropertyName("typeId")]
        [JsonConverter(typeof(StringConverter))]
        public string TypeId { get; set; }

        [JsonPropertyName("typeName")]
        public string TypeName { get; set; }

        [JsonPropertyName("uid")]
        public string Uid { get; set; }

        [JsonPropertyName("units")]
        public string Units { get; set; }

        [JsonPropertyName("vuid")]
        public string Vuid { get; set; }
    }

    public class LabOrders
    {
        public List<Lab> Labs { get; set; }
        public List<Order> Orders { get; set; }
        public LabOrders() { }
    }
}