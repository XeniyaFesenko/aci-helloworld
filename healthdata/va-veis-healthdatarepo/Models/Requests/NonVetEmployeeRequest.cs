using System.ComponentModel;

namespace va_veis_healthdatarepo.Models.Requests
{
    public class NonVetEmployeeRequest : BaseRequest
    {
        public List<string> NationalIds { get; set; }
    }
}
