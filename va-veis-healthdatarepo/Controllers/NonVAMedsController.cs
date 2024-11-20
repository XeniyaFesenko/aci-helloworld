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
    public class NonVAMedsController : BaseController<NonVAMedsController>
    {
        private readonly IFPDSDataService<NonVAMed> _fpdsService;
        public NonVAMedsController(IFPDSDataService<NonVAMed> fpdsService, IDependencyAggregate<NonVAMedsController> aggregate) : base(aggregate)
        {
            _fpdsService = fpdsService;
        }

        // GET NonVAMeds?ClientName=ClientName&NationalId=ICN&StartDate=yyyymmdddd&EndDate=yyyymmdddd
        // Same behavior as NonVAMeds/{version}/{clientName}/{nationalID} in the API controller HDR 1.0
        [HttpGet(), Route("NonVAMeds")]
        [Produces("application/json")]
        public async Task<ActionResult<FPDSResponseMessage<NonVAMed>>> GetV2FromQuery([FromQuery] MedicationsRequest request)
        {
            try
            {
                var validator = new ParameterValidator(_fpdsServiceConfigs.Value, _logger);
                var clientDetail = validator.ValidateClientNameNationalIdController(request.ClientName, request.NationalID, "nonvameds");

                var startDate = request.StartDate.ToDateTimeString(ApiDateFormat);
                var endDate  = request.EndDate.ToDateTimeString(ApiDateFormat);

                var response = await Task.FromResult((FPDSResponseMessage<NonVAMed>)_fpdsService
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

                return new FPDSResponseMessage<NonVAMed> { ErrorMessage = ex.Message, ErrorOccurred = true, DebugInfo = ex.ToString() };
            }
        }

        // GET NonVAMeds/ClientName/ICN/StartDate/EndDate
        // Same behavior as NonVAMeds/{version}/{clientName}/{nationalID} in the API controller HDR 1.0
        [HttpGet(), Route("NonVAMeds/{clientName}/{nationalId}/{startDate}/{endDate}")]
        [Produces("application/json")]
        public async Task<ActionResult<FPDSResponseMessage<NonVAMed>>> GetV2FromRoute([FromRoute] string clientName, [FromRoute] string nationalId, [FromRoute] string startDate, [FromRoute] string endDate)
        {
            try
            {

                var validator = new ParameterValidator(_fpdsServiceConfigs.Value, _logger);
                var clientDetail = validator.ValidateClientNameNationalIdController(clientName, nationalId, "nonvameds");

                startDate = startDate.ToDateTimeString(ApiDateFormat);
                endDate = endDate.ToDateTimeString(ApiDateFormat);

                var response = await Task.FromResult((FPDSResponseMessage<NonVAMed>)_fpdsService
                    .GetDataAsync(clientName, nationalId, RequestSessionId)
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

                return new FPDSResponseMessage<NonVAMed> { ErrorMessage = ex.Message, ErrorOccurred = true, DebugInfo = ex.ToString() };
            }
        }

        // GET NonVAMeds?ClientName=ClientName&NationalId=ICN&StartDate=yyyymmdddd&EndDate=yyyymmdddd
        // Same behavior as NonVAMeds/VATypeN/{version}/{clientName}/{nationalID} in the MVC controller HDR 1.0
        [HttpGet(), Route("NonVAMeds/VATypeN")]
        [Produces("application/json")]
        public async Task<ActionResult<FPDSResponseMessage<NonVAMed>>> GetV1FromQuery([FromQuery] MedicationsRequest request)
        {
            try
            {
                _fpdsService.RequestParameters.Add("vaType", "N");

                var validator = new ParameterValidator(_fpdsServiceConfigs.Value, _logger);
                var clientDetail = validator.ValidateClientNameNationalIdController(request.ClientName, request.NationalID, "nonvameds");

                var startDate = request.StartDate.ToDateTimeString(ApiDateFormat);
                var endDate = request.EndDate.ToDateTimeString(ApiDateFormat);

                var response = await Task.FromResult((FPDSResponseMessage<NonVAMed>)_fpdsService
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

                return new FPDSResponseMessage<NonVAMed> { ErrorMessage = ex.Message, ErrorOccurred = true, DebugInfo = ex.ToString() };
            }
        }
        // GET NonVAMeds/VATypeN/ClientName/ICN/StartDate/EndDate
        // Same behavior as NonVAMeds/{version}/{clientName}/{nationalID} in the MVC controller HDR 1.0
        [HttpGet(), Route("/NonVAMeds/VATypeN/{clientName}/{nationalId}/{startDate}/{endDate}")]
        [Produces("application/json")]
        public async Task<ActionResult<FPDSResponseMessage<NonVAMed>>> GetV1FromRoute([FromRoute] string clientName, [FromRoute] string nationalId, [FromRoute] string startDate, [FromRoute] string endDate)
        {
            try
            {
                _fpdsService.RequestParameters.Add("vaType", "N");

                var validator = new ParameterValidator(_fpdsServiceConfigs.Value, _logger);
                var clientDetail = validator.ValidateClientNameNationalIdController(clientName, nationalId, "nonvameds");

                startDate = startDate.ToDateTimeString(ApiDateFormat);
                endDate = endDate.ToDateTimeString(ApiDateFormat);

                var response = await Task.FromResult((FPDSResponseMessage<NonVAMed>)_fpdsService
                    .GetDataAsync(clientName, nationalId, RequestSessionId)
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

                return new FPDSResponseMessage<NonVAMed> { ErrorMessage = ex.Message, ErrorOccurred = true, DebugInfo = ex.ToString() };
            }
        }
    }
}
