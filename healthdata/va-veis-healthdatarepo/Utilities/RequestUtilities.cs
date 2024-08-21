using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using va_veis_healthdatarepo.Models.Interfaces;
using va_veis_healthdatarepo.Models.Requests;

namespace va_veis_healthdatarepo.Utilities
{
    public class RequestUtilities
    {
        private readonly ILogger _logger;
        public RequestUtilities(ILogger logger)
        {
            _logger = logger;
        }

        public async Task<string> ExecuteAsync(HttpClient request, IRequestMessage requestMessage, bool disableSSLCertificationValidation = false)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

            if (disableSSLCertificationValidation)
            {
                ServicePointManager.ServerCertificateValidationCallback =
                    delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; }; 
            }

            return await GetReqResponseAsync(request, requestMessage);

        }

        public async Task<T> ExecuteAsync<T>(HttpClient request, IRequestMessage requestMessage)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
            ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(
            delegate
            {
                return true;
            });

            return await GetReqResponseAsync<T>(request, requestMessage);
        }

        public async Task<string> GetReqResponseAsync(HttpClient client, IRequestMessage requestMessage)
        {
            var endpoint = $"{client.BaseAddress.AbsoluteUri}/{requestMessage.Resource}";

            var request = new HttpRequestMessage();
            var response = new HttpResponseMessage();

            if (requestMessage.IsGetRequest)
            {
                var uriBuilder = new UriBuilder(endpoint)
                {
                    Query = QueryString.Create(requestMessage.URLParameters.Where(p => !string.IsNullOrEmpty(p.Value))).Value
                };
                endpoint = uriBuilder.Uri.ToString();
                request = new HttpRequestMessage(HttpMethod.Get, endpoint);
            }
            else
            {
                if (requestMessage.ContentType.Equals("application/json"))
                {
                    request = new HttpRequestMessage(HttpMethod.Post, endpoint)
                    {
                        Content = new StringContent(requestMessage.Resource, Encoding.UTF8, requestMessage.ContentType)
                    };
                }
                else
                {
                    request = new HttpRequestMessage(HttpMethod.Post, endpoint)
                    {
                        Content = new StringContent(GetXmlContent(requestMessage), Encoding.UTF8, requestMessage.ContentType)
                    };
                }
            }

            response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            return null;
        }

        public async Task<T> GetReqResponseAsync<T>(HttpClient client, IRequestMessage requestMessage)
        {
            var endpoint = $"{client.BaseAddress.AbsoluteUri}/{requestMessage.Resource}";

            var request = new HttpRequestMessage();
            var response = new HttpResponseMessage();

            if (requestMessage.IsGetRequest)
            {
                var uriBuilder = new UriBuilder(endpoint)
                {
                    Query = QueryString.Create(requestMessage.URLParameters.Where(p => !string.IsNullOrEmpty(p.Value))).Value
                };
                endpoint = uriBuilder.Uri.ToString();
                request = new HttpRequestMessage(HttpMethod.Get, endpoint);
            }
            else
            {
                if(requestMessage.ContentType.Equals("application/json"))
                {
                    request = new HttpRequestMessage(HttpMethod.Post, endpoint)
                    {
                        Content = new StringContent(requestMessage.Resource, Encoding.UTF8, requestMessage.ContentType)
                    };
                }
                else
                {
                    request = new HttpRequestMessage(HttpMethod.Post, endpoint)
                    {
                        Content = new StringContent(GetXmlContent(requestMessage), Encoding.UTF8, requestMessage.ContentType)
                    };
                }
            }

            response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                if (requestMessage.ContentType == "application/json")
                {
                    var resp = response.Content.ReadAsStringAsync();
                    return await response.Content.ReadFromJsonAsync<T>();
                }
                else
                {
                    var respContent = await response.Content.ReadAsStringAsync();
                    return Serialization.DeserializeXmlResponse<T>(respContent);
                }
            }
            return default;
        }
      
        public Dictionary<string, string> GetHeaders(string ContentType)
        {
            Dictionary<string, string> headers = new Dictionary<string, string>();
            try
            {               
                if (ContentType == "application/json")
                {
                    headers.Add("Accept", "application/json");
                }
                else
                {
                   
                    headers.Add("SOAPAction", "");
                    headers.Add("ContentType", "text/xml;charset=\"utf-8\"");
                    headers.Add("Accept", "text/xml");
                }

                headers.Add("Accept-Encoding", "gzip, deflate, br");
                
                return headers;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
            finally
            {
                _logger.LogInformation("BuildBGSEndPoint Exit Point");
            }
        }

        private static string GetXmlContent(IRequestMessage requestMessage)
        {
            if(requestMessage is AppointmentRequestMessage apptMessage)
            {
                return apptMessage.GetPathwaysSoapBody();
            }

            if (requestMessage is AllergyRequestMessage allergyMessage)
            {
                return allergyMessage.BuildSoapRequest();
            }

            if (requestMessage is AlertRequestMessage alertMessage)
            {
                return alertMessage.BuildSoapRequest();
            }
            
            if (requestMessage is NonVetRequestMessage nonVetMessage)
            {
                return nonVetMessage.GetPathwaysSoapBody();
            }

            return null;
        }
    }
}