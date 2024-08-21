using System.Net;
using va_veis_healthdatarepo.Models.Interfaces;

namespace va_veis_healthdatarepo.Models.Responses
{
    public abstract class ResponseMessage<T> : IResponseMessage<T>
    {
        public bool ErrorOccurred { get; set; }
        public string ErrorMessage { get; set; }
        public string Status { get; set; }
        public string DebugInfo { get; set; }
        public List<T> Data { get; set; }
        public string WarningMessage { get; set; }

        public IResponseMessage<T> ValidateResponse()
        {
            if (ErrorOccurred)
            {
                throw new WebException($"ErrorMessage: {ErrorMessage}; WarningMessage: {WarningMessage}; DebugInfo: {DebugInfo}", WebExceptionStatus.UnknownError);
            }
            return this;
        }

    }
}
