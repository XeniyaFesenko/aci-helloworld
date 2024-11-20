using Microsoft.AspNetCore.Mvc;
using va_veis_healthdatarepo.Extentions;
using va_veis_healthdatarepo.Models.Entities;
using va_veis_healthdatarepo.Models.Requests;
using va_veis_healthdatarepo.Models.Responses;
using va_veis_healthdatarepo.Services.Interfaces;
using va_veis_healthdatarepo.Utilities;

namespace va_veis_healthdatarepo.Controllers
{
    public class ConsultNotesController : BaseController<ConsultNotesController>
    {

        private readonly IFPDSDataService<Consult> _fpdsService;
        public ConsultNotesController(IFPDSDataService<Consult> fpdsService, IDependencyAggregate<ConsultNotesController> aggregate) : base(aggregate)
        {
            _fpdsService = fpdsService;
        }

        [HttpGet(), Route("ConsultNotes")]
        [Produces("application/json")]
        public async Task<ActionResult<FPDSResponseMessage<Consult>>> Get([FromQuery] ConsultRequest request)
        {
            try
            {
                var validator = new ParameterValidator(_fpdsServiceConfigs.Value, _logger);
                validator.ValidateClientNameNationalIdController(request.ClientName, request.NationalID, "consultnotes");

                var startDate = request.StartDate.ToDateTimeString(ApiDateFormat);
                var endDate = request.EndDate.ToDateTimeString(ApiDateFormat);

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

                return new FPDSResponseMessage<Consult> { ErrorMessage = ex.Message, ErrorOccurred = true, DebugInfo = ex.ToString() };
            }
        }

        [HttpGet()]
        [Route("CCWF/ConsultNotes/1.0/{clientName}/{nationalId}")]
        [Route("CCWF/ConsultNotes/2.0/{clientName}/{nationalId}")]
        [Route("ConsultNotes/{clientName}/{nationalId}/{startDate}/{endDate}")]
        [Produces("application/json")]
        public async Task<ActionResult<FPDSResponseMessage<Consult>>> Get([FromRoute] string clientName,
                                                                          [FromRoute] string nationalId,
                                                                          [FromRoute] string startDate,
                                                                          [FromRoute] string endDate)
        {
            try
            {
                var validator = new ParameterValidator(_fpdsServiceConfigs.Value, _logger);
                validator.ValidateClientNameNationalIdController(clientName, nationalId, "consultnotes");

                startDate = startDate.ToDateTimeString(ApiDateFormat);
                endDate = endDate.ToDateTimeString(ApiDateFormat);

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

                return new FPDSResponseMessage<Consult> { ErrorMessage = ex.Message, ErrorOccurred = true, DebugInfo = ex.ToString() };
            }
        }
    }
}
