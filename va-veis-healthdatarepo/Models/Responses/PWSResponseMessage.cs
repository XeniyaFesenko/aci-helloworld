namespace va_veis_healthdatarepo.Models.Responses
{
    public class PWSResponseMessage<T> : ResponseMessage<T>
    {
        public PWSResponseMessage()
        {
            ErrorOccurred = false;
            Status = "OK";
            Data = new List<T>();
        }
    }
}
