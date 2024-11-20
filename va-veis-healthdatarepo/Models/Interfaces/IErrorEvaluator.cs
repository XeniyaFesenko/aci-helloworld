using va_veis_healthdatarepo.Models.Entities;

namespace va_veis_healthdatarepo.Models.Interfaces
{
    public interface IErrorEvaluator
    {
        bool hasError(HDRErrorSection errors);
    }
}
