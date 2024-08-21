using System.Xml.Serialization;
using va_veis_healthdatarepo.Models.Errors.CDS;

namespace va_veis_healthdatarepo.Models.Entities
{
    [XmlRoot(ElementName = "alert")]
    public class Alert
    {
        [XmlElement(ElementName = "alertId")]
        public int AlertId { get; set; }

        [XmlElement(ElementName = "patient")]
        public string Patient { get; set; }

        [XmlElement(ElementName = "dateTime")]
        public double DateTime { get; set; }

        [XmlElement(ElementName = "message")]
        public string Message { get; set; }

        [XmlElement(ElementName = "orderingProvider")]
        public string OrderingProvider { get; set; }

        [XmlElement(ElementName = "urgency")]
        public string Urgency { get; set; }
    }

    [XmlRoot(ElementName = "alerts")]
    public class Alerts
    {
        [XmlElement(ElementName = "alert")]
        public List<Alert> Alert { get; set; }
    }

    [XmlRoot(ElementName = "user")]
    public class AlertUser
    {
        [XmlElement(ElementName = "duz")]
        public int Duz { get; set; }

        [XmlElement(ElementName = "name")]
        public string? Name { get; set; }

        [XmlElement(ElementName = "lastSignOnDateTime")]
        public double LastSignOnDateTime { get; set; }

        [XmlElement(ElementName = "facility")]
        public int Facility { get; set; }

        [XmlElement(ElementName = "alerts")]
        public Alerts Alerts { get; set; }
    }

    [XmlRoot(ElementName = "users")]
    public class AlertUsers
    {
        [XmlElement(ElementName = "user")]
        public List<AlertUser> AlertUser { get; set; }
    }

    [XmlRoot(ElementName = "UserAlertRead")]
    public class UserAlertRead
    {
        [XmlElement(ElementName = "users")]
        public AlertUsers AlertUsers { get; set; }
    }

    [XmlRoot(ElementName = "ClinicalData")]
    public class ClinicData
    {
        [XmlElement(ElementName = "errorSection")]
        public ErrorSection ErrorSection { get; set; }

        [XmlElement(ElementName = "templateId")]
        public string TemplateId { get; set; }

        [XmlElement(ElementName = "requestId")]
        public string RequestId { get; set; }

        [XmlElement(ElementName = "UserAlertRead")]
        public UserAlertRead UserAlertRead { get; set; }
    }

    //[XmlRoot(ElementName = "warning")]
    //public class Warning
    //{
    //    [XmlElement(ElementName = "errorId")]
    //    public string ErrorId { get; set; }

    //    [XmlElement(ElementName = "exception")]
    //    public string Exception { get; set; }

    //    [XmlElement(ElementName = "exceptionMessage")]
    //    public string ExceptionMessage { get; set; }

    //    [XmlElement(ElementName = "errorCode")]
    //    public string ErrorCode { get; set; }

    //    [XmlElement(ElementName = "displayMessage")]
    //    public string DisplayMessage { get; set; }

    //    public override string ToString()
    //    {
    //        return $"ErrorId: {ErrorId};Exception: {Exception};ErrorCode: {ErrorCode};DisplayMessage: {DisplayMessage}";
    //    }
    //}

    //[XmlRoot(ElementName = "warnings")]
    //public class Warnings
    //{
    //    [XmlElement(ElementName = "warning")]
    //    public Warning Warning { get; set; }
    //}

    //[XmlRoot(ElementName = "errorSection")]
    //public class ErrorSection
    //{
    //    [XmlElement(ElementName = "fatalErrors")]
    //    public FatalErrors FatalErrors { get; set; }

    //    [XmlElement(ElementName = "warnings")]
    //    public Warnings Warnings { get; set; }
    //}

    //public class FatalError
    //{
    //    [XmlElement(ElementName = "errorId")]
    //    public string ErrorId { get; set; }

    //    [XmlElement(ElementName = "exception")]
    //    public string Exception { get; set; }

    //    [XmlElement(ElementName = "exceptionMessage")]
    //    public string ExceptionMessage { get; set; }

    //    [XmlElement(ElementName = "errorCode")]
    //    public string ErrorCode { get; set; }

    //    [XmlElement(ElementName = "displayMessage")]
    //    public string DisplayMessage { get; set; }

    //    public override string ToString()
    //    {
    //        return $"ErrorId: {ErrorId};Exception: {Exception};ErrorCode: {ErrorCode};DisplayMessage: {DisplayMessage}";
    //    }
    //}

    //[XmlRoot(ElementName = "fatalErrors")]
    //public class FatalErrors
    //{
    //    [XmlElement(ElementName = "fatalError")]
    //    public FatalError FatalError { get; set; }

    //    [XmlElement(ElementName = "errorId")]
    //    public string ErrorId { get; set; }

    //    [XmlElement(ElementName = "exception")]
    //    public string Exception { get; set; }

    //    [XmlElement(ElementName = "exceptionMessage")]
    //    public string ExceptionMessage { get; set; }

    //    [XmlElement(ElementName = "errorCode")]
    //    public string ErrorCode { get; set; }

    //    [XmlElement(ElementName = "displayMessage")]
    //    public string DisplayMessage { get; set; }

    //    public override string ToString()
    //    {
    //        return $"ErrorId: {ErrorId};Exception: {Exception};ErrorCode: {ErrorCode};DisplayMessage: {DisplayMessage}";
    //    }
    //}
}
