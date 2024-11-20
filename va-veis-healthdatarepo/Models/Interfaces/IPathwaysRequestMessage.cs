using va_veis_healthdatarepo.Models;

namespace va_veis_healthdatarepo.Models.Interfaces
{
    public interface IPathwaysRequestMessage
    {
        PathwaySettings ServiceSettings { get; set; }
        string BuildSoapRequestMessage();
    }
}
