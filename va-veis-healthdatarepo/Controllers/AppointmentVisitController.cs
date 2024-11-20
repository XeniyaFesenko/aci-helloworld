using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using va_veis_healthdatarepo.Extentions;
using va_veis_healthdatarepo.Mappers;
using va_veis_healthdatarepo.Models.Entities;
using va_veis_healthdatarepo.Models.Requests;
using va_veis_healthdatarepo.Models.Responses;
using va_veis_healthdatarepo.Services.Interfaces;
using va_veis_healthdatarepo.Utilities;

namespace va_veis_healthdatarepo.Controllers
{
    /// <summary>
    /// Vital Controller to get veteran Appointment Visit data from HDR
    /// </summary>
    [ApiController]
    public class AppointmentVisitController : BaseController<AppointmentVisitController>
    {
        private readonly IFPDSDataService<Visit> _fpdsService;

        public AppointmentVisitController(IFPDSDataService<Visit> fpdsService, IDependencyAggregate<AppointmentVisitController> aggregate) : base(aggregate)
        {
            _fpdsService = fpdsService;
        }

        /// <summary>
        /// GET method to get veteran appointment visit data
        /// </summary>
        /// <param name="request">user should provide client name and national ID in request parameter. Dates are optional</param>
        /// <returns>Json data of all the visits</returns>
        /// 
        // GET AppointmentVisit?ClientName=HDR-24026&NationalId=1012901110V626595&StartDate=04/22/2000&EndDate=10/22/2024
        [HttpGet(), Route("AppointmentVisit")]
        [Produces("application/json")]
        public async Task<ActionResult<FPDSResponseMessage<Visit>>> Get([FromQuery] VisitRequest request)
        {
            try
            {
                var validator = new ParameterValidator(_fpdsServiceConfigs.Value, _logger);
                var clientDetail = validator.ValidateClientNameNationalIdController(request.ClientName, request.NationalID, "appointmentvisit");

                if (request.StartDate == null && request.EndDate == null)
                {
                    request.StartDate = request.StartDate.ToDateTimeString(ApiDateFormat, DateTime.Now.AddMonths(-6));
                    request.EndDate = request.EndDate.ToDateTimeString(ApiDateFormat, DateTime.Now);
                }
                else
                {
                    request.StartDate = request.StartDate.ToDateTimeString(ApiDateFormat);
                    request.EndDate = request.EndDate.ToDateTimeString(ApiDateFormat);
                }

                var response = await Task.FromResult((FPDSResponseMessage<Visit>)_fpdsService
                    .GetDataAsync(request.ClientName, request.NationalID, RequestSessionId, request.StartDate, request.EndDate)
                    .ConfigureAwait(false)
                    .GetAwaiter()
                    .GetResult());

                if (response.Data != null && response.Data.Count > 0)
                {
                    response.Data = TransformVisitData(response.Data);
                }

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    string.Format(
                    "ParameterValidator.ValidateClientNameNationalIdController(): Error Msg:{0}", ex.Message), ex);

                return new FPDSResponseMessage<Visit> { ErrorMessage = ex.Message, ErrorOccurred = true, DebugInfo = ex.ToString() };
            }
        }

        private List<Visit> TransformVisitData(List<Visit> visitlist)
        {
            if (visitlist == null) throw new ArgumentNullException("problemList");

            List<Visit> result = new List<Visit>();

            foreach (var visit in visitlist)
            {
                if (!string.IsNullOrEmpty(visit.DateTime))
                {                    
                    var sourceDate = visit.DateTime.ToDateTimeString("yyyyMMddHHmmss");

                    visit.DateTimeDate = DateTime.ParseExact(sourceDate, "yyyyMMddHHmmss", CultureInfo.InvariantCulture);                    
                }
                result.Add(visit);
            }
            return result;
        }
    }
}
