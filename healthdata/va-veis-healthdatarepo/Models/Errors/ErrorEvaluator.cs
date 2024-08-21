using va_veis_healthdatarepo.Models.Entities;
using va_veis_healthdatarepo.Models.Interfaces;

namespace va_veis_healthdatarepo.Models.Errors
{
    public class ErrorEvaluator : IErrorEvaluator
    {
        #region IErrorEvaluator Members

        public bool hasError(HDRErrorSection errors)
        {
            return false;
        }

        #endregion
    }
}
