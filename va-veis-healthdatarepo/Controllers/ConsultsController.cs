using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using va_veis_healthdatarepo.Extentions;
using va_veis_healthdatarepo.Models.Entities;
using va_veis_healthdatarepo.Models.Requests;
using va_veis_healthdatarepo.Models.Responses;
using va_veis_healthdatarepo.Services.Interfaces;
using va_veis_healthdatarepo.Utilities;

namespace va_veis_healthdatarepo.Controllers
{
    public class ConsultsController : BaseController<ConsultsController>
    {

        private readonly IFPDSDataService<ConsultDocument> _fpdsService;
        public ConsultsController(IFPDSDataService<ConsultDocument> fpdsService, IDependencyAggregate<ConsultsController> aggregate) : base(aggregate)
        {
            _fpdsService = fpdsService;
        }

        // GET api/GetConsults/clientName/nationalID
        [HttpGet(), Route("Consults")]
        [Produces("application/json")]
        public async Task<ActionResult<FPDSResponseMessage<ConsultDocument>>> Get([FromQuery] ConsultRequest request)
        {
            try
            {
                var validator = new ParameterValidator(_fpdsServiceConfigs.Value, _logger);
                validator.ValidateClientNameNationalIdController(request.ClientName, request.NationalID, "consults");

                if (!string.IsNullOrEmpty(request.DocumentTypeCode))
                {
                    _fpdsService.RequestParameters.Add("@.documentTypeCode", request.DocumentTypeCode);
                }

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

                return new FPDSResponseMessage<ConsultDocument> { ErrorMessage = ex.Message, ErrorOccurred = true, DebugInfo = ex.ToString() };
            }
        }

        // GET api/GetConsults/clientName/nationalID
        [HttpGet()]
        [Route("CCWF/Consults/1.0/{clientName}/{nationalId}")]
        [Route("CCWF/Consults/2.0/{clientName}/{nationalId}")]
        [Route("Consults/{clientName}/{nationalId}/{startDate}/{endDate}")]
        [Produces("application/json")]
        public async Task<ActionResult<FPDSResponseMessage<ConsultDocument>>> Get([FromRoute] string clientName,
                                                                                  [FromRoute] string nationalId,
                                                                                  [FromRoute] string startDate,
                                                                                  [FromRoute] string endDate)
        {
            try
            {
                var validator = new ParameterValidator(_fpdsServiceConfigs.Value, _logger);
                validator.ValidateClientNameNationalIdController(clientName, nationalId, "ccwfconsults");

                _fpdsService.RequestParameters.Add("@.documentTypeCode", "CR");

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

                return new FPDSResponseMessage<ConsultDocument> { ErrorMessage = ex.Message, ErrorOccurred = true, DebugInfo = ex.ToString() };
            }
        }
    }
}
