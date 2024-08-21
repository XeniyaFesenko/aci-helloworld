namespace va_veis_healthdatarepo.Models.Requests
{
    public class AllergyDataRequest
    {
        public string ClientName { get; set; }

        public string NationalId { get; set; }

        public string StartDate { get; set; }

        public string EndDate { get; set; }
    }
}
