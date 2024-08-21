using System.Globalization;
using System.Text.Json.Serialization;
using va_veis_healthdatarepo.Middleware;

namespace va_veis_healthdatarepo.Models.Entities
{
    public class Vital
    {
        private string _dn;
        private string _obs;

        [JsonPropertyName("displayName")]
        public string DisplayName
        {
            get { return _dn; }
            set
            {
                _dn = "";
                if (!string.IsNullOrEmpty(value))
                    _dn = value.ToUpper();
            }
        }

        [JsonPropertyName("facilityCode")]
        [JsonConverter(typeof(StringConverter))]
        public string FacilityCode { get; set; }
        
        [JsonPropertyName("facilityName")] 
        public string FacilityName { get; set; }


        [JsonPropertyName("high")]
        [JsonConverter(typeof(StringConverter))]
        public string High { get; set; }
        
        [JsonPropertyName("kind")] 
        public string Kind { get; set; }


        [JsonPropertyName("localId")]
        [JsonConverter(typeof(StringConverter))]
        public string LocalId { get; set; }

        [JsonPropertyName("locationName")]
        [JsonConverter(typeof(StringConverter))]
        public string LocationName { get; set; }
        
        [JsonPropertyName("locationUid")] 
        public string LocationUid { get; set; }


        [JsonPropertyName("low")]
        [JsonConverter(typeof(StringConverter))]
        public string Low { get; set; }

        [JsonPropertyName("observed")]
        [JsonConverter(typeof(StringConverter))]
        public string Observed
        {
            get { return _obs; }
            set
            {
                _obs = value;
                if (string.IsNullOrEmpty(_obs)) return;
                DateTime parsedDate;
                //20171103094211
                //yyyyMMddHHmm
                var parsedOK = DateTime.TryParseExact(_obs,
                    "yyyyMMddHHmm",
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None,
                    out parsedDate);
                if (parsedOK)
                {
                    ObservedDate = parsedDate;
                }
                var parsedOK2 = DateTime.TryParseExact(_obs,
                    "yyyyMMddHHmmss",
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None,
                    out parsedDate);
                if (parsedOK2)
                {
                    ObservedDate = parsedDate;
                }
            }
        }
        [JsonPropertyName("observedDate")]
        public DateTime ObservedDate { get; set; }

        [JsonPropertyName("result")]
        [JsonConverter(typeof(StringConverter))]
        public string Result { get; set; }

        [JsonPropertyName("resulted")]
        [JsonConverter(typeof(StringConverter))]
        public string Resulted { get; set; }

        [JsonPropertyName("summary")] 
        public string Summary { get; set; }

        [JsonPropertyName("typeCode")] 
        public string TypeCode { get; set; }

        [JsonPropertyName("typeName")] 
        public string TypeName { get; set; }

        [JsonPropertyName("uid")] 
        public string Uid { get; set; }

        [JsonPropertyName("units")] 
        public string Units { get; set; }


        [JsonPropertyName("metricResult")]
        [JsonConverter(typeof(StringConverter))]
        public string MetricResult { get; set; }
        
        [JsonPropertyName("metricUnits")] 
        public string MetricUnits { get; set; }

        [JsonPropertyName("qualifiers")] 
        public List<Qualifier> Qualifiers { get; set; }
    }
    public class Qualifier
    {
        [JsonPropertyName("name")] 
        public string Name { get; set; }

        [JsonPropertyName("vuid")]
        [JsonConverter(typeof(StringConverter))]
        public string VuId { get; set; }
    }
}
