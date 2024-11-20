using System.ComponentModel;
using System.Text.Json.Serialization;

namespace va_veis_healthdatarepo.Models.Requests
{
    public class AppointmentRequest : BaseRequest
    {
        public string EndDate { get; set; }

        [JsonRequired()]
        public string NationalID { get; set; }

        [DefaultValue(false)]
        public bool NoFilter { get; set; }

        public string StartDate { get; set; }
    }
}
