using Microsoft.AspNetCore.Mvc;
using va_veis_healthdatarepo.Extentions;
using va_veis_healthdatarepo.Models.Entities;
using va_veis_healthdatarepo.Models.Requests;
using va_veis_healthdatarepo.Models.Responses;
using va_veis_healthdatarepo.Services.Interfaces;
using va_veis_healthdatarepo.Utilities;

namespace va_veis_healthdatarepo.Controllers
{
    public class NotesController : BaseController<NotesController>
    {

        private readonly IFPDSDataService<Note> _fpdsService;

        public NotesController(IFPDSDataService<Note> fpdsService, IDependencyAggregate<NotesController> aggregate) : base(aggregate)
        {
            _fpdsService = fpdsService;
            _fpdsService.RequestParameters.Add("DocumentTypeCode", "PN");
        }

        // GET api/GetNotes/HDR-20560/0000001047710781V477439000000?noFilter=true
        [HttpGet(), Route("Notes")]
        [Produces("application/json")]
        public async Task<ActionResult<FPDSResponseMessage<Note>>> Get([FromQuery] NotesRequest request)
        {
            try
            {
                var validator = new ParameterValidator(_fpdsServiceConfigs.Value, _logger);
                validator.ValidateClientNameNationalIdController(request.ClientName, request.NationalID, "notes");

                request.StartDate = request.StartDate.ToDateTimeString(ApiDateFormat, DateTime.Now.AddMonths(-6));
                request.EndDate = request.EndDate.ToDateTimeString(ApiDateFormat, DateTime.Now);

                var response = await Task.FromResult((FPDSResponseMessage<Note>)_fpdsService
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

                return new FPDSResponseMessage<Note> { ErrorMessage = ex.Message, ErrorOccurred = true, DebugInfo = ex.ToString() };
            }
        }

        //GET api/GetNotes/HDR-20560/0000001047710781V477439000000? noFilter = true
        [HttpGet()]
        [Route("CCWF/Notes/1.0/{clientName}/{nationalId}")]
        [Route("CCWF/Notes/2.0/{clientName}/{nationalId}")]
        [Produces("application/json")]
        public async Task<ActionResult<FPDSResponseMessage<Note>>> Get([FromRoute] string clientName, [FromRoute] string nationalId)
        {
            try
            {
                var validator = new ParameterValidator(_fpdsServiceConfigs.Value, _logger);
                validator.ValidateClientNameNationalIdController(clientName, nationalId, "notes");

                var response = await Task.FromResult((FPDSResponseMessage<Note>)_fpdsService
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

                return new FPDSResponseMessage<Note> { ErrorMessage = ex.Message, ErrorOccurred = true, DebugInfo = ex.ToString() };
            }
        }

        //GET api/GetNotes/HDR-20560/0000001047710781V477439000000? noFilter = true
        [HttpGet()]
        [Route("Notes/{clientName}/{nationalId}/{startDate?}/{endDate?}")]
        [Produces("application/json")]
        public async Task<ActionResult<FPDSResponseMessage<Note>>> Get([FromRoute] string clientName, [FromRoute] string nationalId, [FromRoute] string startDate, [FromRoute] string endDate)
        {
            try
            {
                var validator = new ParameterValidator(_fpdsServiceConfigs.Value, _logger);
                validator.ValidateClientNameNationalIdController(clientName, nationalId, "notes");

                if (string.IsNullOrEmpty(startDate))
                {
                    startDate = startDate.ToDateTimeString(ApiDateFormat, DateTime.Now.AddMonths(-12));
                }
                else
                {
                    startDate = startDate.ToDateTimeString(ApiDateFormat, startDate);
                }
                if (string.IsNullOrEmpty(endDate))
                {
                    endDate = endDate.ToDateTimeString(ApiDateFormat, DateTime.Now);
                }
                else
                {
                    endDate = endDate.ToDateTimeString(ApiDateFormat, endDate);
                }

                var response = await Task.FromResult((FPDSResponseMessage<Note>)_fpdsService
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

                return new FPDSResponseMessage<Note> { ErrorMessage = ex.Message, ErrorOccurred = true, DebugInfo = ex.ToString() };
            }
        }
    }
}
