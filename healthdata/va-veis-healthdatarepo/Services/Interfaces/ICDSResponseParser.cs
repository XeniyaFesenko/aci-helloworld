using va_veis_healthdatarepo.Models.Entities;

namespace va_veis_healthdatarepo.Services.Interfaces
{
    public interface ICDSResponseParser
    {
        List<CDSAllergy> ParseAllergyFromXMLResponse(string dataRawXMLResponse);
    }
}
