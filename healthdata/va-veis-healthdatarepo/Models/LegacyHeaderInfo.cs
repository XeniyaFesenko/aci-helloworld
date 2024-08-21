using System.Runtime.Serialization;

namespace va_veis_healthdatarepo.Models
{
    [DataContract]
    public class LegacyHeaderInfo
    {
        [DataMember]
        public string StationNumber { get; set; }
        [DataMember]
        public string LoginName { get; set; }
        [DataMember]
        public string ApplicationName { get; set; }
        [DataMember]
        public string ClientMachine { get; set; }
        [DataMember]
        public string Password { get; set; }
    }
}
