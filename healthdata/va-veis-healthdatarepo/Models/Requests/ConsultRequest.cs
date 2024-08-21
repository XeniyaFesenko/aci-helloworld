using System.Text.Json.Serialization;

namespace va_veis_healthdatarepo.Models.Requests
{
    public class ConsultRequest : BaseRequest
    {
        public string DocumentTypeCode { get; set; } = "CR";

        [JsonRequired()]
        public string NationalID { get; set; }

        public string StartDate { get; set; }

        public string EndDate { get; set; }

        public ConsultRequest() : base() { }
    }
}
