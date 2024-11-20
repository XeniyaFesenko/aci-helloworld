using System.Text.Json.Serialization;

namespace va_veis_healthdatarepo.Models.Requests
{
    public class BaseRequest
    {
        public BaseRequest(){}

        public BaseRequest(string clientName)
        {
            ClientName = clientName;
        }

        [JsonRequired()]
        [JsonPropertyOrder(0)]
        public string ClientName { get; set; }
    }
}
