using System.Configuration;
using va_veis_healthdatarepo.Models.Entities;
using va_veis_healthdatarepo.Models.Interfaces;

namespace va_veis_healthdatarepo.Models.Requests
{
    public class FPDSRequestMessage<T> : IRequestMessage
    {
        public FPDSRequestMessage(string requestId) : this(requestId, new FPDS_Settings()) { }

        public FPDSRequestMessage(string requestId, FPDS_Settings configurationSettings)
        {
            requestId ??= Guid.NewGuid().ToString();

            BaseUrl = configurationSettings.BaseUrl;
            EndPointUrl = $"{configurationSettings.BaseUrl}/{configurationSettings.FPDSEndpointURL}";
            ContentType = "application/json";
            ConfigurationSettings = configurationSettings;
            IsGetRequest = true;
            IsPostRequest = false;

            URLParameters = new List<KeyValuePair<string, string>>
            {
                new("clientRequestInitiationTime", DateTime.Now.ToString("s")),
                new("requestId", requestId),
                new("templateId", ConfigurationSettings.FPDSParamTemplateId),
                new("filterId", ConfigurationSettings.FPDSParamFilterId),
                new("text", ConfigurationSettings.FPDSParamText)   
            };

            FPDSResourceMap = new Dictionary<System.Type, string>
            {
                {typeof(FPDSAppointment), "appointments"},
                {typeof(ConsultDocument), "document"},
                {typeof(Consult), "consult"},
                {typeof(Lab), "lab"},
                {typeof(Medication), "med"},
                {typeof(NonVAMed), "med"},
                {typeof(Note), "document"},
                //{typeof(NoteDetail), "document"},
                {typeof(Order), "order"},
                {typeof(Posting), "document"},
                {typeof(Problem), "problem"},
                //{typeof(Radiology), "rad"},
                {typeof(RadiologyDocument), "document" },
                {typeof(Vital), "vital"},
                {typeof(Visit), "visit"},
                {typeof(Flag), "flags"}
            };
            Resource = GetResourceFromRequestType();
        }

        public string BaseUrl { get; set; }

        public string Body { get; set; }

        public FPDS_Settings ConfigurationSettings { get; set; }

        public string ContentType {  get; set; }

        public string EndPointUrl { get; set; }

        public bool IsGetRequest { get; set; }

        public bool IsPostRequest { get; set; }

        public Dictionary<System.Type, string> FPDSResourceMap { get; set; }

        public string Resource { get; set; }

        public List<KeyValuePair<string, string>> URLParameters { get; set; }

        protected string GetResourceFromRequestType()
        {
            return FPDSResourceMap[typeof(T)];
        }
    }
}
