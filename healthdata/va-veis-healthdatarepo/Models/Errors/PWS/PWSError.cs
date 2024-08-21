using System.Xml.Serialization;

namespace va_veis_healthdatarepo.Models.Errors.PWS
{
    public class PWSError {}

    [XmlRoot(ElementName = "errorSection")]
    public class ErrorSection
    {
        [XmlElement(ElementName = "fatalErrors")]
        public FatalErrors FatalErrors { get; set; }

        [XmlElement(ElementName = "warnings")]
        public Warnings Warnings { get; set; }
    }

    public class FatalError
    {
        [XmlElement(ElementName = "errorId")]
        public string ErrorId { get; set; }

        [XmlElement(ElementName = "exception")]
        public string Exception { get; set; }

        [XmlElement(ElementName = "exceptionMessage")]
        public string ExceptionMessage { get; set; }

        [XmlElement(ElementName = "errorCode")]
        public string ErrorCode { get; set; }

        [XmlElement(ElementName = "displayMessage")]
        public string DisplayMessage { get; set; }

        public override string ToString()
        {
            return $"ErrorId: {ErrorId}; Exception: {Exception}; ErrorCode: {ErrorCode}; DisplayMessage: {DisplayMessage}";
        }
    }

    [XmlRoot(ElementName = "fatalErrors")]
    public class FatalErrors
    {
        [XmlElement(ElementName = "fatalError")]
        public FatalError FatalError { get; set; }

        [XmlElement(ElementName = "errorId")]
        public string ErrorId { get; set; }

        [XmlElement(ElementName = "exception")]
        public string Exception { get; set; }

        [XmlElement(ElementName = "exceptionMessage")]
        public string ExceptionMessage { get; set; }

        [XmlElement(ElementName = "errorCode")]
        public string ErrorCode { get; set; }

        [XmlElement(ElementName = "displayMessage")]
        public string DisplayMessage { get; set; }

        public override string ToString()
        {
            return $"ErrorId: {ErrorId}; Exception: {Exception}; ErrorCode: {ErrorCode}; DisplayMessage: {DisplayMessage}";
        }
    }

    [XmlRoot(ElementName = "warning")]
    public class Warning
    {
        [XmlElement(ElementName = "errorId")]
        public string ErrorId { get; set; }

        [XmlElement(ElementName = "exception")]
        public string Exception { get; set; }

        [XmlElement(ElementName = "exceptionMessage")]
        public string ExceptionMessage { get; set; }

        [XmlElement(ElementName = "errorCode")]
        public string ErrorCode { get; set; }

        [XmlElement(ElementName = "displayMessage")]
        public string DisplayMessage { get; set; }

        public override string ToString()
        {
            return $"ErrorId: {ErrorId}; Exception: {Exception}; ErrorCode: {ErrorCode}; DisplayMessage: {DisplayMessage}";
        }
    }

    [XmlRoot(ElementName = "warnings")]
    public class Warnings
    {
        [XmlElement(ElementName = "warning")]
        public Warning Warning { get; set; }
    }
}
