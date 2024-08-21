namespace va_veis_healthdatarepo.Models.Requests
{
    public class PathwaysRequestParams
    {
        public string ClientName { get; set; }
        public string EndDate { get; set; }
        public string NationalId { get; set; }
        public List<string> NationalIds { get; set; }
        public string StartDate { get; set; }
    }
}
