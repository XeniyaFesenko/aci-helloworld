using va_veis_healthdatarepo.Models.Requests;

namespace va_veis_healthdatarepo.Models.Interfaces
{
    public interface ICDSRequestMessageFactory<T> where T : new()
    {
        Settings ServiceSettings { get; set; }
        FPDS_Settings FPDS_Settings { get; set; }
        CDSRequestMessage CreateRequestMessage(CDSRequestParams requestParams);
    }
}
