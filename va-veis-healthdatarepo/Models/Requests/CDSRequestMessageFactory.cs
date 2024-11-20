using va_veis_healthdatarepo.Models.Entities;
using va_veis_healthdatarepo.Models.Interfaces;

namespace va_veis_healthdatarepo.Models.Requests
{
    public class CDSRequestMessageFactory<T> : ICDSRequestMessageFactory<T> where T : new()
    {
        public CDSRequestMessageFactory(Settings serviceSettings, FPDS_Settings fPDS_Settings)
        {
            RequestType = new T();
            ServiceSettings = serviceSettings;
            FPDS_Settings = fPDS_Settings;
        }

        public T RequestType { get; set; }

        public Settings ServiceSettings { get; set; }
        public FPDS_Settings FPDS_Settings { get; set; }

        public CDSRequestMessage CreateRequestMessage(CDSRequestParams requestParams)
        {
            if (RequestType is Alert)
            {
                var message = new AlertRequestMessage
                {
                    BaseUrl = ServiceSettings.BaseUrl,
                    EndPointUrl = $"{ServiceSettings.BaseUrl}/{ServiceSettings.EndPoint}",
                    ContentType = "application/xml",
                    IsGetRequest = false,
                    IsPostRequest = true,
                    RequestParams = requestParams
                };

                return message;
            }

            if (RequestType is AlertUser)
            {
                var message = new AlertRequestMessage
                {
                    BaseUrl = ServiceSettings.BaseUrl,
                    EndPointUrl = $"{ServiceSettings.BaseUrl}/{ServiceSettings.EndPoint}",
                    ContentType = "application/xml",
                    IsGetRequest = false,
                    IsPostRequest = true,
                    RequestParams = requestParams
                };

                return message;
            }

            if (RequestType is Alert)
            {
                var message = new AlertRequestMessage
                {
                    BaseUrl = ServiceSettings.BaseUrl,
                    EndPointUrl = $"{ServiceSettings.BaseUrl}/{ServiceSettings.EndPoint}",
                    ContentType = "application/xml",
                    IsGetRequest = false,
                    IsPostRequest = true,
                    RequestParams = requestParams
                };

                return message;
            }

            if (RequestType is Allergy)
            {
                var message = new AllergyRequestMessage
                {
                    BaseUrl = FPDS_Settings.BaseUrl,
                    EndPointUrl = $"{FPDS_Settings.BaseUrl}/{ServiceSettings.EndPoint}",
                    ContentType = "application/xml",
                    IsGetRequest = false,
                    IsPostRequest = true,
                    RequestParams = requestParams
                };

                return message;
            }

            if (RequestType is CDSMedication)
            {
                var message = new MedicationRequestMessage
                {
                    BaseUrl = ServiceSettings.BaseUrl,
                    EndPointUrl = $"{ServiceSettings.BaseUrl}/{ServiceSettings.EndPoint}",
                    RequestParams = requestParams
                };


                if (string.IsNullOrEmpty(message.RequestParams.nationalId))
                    throw new ApplicationException("MedicationRequestMessage - Patient National ID not set!");
                if (string.IsNullOrEmpty(message.RequestParams.clientRequestInitiationTime))
                    throw new ApplicationException("MedicationRequestMessage - clientRequestInitiationTime not set!");
                if (string.IsNullOrEmpty(message.RequestParams.startDate))
                    throw new ApplicationException("MedicationRequestMessage - startDate not set!");
                if (string.IsNullOrEmpty(message.RequestParams.endDate))
                    throw new ApplicationException("MedicationRequestMessage - endDate not set!");
                return message;
            }
            var type = RequestType.GetType().FullName;
            throw new ApplicationException("CDSRequestMessageFactory called with unsupported Request Type: " + type);
        }
    }
}
