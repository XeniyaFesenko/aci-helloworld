using System.Text.Json.Serialization;

namespace va_veis_healthdatarepo.Models.Requests
{
    public class LabsOrdersRequest : BaseRequest
    {
        public string EndDate { get; set; }

        [JsonRequired()]
        public string NationalID { get; set; }

        public string StartDate { get; set; }

        public LabsOrdersRequest() : base() { }
    }
}
