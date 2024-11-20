using va_veis_healthdatarepo.Models.Entities;
using va_veis_healthdatarepo.Models.Interfaces;

namespace va_veis_healthdatarepo.Models.Requests
{
    public class PWSRequestMessageFactory<T> : IPWSRrequestMessageFactory<T> where T : new()
    {
        public PWSRequestMessageFactory(PathwaySettings settings)
        {
            RequestType = new T();
            ServiceSettings = settings;
        }

        public PathwaySettings ServiceSettings { get; set; }

        public T RequestType { get; set; }

        public PathwaysRequestMessage CreateRequestMessage(PathwaysRequestParams requestParams)
        {
            if (RequestType is PWSAppointment)
            {
                var message = new AppointmentRequestMessage(requestParams.ClientName, requestParams.NationalId, requestParams.StartDate, requestParams.EndDate)
                {
                    BaseUrl = ServiceSettings.BaseUrl,
                    EndPointUrl = $"{ServiceSettings.BaseUrl}/{ServiceSettings.PWSEndpointURL}",
                    ServiceSettings = ServiceSettings
                };

                return message;
            }

            if (RequestType is NonVeteranEmployeeData)
            {
                var message = new NonVetRequestMessage(requestParams.ClientName, requestParams.NationalIds)
                {
                    BaseUrl = ServiceSettings.BaseUrl,
                    EndPointUrl = $"{ServiceSettings.BaseUrl}/{ServiceSettings.PWSEndpointURL}",
                    ServiceSettings = ServiceSettings
                };

                return message;
            }

            var type = RequestType.GetType().FullName;
            throw new ApplicationException("PWSRequestMessageFactory called with unsupported Request Type: " + type);
        }
    }
}
