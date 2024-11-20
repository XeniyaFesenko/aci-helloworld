using va_veis_healthdatarepo.Models.Entities;
using va_veis_healthdatarepo.Models.Interfaces;

namespace va_veis_healthdatarepo.Models.Requests
{
    public class FPDSRequestMessageFactory<T> : IFPDSRequestMessageFactory<T> where T : new()
    {
        public FPDSRequestMessageFactory(FPDS_Settings settings) {
            ServiceSettings = settings;
            RequestType = new T();
        }

        public FPDS_Settings ServiceSettings { get; set; }
 
        public T RequestType { get; set; }
    }
}
