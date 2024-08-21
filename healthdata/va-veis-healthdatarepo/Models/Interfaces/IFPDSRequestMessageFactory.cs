using va_veis_healthdatarepo.Models.Requests;

namespace va_veis_healthdatarepo.Models.Interfaces
{
    public interface IFPDSRequestMessageFactory<T> where T : new()
    {
        FPDS_Settings ServiceSettings { get; set; }
        //FPDSRequestMessage CreateRequestMessage(FPDSRequestParams requestParams);
    }
}
