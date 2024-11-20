using System.Xml.Linq;
using va_veis_healthdatarepo.Models.Entities;
using va_veis_healthdatarepo.Models.Errors.PWS;
using va_veis_healthdatarepo.Models.Interfaces;
using va_veis_healthdatarepo.Models.Requests;
using va_veis_healthdatarepo.Models.Responses;
using va_veis_healthdatarepo.Services.Interfaces;
using va_veis_healthdatarepo.Utilities;

namespace va_veis_healthdatarepo.Services
{
    public class PWSDataService<T> : BaseService<PWSDataService<T>>, IPWSDataService<T> where T : new()
    {

        public PWSDataService(IDependencyAggregate<PWSDataService<T>> aggregate) : base(aggregate)
        {
            PWSRequestMessageFactory = new PWSRequestMessageFactory<T>(_ipwServiceConfigs.Value);
        }

        public IPWSRrequestMessageFactory<T> PWSRequestMessageFactory { get; set; }

        #region IPWSData Members
        public async Task<PWSResponseMessage<PWSAppointment>> GetAppointmentsAsync(string clientName, string nationalID, string startDate, string endDate)
        {
            if (string.IsNullOrEmpty(clientName)) throw new ArgumentNullException("clientName");
            if (string.IsNullOrEmpty(nationalID)) throw new ArgumentNullException("nationalID");
            if (string.IsNullOrEmpty(startDate)) throw new ArgumentNullException("startDate");
            if (string.IsNullOrEmpty(endDate)) throw new ArgumentNullException("endDate");

            var response = new PWSResponseMessage<PWSAppointment>();
            try
            {
                var utils = new RequestUtilities(_logger);
                var dataRequest = PWSRequestMessageFactory.CreateRequestMessage(
                    new PathwaysRequestParams
                    {
                        ClientName = clientName,
                        NationalId = nationalID,
                        EndDate = endDate,
                        StartDate = startDate
                    });
               
                

                var client = _httpClientFactory.CreateClient("hdr");

                client.BaseAddress = new Uri(dataRequest.EndPointUrl);

                var headers = utils.GetHeaders(dataRequest.ContentType);
                foreach (KeyValuePair<string, string> key in headers)
                {
                    client.DefaultRequestHeaders.Add(key.Key, key.Value);
                };

                var dataResponse = await utils.ExecuteAsync(client, dataRequest);

                var appointmentDataContant = XDocument.Parse(dataResponse).Root.Descendants().FirstOrDefault(d => d.Name.LocalName.ToLower().Equals("out")).Value;

                //Remove xmlns and prefix
                int start, end = 0;

                start = appointmentDataContant.IndexOf("xmlns");
                end = appointmentDataContant.IndexOf(">", start + 1);

                if (start > 0 && end > start) appointmentDataContant = appointmentDataContant.Remove(start, end - start);
                appointmentDataContant = appointmentDataContant.Replace("appointmentsdata:", "");

                var responseData = Serialization.DeserializeXmlResponse<AppointmentsData>(appointmentDataContant);

                if (responseData.ErrorSection != null)
                {
                    response.ErrorOccurred = true;
                    response.ErrorMessage = GetFatalErrorMessage(responseData.ErrorSection?.FatalErrors);
                    response.DebugInfo = GetFatalError(responseData.ErrorSection?.FatalErrors);
                }
                else
                {
                    response.Data = TransformPWSAppointments(responseData.Patients.Patient.Appointments);
                }
            }
            catch (Exception ex)
            {
                var errorMsg = ex.Message;
                _logger.LogError(
                    string.Format(
                        "PWSData::GetAppointments(): Error making PWS request for Client: {0} NationalID: {1} Details: {2}",
                        clientName, nationalID, ex.Message), ex);
                response.ErrorOccurred = true;
                response.Status = "Error";
                response.DebugInfo = errorMsg;
                response.ErrorMessage = "PWS Error: " + errorMsg;
            }
            return response;
        }

