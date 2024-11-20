using va_veis_healthdatarepo.Models.Entities;
using va_veis_healthdatarepo.Models.Responses;

namespace va_veis_healthdatarepo.Services.Interfaces
{
    public interface ICDSDataService<T> where T : new()
    {
        Task<CDSResponseMessage<CDSAlert>> GetAlertDataAsync(string clientName, Guid messageID, string users);

        Task<CDSResponseMessage<CDSAllergy>> GetAllergyDataAsync(string clientName, string nationalID, string startDate = null, string endDate = null);
    }
}
