using Microsoft.AspNetCore.Mvc;
using va_veis_healthdatarepo.Models.Entities;
using va_veis_healthdatarepo.Models.Requests;
using va_veis_healthdatarepo.Models.Responses;
using va_veis_healthdatarepo.Services.Interfaces;
using va_veis_healthdatarepo.Utilities;

namespace va_veis_healthdatarepo.Controllers
{
    public class FlagsController : BaseController<FlagsController>
    {

        private readonly IFPDSDataService<Flag> _fpdsService;

        public FlagsController(IFPDSDataService<Flag> fpdsService, IDependencyAggregate<FlagsController> aggregate) : base(aggregate)
        {
            _fpdsService = fpdsService;
        }

        // GET api/GetFlags/ftpCRM/1012592732?noFilter=true
        [HttpGet(), Route("api/Flags")]
        [Produces("application/json")]
        public async Task<ActionResult<FPDSResponseMessage<Flag>>> Get([FromQuery] FlagsRequest request)
        {
            try
            {
                var validator = new ParameterValidator(_fpdsServiceConfigs.Value, _logger);
                validator.ValidateClientNameNationalIdController(request.ClientName, request.NationalID, "flags");

                var response = await Task.FromResult((FPDSResponseMessage<Flag>)_fpdsService
                    .GetFlagDataAsync(request.ClientName, request.NationalID, RequestSessionId)
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

                return new FPDSResponseMessage<Flag> { ErrorMessage = ex.Message, ErrorOccurred = true, DebugInfo = ex.ToString() };
            }
        }

        // GET api/GetFlags/ftpCRM/1012592732?noFilter=true
        [HttpGet()]
        [Route("api/Flags/1.0/{clientName}/{nationalId}")]
        [Produces("application/json")]
        public async Task<ActionResult<FPDSResponseMessage<Flag>>> Get([FromRoute] string clientName, [FromRoute] string nationalId)
        {
            try
            {
                var validator = new ParameterValidator(_fpdsServiceConfigs.Value, _logger);
                validator.ValidateClientNameNationalIdController(clientName, nationalId, "flags");

                var response = await Task.FromResult((FPDSResponseMessage<Flag>)_fpdsService
                    .GetFlagDataAsync(clientName, nationalId, RequestSessionId)
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

                return new FPDSResponseMessage<Flag> { ErrorMessage = ex.Message, ErrorOccurred = true, DebugInfo = ex.ToString() };
            }
        }
    }
}
