using System.Text;
using va_veis_healthdatarepo.Models.Interfaces;

namespace va_veis_healthdatarepo.Models.Requests
{
    public class PathwaysRequestMessage : IPathwaysRequestMessage, IRequestMessage
    {
        public PathwaysRequestMessage(string clientName)
        {
            if (string.IsNullOrEmpty(clientName)) throw new ArgumentNullException("clientName");
        }

        public PathwaysRequestMessage(string clientName, PathwaySettings serviceSettings)
        {
            if (string.IsNullOrEmpty(clientName)) throw new ArgumentNullException("clientName");
            ServiceSettings = serviceSettings;
            BaseUrl = serviceSettings.BaseUrl;
            EndPointUrl = $"{serviceSettings.BaseUrl}/{serviceSettings.PWSEndpointURL}";
        }

        public string BaseUrl { get; set; }
        public string Body { get; set; }
        public string EndPointUrl { get; set; }
        public string ContentType { get; set; }
        public string Resource { get; set; }
        public bool IsGetRequest { get; set; }
        public bool IsPostRequest { get; set; }
        public PathwaySettings ServiceSettings { get; set; }
        public List<KeyValuePair<string, string>> URLParameters { get; set; }

        #region IPathwaysRequestMessage Members
        public string BuildSoapRequestMessage()
        {
            const string endOfSoapEnvelope = "</soapenv:Envelope>";
            var soapHeader = GetPathwaysSoapHeader();
            // soap body implemented by concrete service classes
            var soapBody = GetPathwaysSoapBody();
            var soapRequestMessage = new StringBuilder();
            soapRequestMessage
                .Append(soapHeader)
                .Append(soapBody)
                .Append(endOfSoapEnvelope);
            return soapRequestMessage.ToString();
        }

        #endregion

        public virtual string GetPathwaysSoapHeader()
        {
            var nameSpaceUrl = ServiceSettings.PWSNameSpace;
            var nameSpacePrefix = ServiceSettings.PWSNameSpacePrefix;
            if (string.IsNullOrEmpty(nameSpacePrefix))
            {
                throw new ApplicationException("PathwaysRequestMessage::Invalid NameSpacePrefix");
            }

            if (string.IsNullOrEmpty(nameSpaceUrl))
            {
                throw new ApplicationException("PathwaysRequestMessage::Invalid NameSpaceUrl");
            }

            var soapHeader = string.Format(
                @"<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:{0}=""{1}""> 
                                <soapenv:Header/>", nameSpacePrefix, nameSpaceUrl);
            return soapHeader;
        }

        public virtual string GetPathwaysSoapBody()
        {
            throw new ApplicationException("GetPathwaysSoapBody base method was called. This should not be called directly.");
        }
    }
}
