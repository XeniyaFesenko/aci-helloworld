using Microsoft.AspNetCore.Mvc;
using va_veis_healthdatarepo.Extentions;
using va_veis_healthdatarepo.Models.Entities;
using va_veis_healthdatarepo.Models.Requests;
using va_veis_healthdatarepo.Models.Responses;
using va_veis_healthdatarepo.Services.Interfaces;
using va_veis_healthdatarepo.Utilities;

namespace va_veis_healthdatarepo.Controllers
{
    public class AppointmentsController : BaseController<AppointmentsController>
    {
        private readonly IPWSDataService<PWSAppointment> _pathwaysData;

        public AppointmentsController(IPWSDataService<PWSAppointment> pWSDataService, IDependencyAggregate<AppointmentsController> aggregate) : base(aggregate)
        {
            _pathwaysData = pWSDataService;
        }

        // GET Appointments/FtPCRM/1008523096V381537?noFilter=true
        [HttpGet(), Route("Appointments")]
        [Produces("application/json")]
        public async Task<ActionResult<PWSResponseMessage<PWSAppointment>>> Get([FromQuery] AppointmentRequest request)
        {
            try
            {
                var validator = new ParameterValidator(_logger);
                var clientDetail = validator.ValidateClientNameNationalIdController(request.ClientName, request.NationalID, "appointments");

                if (request.NoFilter)
                {
                    request.StartDate = DateTime.Parse("01/01/1950").ToString(ApiDateFormat);
                    request.EndDate = DateTime.Parse("01/01/2050").ToString(ApiDateFormat);
                }
                else
                {
                    request.StartDate = request.StartDate.ToDateTimeString(ApiDateFormat, DateTime.Now.AddMonths(-3));
                    request.EndDate = request.EndDate.ToDateTimeString(ApiDateFormat, DateTime.Now.AddMonths(6));
                }

                var response = await Task.FromResult((PWSResponseMessage<PWSAppointment>)_pathwaysData
                    .GetAppointmentsAsync(request.ClientName, request.NationalID, request.StartDate, request.EndDate)
                    .ConfigureAwait(false)
                    .GetAwaiter()
                    .GetResult());

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    string.Format(
                    "ParameterValidator.ValidateClientNameNationalIdController(): Error Msg:{0}", ex.Message), ex);

                return new PWSResponseMessage<PWSAppointment> { ErrorMessage = ex.Message, ErrorOccurred = true, DebugInfo = ex.ToString() };
            }
        }

        // GET Appointments/FtPCRM/1008523096V381537?noFilter=true
        [HttpGet()]
        [Route("Appointments/{clientName}/{nationalId}/{startDate?}/{endDate?}")]
        [Produces("application/json")]
        public async Task<ActionResult<PWSResponseMessage<PWSAppointment>>> Get([FromRoute] string clientName, [FromRoute] string nationalId, [FromRoute] string startDate, [FromRoute] string endDate, [FromQuery] bool noFilter = false)
        {
            try
            {
                var validator = new ParameterValidator(_logger);
                var clientDetail = validator.ValidateClientNameNationalIdController(clientName, nationalId, "appointments");

                if (noFilter)
                {
                    startDate = DateTime.Parse("01/01/1950").ToString(ApiDateFormat);
                    endDate = DateTime.Parse("01/01/2050").ToString(ApiDateFormat);
                }
                else
                {
                    startDate = startDate.ToDateTimeString(ApiDateFormat, DateTime.Now.AddMonths(-3));
                    endDate = endDate.ToDateTimeString(ApiDateFormat, DateTime.Now.AddMonths(6));
                }

                var response = await Task.FromResult((PWSResponseMessage<PWSAppointment>)_pathwaysData
                    .GetAppointmentsAsync(clientName, nationalId, startDate, endDate)
                    .ConfigureAwait(false)
                    .GetAwaiter()
                    .GetResult());

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    string.Format(
                    "ParameterValidator.ValidateClientNameNationalIdController(): Error Msg:{0}", ex.Message), ex);

                return new PWSResponseMessage<PWSAppointment> { ErrorMessage = ex.Message, ErrorOccurred = true, DebugInfo = ex.ToString() };
            }
        }

        // GET Appointments/FtPCRM/1008523096V381537?noFilter=true
        [HttpGet()]
        [Route("CCWF/Appointments/1.0/{clientName}/{nationalId}")]
        [Route("CCWF/Appointments/2.0/{clientName}/{nationalId}")]
        [Produces("application/json")]
        public async Task<ActionResult<PWSResponseMessage<PWSAppointment>>> Get([FromRoute] string clientName, [FromRoute] string nationalId, [FromQuery] bool noFilter = false)
        {
            try
            {
                var validator = new ParameterValidator(_logger);
                var clientDetail = validator.ValidateClientNameNationalIdController(clientName, nationalId, "ccwfappointments");

                var startDate = noFilter ? DateTime.Parse("01/01/1950").ToString(ApiDateFormat) : new DateTime().ToString(ApiDateFormat);
                var endDate = noFilter ? DateTime.Parse("01/01/2050").ToString(ApiDateFormat) : new DateTime().ToString(ApiDateFormat);

                var response = await Task.FromResult((PWSResponseMessage<PWSAppointment>)_pathwaysData
                    .GetAppointmentsAsync(clientName, nationalId, startDate, endDate)
                    .ConfigureAwait(false)
                    .GetAwaiter()
                    .GetResult());

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    string.Format(
                    "ParameterValidator.ValidateClientNameNationalIdController(): Error Msg:{0}", ex.Message), ex);

                return new PWSResponseMessage<PWSAppointment> { ErrorMessage = ex.Message, ErrorOccurred = true, DebugInfo = ex.ToString() };
            }
        }
    }
}
