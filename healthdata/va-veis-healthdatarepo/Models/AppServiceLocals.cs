namespace va_veis_healthdatarepo.Models
{
    public class FPDS_Settings : Settings
    {
        public string FPDSEndpointURL { get; set; }

        public string FPDSParamFilterId { get; set; }

        public string FPDSParamTemplateId { get; set; }

        public string FPDSParamText { get; set; }
    }

    public class PathwaySettings : Settings
    {
        public string PWSEndpointURL { get; set; }

        public string PWSNameSpace { get; set; }

        public string PWSNameSpacePrefix { get; set; }
    }

    public class Settings
    {
        public string BaseUrl { get; set; }

        public bool DisableSSLCertificateValidation { get; set; }

        public string EndPoint { get; set; }
    }

    //public class VistaUsersSettings : Settings
    //{
    //}

}
