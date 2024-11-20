using va_veis_healthdatarepo.Models.Requests;

namespace va_veis_healthdatarepo.Models.Interfaces
{
    public interface IPWSRrequestMessageFactory<T> where T : new()
    {
        PathwaySettings ServiceSettings { get; set; }
        PathwaysRequestMessage CreateRequestMessage(PathwaysRequestParams requestParams);
    }
}
