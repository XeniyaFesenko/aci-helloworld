using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using va_veis_healthdatarepo.Middleware;
using System.Text;
using System.Text.Json.Serialization;
using va_veis_healthdatarepo.Models.Errors.FPDS;

namespace va_veis_healthdatarepo.Models.Entities
{
    public class FPDSRoot<T>
    {
        [JsonPropertyName("sites")]
        public List<Site<T>> Sites { get; set; }
    }

    public class Params
    {
        [JsonPropertyName("domain")]
        public string Domain { get; set; }

        [JsonPropertyName("systemId")]
        public string SystemId { get; set; }
    }

    public class Data<T>
    {
        [JsonPropertyName("totalItems")]
        public int TotalItems { get; set; }

        [JsonPropertyName("items")]
        public List<T> Items { get; set; }

        [JsonPropertyName("updated")]
        public string Updated { get; set; }
    }

    public class Site<T>
    {
        public Site()
        {
            Params = new Params();
        }

        [JsonPropertyName("apiVersion")]
        public string ApiVersion { get; set; }

        [JsonPropertyName("params")]
        public Params Params { get; set; }

        [JsonPropertyName("data")]
        public Data<T> Data { get; set; }

        public Error Error { get; set; }

        [JsonPropertyName("errorSection")]
        public HDRErrorSection ErrorSection { get; set; }
    }

    public class Error
    {
        public string Message { get; set; }
    }

    public class HDRErrorSection
    {
        public List<HDRErrors> FatalErrors { get; set; }

        public List<HDRErrors> Errors { get; set; }

        public List<HDRErrors> Warnings { get; set; }
    }

    public class HDRErrors
    {
        public string ExceptionMessage { get; set; }

        public string DisplayMessage { get; set; }

        public string Exception { get; set; }

        public string ErrorCode { get; set; }

        public string ErrorId { get; set; }

        public StringBuilder ToStringBuilder()
        {
            return new StringBuilder(string.Format(
                "{{Display Message: '{0}', Exception: '{1}', Error Code: '{2}', Error Id: '{3}'}}", DisplayMessage,
                Exception, ErrorCode, ErrorId));
        }
    }

    public class Clinician
    {
        public string name { get; set; }
        public string role { get; set; }
        public string uid { get; set; }
        public string? signature { get; set; }
        [JsonConverter(typeof(Middleware.StringConverter))]
        public string signedDateTime { get; set; }
    }

    public class Data
    {
        public int totalItems { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<Item> items { get; set; }
        [JsonConverter(typeof(Middleware.StringConverter))]
        public string updated { get; set; }
    }

    public class Item
    {

        public string documentClass { get; set; }

        public string documentTypeCode { get; set; }

        public string documentTypeName { get; set; }

        public string encounterName { get; set; }

        public string encounterUid { get; set; }


        [JsonConverter(typeof(Middleware.StringConverter))]
        public string entered { get; set; }

        [JsonConverter(typeof(Middleware.StringConverter))]
        public string facilityCode { get; set; }

        public string facilityName { get; set; }
        public int? localId { get; set; }

        public string localTitle { get; set; }

        public NationalTitle nationalTitle { get; set; }

        public NationalTitleSubject nationalTitleSubject { get; set; }

        public NationalTitleType nationalTitleType { get; set; }

        [JsonConverter(typeof(Middleware.StringConverter))]
        public string referenceDateTime { get; set; }

        public string statusName { get; set; }

        public List<Text> text { get; set; }

        public string uid { get; set; }
    }

    public class NationalTitleSubject
    {
        public string subject { get; set; }
        public string vuid { get; set; }
    }

    public class Site
    {
        public ErrorSection ErrorSection { get; set; }
        public string apiVersion { get; set; }
        public Params @params { get; set; }
        public Data data { get; set; }
    }

    public class Text
    {
        public List<Clinician> clinicians { get; set; }
        public string content { get; set; }

        [JsonConverter(typeof(Middleware.StringConverter))]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string dateTime { get; set; }
        public string status { get; set; }
        public string uid { get; set; }
    }
}