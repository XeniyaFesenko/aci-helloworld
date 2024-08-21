namespace va_veis_healthdatarepo.Models.Requests
{
    public class ConsultRequestMessage
    {
        public string DocumentTypeCode { get; set; } = "CR";
        public ConsultRequestMessage(string requestId, FPDS_Settings settings) { }
    }
}
