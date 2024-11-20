namespace va_veis_healthdatarepo.Models.Interfaces
{
    public interface IResponseMessage<T>
    {
        bool ErrorOccurred { get; set; }
        string ErrorMessage { get; set; }
        string Status { get; set; }
        string DebugInfo { get; set; }
        List<T> Data { get; set; }
        string WarningMessage { get; set; }
    }
}
