using Microsoft.AspNetCore.Mvc;
using va_veis_healthdatarepo.Extentions;
using va_veis_healthdatarepo.Models.Entities;
using va_veis_healthdatarepo.Models.Requests;
using va_veis_healthdatarepo.Models.Responses;
using va_veis_healthdatarepo.Services.Interfaces;
using va_veis_healthdatarepo.Utilities;

namespace va_veis_healthdatarepo.Controllers
{
    /// <summary>
    /// Vital Controller to get veteran vital data from HDR
    /// </summary>
    public class VitalsController : BaseController<VitalsController>
    {
        private readonly IFPDSDataService<Vital> _fpdsService;
        public VitalsController(IFPDSDataService<Vital> fpdsService, IDependencyAggregate<VitalsController> aggregate) : base(aggregate)
        {
            _fpdsService = fpdsService;
        }

        /// <summary>
        /// GET method to get veteran vital data
        /// </summary>
        /// <param name="request">user should provide client name and national ID in request parameter"</param>
        /// <returns>Return veteran vital data in JSON format</returns>
        [HttpGet(), Route("Vitals")]
        [Produces("application/json")]
        public async Task<ActionResult<FPDSResponseMessage<Vital>>> Get([FromQuery] VitalRequest request)
        {
            try
            {
                //check for valid client name and national id
                var validator = new ParameterValidator(_fpdsServiceConfigs.Value, _logger);
                validator.ValidateClientNameNationalIdController(request.ClientName, request.NationalID, "vitals");

                var startDate = request.StartDate.ToDateTimeString(ApiDateFormat);
                var endDate = request.EndDate.ToDateTimeString(ApiDateFormat);
                //get vital data from fpds service
                var response = await Task.FromResult(_fpdsService
                    .GetDataAsync(request.ClientName, request.NationalID, RequestSessionId, startDate, endDate)
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

                return new FPDSResponseMessage<Vital> { ErrorMessage = ex.Message, ErrorOccurred = true, DebugInfo = ex.ToString() };
            }
        }

        /// <summary>
        /// Legacy GET method to get veteran vital data
        /// </summary>
        /// <param name="clientName">require client name</param>
        /// <param name="nationalId">require veteran national ID</param>
        /// <returns>return vital data in JSON format</returns>
        [HttpGet]
        [Route("api/Vitals/1.0/{clientName}/{nationalId}")]
        [Route("Vitals/{clientName}/{nationalId}/{startDate}/{endDate}")]
        public async Task<ActionResult<FPDSResponseMessage<Vital>>> Get([FromRoute] string clientName,
                                                                        [FromRoute] string nationalId,
                                                                        [FromRoute] string startDate,
                                                                        [FromRoute] string endDate)
        {
            try
            {
                //check for valid client name and national id
                var validator = new ParameterValidator(_fpdsServiceConfigs.Value, _logger);
                validator.ValidateClientNameNationalIdController(clientName, nationalId, "vitals");

                startDate = startDate.ToDateTimeString(ApiDateFormat);
                endDate = endDate.ToDateTimeString(ApiDateFormat);
                //get vital data from fpds service
                var response = await Task.FromResult(_fpdsService
                    .GetDataAsync(clientName, nationalId, RequestSessionId, startDate, endDate)
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

                return new FPDSResponseMessage<Vital> { ErrorMessage = ex.Message, ErrorOccurred = true, DebugInfo = ex.ToString() };
            }
        }
    }
}
