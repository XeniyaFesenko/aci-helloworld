using va_veis_healthdatarepo.Models.Interfaces;

namespace va_veis_healthdatarepo.Models.Requests
{
    public abstract class CDSRequestMessage : IRequestMessage
    {
        protected CDSRequestMessage()
        {
            RequestParams = new CDSRequestParams();
            IsGetRequest = true;
            IsPostRequest = false;
            ContentType = "application/xml";
        }

        public string BaseUrl { get; set; }
        public string Body { get; set; }
        public string ContentType { get; set; }
        public string EndPointUrl { get; set; }
        public bool IsGetRequest { get; set; }
        public bool IsPostRequest { get; set; }
        public CDSRequestParams RequestParams { get; set; }
        public string Resource { get; set; }
        public List<KeyValuePair<string, string>> URLParameters { get; set; }

        public virtual string BuildSoapRequest()
        {
            throw new ApplicationException("CDSRequestMessage base class cannot be called directly");
        }
    }
}
