namespace va_veis_healthdatarepo.Models.Responses
{
    public class CDSResponseMessage<T> : ResponseMessage<T>
    {
        public CDSResponseMessage()
        {
            ErrorOccurred = false;
            Status = "OK";
            Data = new List<T>();
        }
    }
}
