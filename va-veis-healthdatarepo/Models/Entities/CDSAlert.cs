namespace va_veis_healthdatarepo.Models.Entities
{
    public class CDSAlert
    {
        public CDSUser User { get; set; }
    }

    public class CDSUser
    {
        public string Duz { get; set; }
        public string Name { get; set; }
        public string Facility { get; set; }
        public string LastSignOnDateTime { get; set; }
        public List<CDSUserAlert> Alerts { get; set; }
    }

    public class CDSUserAlert
    {
        public int AlertId { get; set; }
        public string Patient { get; set; }
        public string DateTime { get; set; }
        public string Message { get; set; }
        public string OrderingProvider { get; set; }
        public string Urgency { get; set; }
    }
}
