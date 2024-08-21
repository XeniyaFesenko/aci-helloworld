using va_veis_healthdatarepo.Models.Entities;
using va_veis_healthdatarepo.Models.Interfaces;
using va_veis_healthdatarepo.Models.Responses;

namespace va_veis_healthdatarepo.Services.Interfaces
{
    public interface IFPDSDataService<T> where T : new()
    {        
        IErrorEvaluator ErrorEvaluator { get; set; }

        Dictionary<string, string> RequestParameters { get; set; }

        Task<FPDSResponseMessage<T>> GetDataAsync(string clientName, string nationalID, string requestId = null, string startDate = null, string endDate = null);

        Task<FPDSResponseMessage<T>> GetDataByTypeAsync<T>(string clientName, string nationalID, string requestId = null, string startDate = null, string endDate = null);

        Task<FPDSResponseMessage<Flag>> GetFlagDataAsync(string clientName, string nationalID, string requestID);

        Task<FPDSResponseMessage<LabOrders>> GetLabDataAsync(string clientName, string nationalID, string requestId, string startDate, string endDate);
    }
}
