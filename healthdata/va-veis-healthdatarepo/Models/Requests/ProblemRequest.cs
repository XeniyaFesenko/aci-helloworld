using System.Text.Json.Serialization;

namespace va_veis_healthdatarepo.Models.Requests
{
    public class ProblemRequest : BaseRequest
    {
        [JsonRequired()]
        public string NationalID { get; set; }
    }
}
