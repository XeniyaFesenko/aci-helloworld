using System.Collections;
using System.Text.Json.Serialization;
using va_veis_healthdatarepo.Middleware;
using va_veis_healthdatarepo.Models.Errors.FPDS;

namespace va_veis_healthdatarepo.Models.Entities
{
    public class ApprovedBy
    {
        [JsonConverter(typeof(StringConverter))]
        [JsonPropertyName("code")]
        public string Code { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }
    }

    public class Assigned
    {
        [JsonPropertyName("value")]
        public double Value { get; set; }
    }

    public class Category
    {
        [JsonPropertyName("value")]
        public string Value { get; set; }
    }

    public class Content
    {
        [JsonPropertyName("xml:space")]
        public string XmlSpace { get; set; }

        [JsonPropertyName("content")]
        public string Value { get; set; }
    }

    public class Flags
    {
        [JsonPropertyName("total")]
        public int Total { get; set; }

        [JsonPropertyName("flag")]
        [JsonConverter(typeof(SingleOrArrayConverter))]
        public List<FPDSFlag> Flag { get; set; }
    }

    public class FlagsRoot
    {
        [JsonPropertyName("sites")]
        public List<FPDSFlagSite> Sites { get; set; }
    }

    public class FPDSFlag
    {
        [JsonPropertyName("id")]
        public Id Id { get; set; }

        [JsonPropertyName("content")]
        public Content Content { get; set; }

        [JsonPropertyName("ownSite")]
        public OwnSite OwnSite { get; set; }

        [JsonPropertyName("category")]
        public Category Category { get; set; }

        [JsonPropertyName("name")]
        public Name Name { get; set; }

        [JsonPropertyName("reviewDue")]
        public ReviewDue ReviewDue { get; set; }

        [JsonPropertyName("origSite")]
        public OrigSite OrigSite { get; set; }

        [JsonPropertyName("assigned")]
        public Assigned Assigned { get; set; }

        [JsonPropertyName("approvedBy")]
        public ApprovedBy ApprovedBy { get; set; }

        [JsonPropertyName("type")]
        public Type Type { get; set; }
    }

    public class Id
    {
        [JsonPropertyName("value")]
        public string Value { get; set; }
    }

    public class Name
    {
        [JsonPropertyName("value")]
        public string Value { get; set; }
    }

    public class OrigSite
    {
        [JsonPropertyName("code")]
        [JsonConverter(typeof(StringConverter))]
        public string Code { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }
    }

    public class OwnSite
    {
        [JsonPropertyName("code")]
        [JsonConverter(typeof(StringConverter))]
        public string Code { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }
    }

    public class Results
    {
        [JsonPropertyName("flags")]
        public Flags Flags { get; set; }

        [JsonPropertyName("timeZone")]
        public string TimeZone { get; set; }

        [JsonPropertyName("version")]
        public double Version { get; set; }
    }

    public class ReviewDue
    {
        [JsonPropertyName("value")]
        public int Value { get; set; }
    }

    public class FPDSFlagSite
    {
        public ErrorSection ErrorSection { get; set; }

        [JsonPropertyName("results")]
        public Results Results { get; set; }
    }

    public class Type
    {
        [JsonPropertyName("value")]
        public string Value { get; set; }
    }
}
