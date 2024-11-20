using System.Text.Json.Serialization;
using va_veis_healthdatarepo.Models.Common;

namespace va_veis_healthdatarepo.Models.Requests
{
    public class AlertsRequest
    {
        [JsonRequired()]
        public string ClientName { get; set; }

        public Guid MessageId { get; set; }

        [JsonRequired()]
        public List<User> Users { get; set; }
    }
}
