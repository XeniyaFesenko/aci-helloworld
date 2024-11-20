using System.Text.Json.Serialization;

namespace va_veis_healthdatarepo.Models.Requests
{
    public class FlagsRequest : BaseRequest
    {
        [JsonRequired()]
        public string NationalID { get; set; }

        public FlagsRequest() : base() { }
    }
}
