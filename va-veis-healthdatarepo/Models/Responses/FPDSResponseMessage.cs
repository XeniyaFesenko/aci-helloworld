namespace va_veis_healthdatarepo.Models.Responses
{
    public class FPDSResponseMessage<T> : ResponseMessage<T>
    {
        public FPDSResponseMessage()
        {
            ErrorOccurred = false;
            WarningMessage = null;
            Status = "OK";
            Data = new List<T>();
        }
    }
}
