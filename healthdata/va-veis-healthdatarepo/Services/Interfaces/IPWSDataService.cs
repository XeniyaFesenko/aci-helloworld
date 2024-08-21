using va_veis_healthdatarepo.Models.Entities;
using va_veis_healthdatarepo.Models.Responses;

namespace va_veis_healthdatarepo.Services.Interfaces
{
    public interface IPWSDataService<T>
    {
        Task<PWSResponseMessage<PWSAppointment>> GetAppointmentsAsync(string clientName, string nationalID, string startDate, string endDate);

        Task<PWSResponseMessage<NonVeteranEmployeeData>> GetNonVetRecordAsync(string clientName, List<string> nationalIds);
    }
}
