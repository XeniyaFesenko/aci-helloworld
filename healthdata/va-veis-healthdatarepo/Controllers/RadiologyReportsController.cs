using Microsoft.AspNetCore.Mvc;
using va_veis_healthdatarepo.Extentions;
using va_veis_healthdatarepo.Models.Entities;
using va_veis_healthdatarepo.Models.Requests;
using va_veis_healthdatarepo.Models.Responses;
using va_veis_healthdatarepo.Services.Interfaces;
using va_veis_healthdatarepo.Utilities;

namespace va_veis_healthdatarepo.Controllers
{
    public class RadiologyReportsController : BaseController<RadiologyReportsController>
    {

        private readonly IFPDSDataService<RadiologyDocument> _fpdsService;
        public RadiologyReportsController(IFPDSDataService<RadiologyDocument> fpdsService, IDependencyAggregate<RadiologyReportsController> aggregate) : base(aggregate)
        {
            _fpdsService = fpdsService;
            _fpdsService.RequestParameters.Add("category", "RA");
        }

        [HttpGet(), Route("RadiologyReports")]
        [Produces("application/json")]
        public async Task<ActionResult<FPDSResponseMessage<RadiologyDocument>>> Get([FromQuery] RadiologyReportsRequest request)
        {
            try
            {
                var validator = new ParameterValidator(_fpdsServiceConfigs.Value, _logger);
                validator.ValidateClientNameNationalIdController(request.ClientName, request.NationalID, "radiologyreports");

                request.StartDate = request.StartDate.ToDateTimeString(ApiDateFormat);
                request.EndDate = request.EndDate.ToDateTimeString(ApiDateFormat);

                var response = await Task.FromResult(_fpdsService
                    .GetDataAsync(request.ClientName, request.NationalID, RequestSessionId, request.StartDate, request.EndDate)
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

                return new FPDSResponseMessage<RadiologyDocument> { ErrorMessage = ex.Message, ErrorOccurred = true, DebugInfo = ex.ToString() };
            }
        }

        [HttpGet()]
        [Route("RadiologyReports/{clientName}/{nationalId}/{startDate}/{endDate}")]
        [Produces("application/json")]
        public async Task<ActionResult<FPDSResponseMessage<RadiologyDocument>>> Get([FromRoute] string clientName, [FromRoute] string nationalId, [FromRoute] string startDate, [FromRoute] string endDate)
        {
            try
            {
                var validator = new ParameterValidator(_ipwServiceConfigs.Value, _logger);
                validator.ValidateClientNameNationalIdController(clientName, nationalId, "radiologyreports");

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

                return new FPDSResponseMessage<RadiologyDocument> { ErrorMessage = ex.Message, ErrorOccurred = true, DebugInfo = ex.ToString() };
            }
        }
    }
}