        public async Task<PWSResponseMessage<NonVeteranEmployeeData>> GetNonVetRecordAsync(string clientName, List<string> nationalIds)
        {
            var response = new PWSResponseMessage<NonVeteranEmployeeData>();
            try
            {
                var utils = new RequestUtilities(_logger);
                var dataRequest = new NonVetRequestMessage(clientName, nationalIds, _ipwServiceConfigs.Value);
                var client = _httpClientFactory.CreateClient("hdr");
                client.BaseAddress = new Uri(dataRequest.EndPointUrl);

                var headers = utils.GetHeaders(dataRequest.ContentType);
                foreach (KeyValuePair<string, string> key in headers)
                {
                    client.DefaultRequestHeaders.Add(key.Key, key.Value);
                };
                var serviceCallResponse = await utils.ExecuteAsync(client, dataRequest);

                //CheckForExceptions(serviceCallResponse);

                var nonVeteranDataContent = XDocument.Parse(serviceCallResponse).Root.Descendants().FirstOrDefault(d => d.Name.LocalName.ToLower().Equals("out")).Value;

                //Remove xmlns and prefix
                int start, end = 0;

                start = nonVeteranDataContent.IndexOf("xmlns");
                end = nonVeteranDataContent.IndexOf(">", start + 1);

                if (start > 0 && end > start) nonVeteranDataContent = nonVeteranDataContent.Remove(start, end - start);
                nonVeteranDataContent = nonVeteranDataContent.Replace("nonVeteranEmployeedata:", "");

                var responseData = Serialization.DeserializeXmlResponse<NonVeteranEmployeeData>(nonVeteranDataContent);

                if (responseData.patients.Any((p) => p.ErrorSection != null))
                {
                    var errorSection = responseData.patients.FirstOrDefault((p) => p.ErrorSection != null).ErrorSection;
                    response.ErrorOccurred = true;
                    response.ErrorMessage = GetFatalErrorMessage(errorSection?.FatalErrors);
                    response.DebugInfo = GetFatalError(errorSection?.FatalErrors);
                }
                else
                {
                    response.Data = new List<NonVeteranEmployeeData> { responseData };
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "PWSData::GetNonVetRecord(): Error making Pathways request for Client: {0} Details: {1}",
                        clientName, ex.Message);
                response.ErrorOccurred = true;
                response.Status = "Error";
                response.ErrorMessage = "PWS Error: " + ex.Message;
                response.DebugInfo = ex.Message;
            }
            return response;
        }
        #endregion

        public List<PWSAppointment> TransformPWSAppointments(Appointments appointments)
        {
            var appointmentList = new List<PWSAppointment>();
            if (appointments != null)
            {
                foreach (var apt in appointments.Appointment)
                {
                    var aptItem = new PWSAppointment();

                    //
                    // data from repository is inconsistent...must null check properties before accessing
                    //
                    if (apt.Location != null && apt.Location.Institution != null &&
                        apt.Location.Institution.Identifier != null && apt.Location.Institution.Identifier.Name != null)
                    {
                        aptItem.FacilityName = apt.Location.Institution.Identifier.Name;
                    }
                    if (apt.Location != null && apt.Location.Institution != null &&
                        apt.Location.Institution.Identifier != null)
                    {
                        aptItem.FacilityCode = apt.Location.Institution.Identifier.Identity;
                    }
                    if (apt.Location != null && apt.Location.Identifier != null && apt.Location.Identifier.Name != null)
                    {
                        aptItem.ClinicName = apt.Location.Identifier.Name;
                    }
                    if (apt.Location != null)
                    {
                        aptItem.ClinicCode = apt.Location.Identifier.Identity;
                    }
                    aptItem.OtherInformation = apt.OtherInformation ?? "";
                    if (apt.Status != null && !string.IsNullOrEmpty(apt.Status.DisplayText))
                    {
                        aptItem.StatusName = apt.Status.DisplayText;
                    }
                    if (apt.Status != null)
                    {
                        aptItem.StatusCode = apt.Status.Code.ToString();
                    }
                    if (apt.AppointmentStatus != null && !string.IsNullOrEmpty(apt.AppointmentStatus.Code))
                    {
                        aptItem.AppointmentStatusCode = apt.AppointmentStatus.Code;
                    }
                    if (apt.AppointmentStatus != null && !string.IsNullOrEmpty(apt.AppointmentStatus.DisplayText))
                    {
                        aptItem.AppointmentStatusName = apt.AppointmentStatus.DisplayText;
                    }
                    if (apt.AppointmentType != null && !string.IsNullOrEmpty(apt.AppointmentType.DisplayText))
                    {
                        aptItem.TypeName = apt.AppointmentType.DisplayText;
                    }
                    if (apt.AppointmentType != null)
                    {
                        aptItem.TypeCode = apt.AppointmentType.Code.ToString();
                    }
                    if (apt.AppointmentDateTime != null)
                    {
                        aptItem.DateTime = apt.AppointmentDateTime.Literal.ToString();
                    }
                    if (apt.RecordIdentifier != null)
                    {
                        aptItem.LocalID = apt.RecordIdentifier.Identity.ToString();
                    }
                    appointmentList.Add(aptItem);
                }
            }
            return appointmentList;
        }

        private string GetFatalError(FatalErrors fatalErrors)
        {
            return fatalErrors.FatalError != null
                ? fatalErrors.FatalError.ToString()
                : fatalErrors.ToString();
        }

        private string GetFatalErrorMessage(FatalErrors fatalErrors)
        {
            return fatalErrors.FatalError != null
                ? fatalErrors.FatalError.ExceptionMessage
                : fatalErrors.ExceptionMessage;
        }
    }
}
