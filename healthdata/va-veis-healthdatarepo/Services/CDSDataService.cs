using System.Xml.Linq;
using va_veis_healthdatarepo.Mappers;
using va_veis_healthdatarepo.Models.Entities;
using va_veis_healthdatarepo.Models.Interfaces;
using va_veis_healthdatarepo.Models.Requests;
using va_veis_healthdatarepo.Models.Responses;
using va_veis_healthdatarepo.Services.Interfaces;
using va_veis_healthdatarepo.Utilities;

namespace va_veis_healthdatarepo.Services
{
    public class CDSDataService<T> : BaseService<CDSDataService<T>>, ICDSDataService<T> where T : new()
    {
        private readonly ICDSResponseParser _parser;
        public CDSDataService(IDependencyAggregate<CDSDataService<T>> aggregate, ICDSResponseParser parser) : base(aggregate)
        {
            CDSRequestMessageFactory = new CDSRequestMessageFactory<T>(_cdsServiceConfigs.Value, _fpdsServiceConfigs.Value);
            _parser = parser;
        }

        public ICDSRequestMessageFactory<T> CDSRequestMessageFactory { get; set; }

        public async Task<CDSResponseMessage<CDSAlert>> GetAlertDataAsync(string clientName, Guid messageID, string users)
        {
            var response = new CDSResponseMessage<CDSAlert>();
            try
            {
                var utils = new RequestUtilities(_logger);
                var requestParams = new CDSRequestParams
                {
                    clientName = clientName,
                    messageId = messageID,
                    users = users
                };
                                
                var dataRequest = CDSRequestMessageFactory.CreateRequestMessage(requestParams);

                var client = _httpClientFactory.CreateClient("hdr");

                client.BaseAddress = new Uri(dataRequest.EndPointUrl);

                var headers = utils.GetHeaders(dataRequest.ContentType);
                foreach (KeyValuePair<string, string> key in headers)
                {
                    client.DefaultRequestHeaders.Add(key.Key, key.Value);
                };

                var dataResponse = await utils.ExecuteAsync(client, dataRequest, _cdsServiceConfigs.Value.DisableSSLCertificateValidation);

                var clinicDataContent = XDocument.Parse(dataResponse).Root.Descendants().FirstOrDefault(d => d.Name.LocalName.ToLower().Equals("out")).Value;

                //Remove xmlns and prefix
                int start, end = 0;

                start = clinicDataContent.IndexOf("xmlns");
                end = clinicDataContent.IndexOf(">", start + 1);

                if (end > start) clinicDataContent = clinicDataContent.Remove(start, end - start);
                clinicDataContent = clinicDataContent.Replace("clinicaldata:", "");

                var responseData = Serialization.DeserializeXmlResponse<ClinicData>(clinicDataContent);

                if (responseData.ErrorSection != null)
                {
                    response.ErrorOccurred = true;
                    response.ErrorMessage = responseData.ErrorSection?.Warnings?.Warning?.ExceptionMessage;
                    response.DebugInfo = responseData.ErrorSection?.Warnings?.Warning.ToString();
                }
                else
                {
                    response.Data = new AlertTransformer().TransformAlerts(responseData.UserAlertRead.AlertUsers.AlertUser);
                }
            }
            catch (Exception ex)
            {
                response.ErrorOccurred = true;
                response.Status = "Error";
                response.ErrorMessage = "An unexpected error occurred. Please navigate to VistA/CPRS for this information.";
                response.DebugInfo = ex.ToString();
            }
            return response;
        }

        public async Task<CDSResponseMessage<CDSAllergy>> GetAllergyDataAsync(string clientName, string nationalID, string startDate = null, string endDate = null)
        {
            var response = new CDSResponseMessage<CDSAllergy>();

            try
            {
                var utils = new RequestUtilities(_logger);
                var requestParams = new CDSRequestParams
                {
                    clientName = clientName,
                    nationalId = nationalID
                };

                requestParams.startDate = !string.IsNullOrEmpty(startDate) ? startDate : requestParams.startDate;
                requestParams.endDate = !string.IsNullOrEmpty(endDate) ? endDate : requestParams.endDate;

                var dataRequest = CDSRequestMessageFactory.CreateRequestMessage(requestParams);
                var client = _httpClientFactory.CreateClient("hdr");
                var headers = utils.GetHeaders(dataRequest.ContentType);

                client.BaseAddress = new Uri(dataRequest.EndPointUrl);

                foreach (var key in headers)
                    client.DefaultRequestHeaders.Add(key.Key, key.Value);

                var dataResponse = await utils.ExecuteAsync(client, dataRequest, _cdsServiceConfigs.Value.DisableSSLCertificateValidation);
                var parsedResponse = _parser.ParseAllergyFromXMLResponse(dataResponse);

                response.Data = parsedResponse;
                
            }
            catch (Exception ex)
            {
                response.ErrorOccurred = true;
                response.Status = "Error";
                response.ErrorMessage = "An unexpected error occurred. Please navigate to VistA/CPRS for this information.";
                response.DebugInfo = ex.ToString();
            }

            return response;
        }
    }
}
