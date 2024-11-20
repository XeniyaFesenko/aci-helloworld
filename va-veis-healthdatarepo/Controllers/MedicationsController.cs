using Microsoft.AspNetCore.Mvc;
using va_veis_healthdatarepo.Extentions;
using va_veis_healthdatarepo.Models.Entities;
using va_veis_healthdatarepo.Models.Requests;
using va_veis_healthdatarepo.Models.Responses;
using va_veis_healthdatarepo.Services.Interfaces;
using va_veis_healthdatarepo.Utilities;

namespace va_veis_healthdatarepo.Controllers
{
    public class MedicationsController : BaseController<MedicationsController>
    {
        private readonly IFPDSDataService<Medication> _fpdsService;
        public MedicationsController(IFPDSDataService<Medication> fpdsService, IDependencyAggregate<MedicationsController> aggregate) : base(aggregate)
        {
            _fpdsService = fpdsService;
            _fpdsService.RequestParameters.Add("vaType", "O");
        }

        // GET api/GetMedications/FtPCRM/1012592732?noFilter=true
        [HttpGet(), Route("Medications")]
        [Produces("application/json")]
        public async Task<ActionResult<FPDSResponseMessage<Medication>>> Get([FromQuery] MedicationsRequest request)
        {
            try
            {
                var validator = new ParameterValidator(_fpdsServiceConfigs.Value, _logger);
                var clientDetail = validator.ValidateClientNameNationalIdController(request.ClientName, request.NationalID, "medications");

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

                return new FPDSResponseMessage<Medication> { ErrorMessage = ex.Message, ErrorOccurred = true, DebugInfo = ex.ToString() };
            }
        }

        // GET api/GetMedications/FtPCRM/1012592732?noFilter=true
        [HttpGet()]
        [Route("Medications/{clientName}/{nationalId}")]
        [Route("Medications/{clientName}/{nationalId}/{startDate}/{endDate}")]
        [Produces("application/json")]
        public async Task<ActionResult<FPDSResponseMessage<Medication>>> Get([FromRoute] string clientName,
                                                                             [FromRoute] string nationalId,
                                                                             [FromRoute] string startDate,
                                                                             [FromRoute] string endDate)
        {
            try
            {
                var validator = new ParameterValidator(_fpdsServiceConfigs.Value, _logger);
                var clientDetail = validator.ValidateClientNameNationalIdController(clientName, nationalId, "medications");

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

                return new FPDSResponseMessage<Medication> { ErrorMessage = ex.Message, ErrorOccurred = true, DebugInfo = ex.ToString() };
            }
        }
    }
}
