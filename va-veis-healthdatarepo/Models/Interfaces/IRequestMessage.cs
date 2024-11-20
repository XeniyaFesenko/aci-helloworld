namespace va_veis_healthdatarepo.Models.Interfaces
{
    public interface IRequestMessage
    {
        string BaseUrl { get; set; }
        string ContentType { get; set; }
        string Resource { get; set; }
        List<KeyValuePair<string, string>> URLParameters { get; set; }
        bool IsGetRequest { get; set; }
        bool IsPostRequest { get; set; }
        string Body { get; set; }
    }
}
