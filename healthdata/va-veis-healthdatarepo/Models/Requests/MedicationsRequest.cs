using System.ComponentModel;
using System.Text.Json.Serialization;

namespace va_veis_healthdatarepo.Models.Requests
{
    public class MedicationsRequest : BaseRequest
    {
        [JsonRequired()]
        public string NationalID { get; set; }

        public string StartDate { get; set; }

        public string EndDate { get; set; }

        public MedicationsRequest() : base() { }
    }
}
