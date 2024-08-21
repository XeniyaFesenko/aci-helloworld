namespace va_veis_healthdatarepo.Models.Errors.FPDS
{
    public class FPDSError
    {
    }

    public class ErrorSection {
        public List<FatalError> FatalErrors { get; set; }
    }

    public class FatalError
    {
        public string DisplayMessage { get; set; }

        public string ErrorCode { get; set; }

        public string ErrorId { get; set; }

        public string Exception { get; set; }

        public string ExceptionMessage { get; set; }

        public override string ToString()
        {
            return $"ErrorCode: {ErrorCode}; ErrorId: {ErrorId}; Exception: {Exception}; DisplayMessage: {DisplayMessage}";
        }
    }
}
