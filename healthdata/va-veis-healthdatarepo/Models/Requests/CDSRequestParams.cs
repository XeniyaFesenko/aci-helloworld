namespace va_veis_healthdatarepo.Models.Requests
{
    public class CDSRequestParams
    {
        public CDSRequestParams()
        {
            clientRequestInitiationTime = DateTime.Now.ToString("s");
            timeout = 120;
            startDate = "1957-08-13";
            endDate = "2050-08-13";
        }
        public string clientName { get; set; }
        public string clientRequestInitiationTime { get; set; }
        public Guid messageId { get; set; }
        public string startDate { get; set; }
        public string endDate { get; set; }
        public int timeout { get; set; }
        public string nationalId { get; set; }
        public string users { get; set; }
    }
}
