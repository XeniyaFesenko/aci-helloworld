using System.ComponentModel;
using System.Text.Json.Serialization;

namespace va_veis_healthdatarepo.Models.Requests
{
    public class RadiologyReportsRequest : BaseRequest
    {
        [JsonRequired()]
        public string EndDate { get; set; }

        [JsonRequired()]
        public string NationalID { get; set; }

        [JsonRequired()]
        public string StartDate { get; set; }
    }
}
