using Microsoft.AspNetCore.Mvc;
using va_veis_healthdatarepo.Extentions;
using va_veis_healthdatarepo.Models.Entities;
using va_veis_healthdatarepo.Models.Requests;
using va_veis_healthdatarepo.Models.Responses;
using va_veis_healthdatarepo.Services.Interfaces;
using va_veis_healthdatarepo.Utilities;

namespace va_veis_healthdatarepo.Controllers
{
    public class PostingsController : BaseController<PostingsController>
    {


        private readonly IFPDSDataService<Posting> _fpdsService;

        public PostingsController(IFPDSDataService<Posting> fpdsService, IDependencyAggregate<PostingsController> aggregate) : base(aggregate)
        {
            _fpdsService = fpdsService;
            _fpdsService.RequestParameters.Add("category", "CWAD");
        }

        [HttpGet(), Route("Postings")]
        [Produces("application/json")]
        public async Task<ActionResult<FPDSResponseMessage<Posting>>> Get([FromQuery] PostingRequest request)
        {
            try
            {
                var validator = new ParameterValidator(_fpdsServiceConfigs.Value, _logger);
                validator.ValidateClientNameNationalIdController(request.ClientName, request.NationalID, "postings");

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

                return new FPDSResponseMessage<Posting> { ErrorMessage = ex.Message, ErrorOccurred = true, DebugInfo = ex.ToString() };
            }
        }

        [HttpGet()]
        [Route("Postings/{clientName}/{nationalId}")]
        [Route("Postings/{clientName}/{nationalId}/{startDate}/{endDate}")]
        [Route("api/Postings/1.0/{clientName}/{nationalId}")]
        [Route("api/Postings/2.0/{clientName}/{nationalId}")]
        [Route("CCWF/Postings/1.0/{clientName}/{nationalId}")]
        [Route("CCWF/Postings/2.0/{clientName}/{nationalId}")]
        [Produces("application/json")]
        public async Task<ActionResult<FPDSResponseMessage<Posting>>> Get([FromRoute] string clientName,
                                                                          [FromRoute] string nationalId,
                                                                          [FromRoute] string startDate,
                                                                          [FromRoute] string endDate)
        {
            try
            {
                var validator = new ParameterValidator(_fpdsServiceConfigs.Value, _logger);
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

                return new FPDSResponseMessage<Posting> { ErrorMessage = ex.Message, ErrorOccurred = true, DebugInfo = ex.ToString() };
            }
        }
    }
}
