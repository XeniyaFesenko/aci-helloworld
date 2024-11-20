using Microsoft.AspNetCore.Mvc;
using va_veis_healthdatarepo.Extentions;
using va_veis_healthdatarepo.Models.Entities;
using va_veis_healthdatarepo.Models.Requests;
using va_veis_healthdatarepo.Models.Responses;
using va_veis_healthdatarepo.Services.Interfaces;
using va_veis_healthdatarepo.Utilities;

namespace va_veis_healthdatarepo.Controllers
{
    public class LabsOrdersController : BaseController<LabsOrdersController>
    {

        private readonly IFPDSDataService<LabOrders> _fpdsService;
        public LabsOrdersController(IFPDSDataService<LabOrders> fpdsService, IDependencyAggregate<LabsOrdersController> aggregate) : base(aggregate)
        {
            _fpdsService = fpdsService;
        }

        // GET api/GetLabsOrders/FtPCRM/1012592732?noFilter=true
        [HttpGet(), Route("LabsOrders")]
        [Produces("application/json")]
        public async Task<ActionResult<FPDSResponseMessage<LabOrders>>> Get([FromQuery] LabsOrdersRequest request)
        {
            try
            {
                var validator = new ParameterValidator(_fpdsServiceConfigs.Value, _logger);
                validator.ValidateClientNameNationalIdController(request.ClientName, request.NationalID, "labsorders");

                if (string.IsNullOrEmpty(request.StartDate))
                {
                    request.StartDate = request.StartDate.ToDateTimeString(ApiDateFormat, DateTime.Now.AddMonths(-12));
                }
                else
                {
                    request.StartDate = request.StartDate.ToDateTimeString(ApiDateFormat, request.StartDate);
                }
                if (string.IsNullOrEmpty(request.EndDate))
                {
                    request.EndDate = request.EndDate.ToDateTimeString(ApiDateFormat, DateTime.Now);
                }
                else
                {
                    request.EndDate = request.EndDate.ToDateTimeString(ApiDateFormat, request.EndDate);
                }

                var response = await Task.FromResult((FPDSResponseMessage<LabOrders>)_fpdsService
                    .GetLabDataAsync(request.ClientName, request.NationalID, RequestSessionId, request.StartDate, request.EndDate)
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

                return new FPDSResponseMessage<LabOrders> { ErrorMessage = ex.Message, ErrorOccurred = true, DebugInfo = ex.ToString() };
            }
        }

        // GET api/GetLabsOrders/FtPCRM/1012592732?noFilter=true
        [HttpGet()]
        [Route("LabsOrders/1.0/{clientName}/{nationalId}/{startDate}/{endDate}")]
        [Produces("application/json")]
        public async Task<ActionResult<FPDSResponseMessage<LabOrders>>> Get([FromRoute] string clientName, [FromRoute] string nationalId, [FromRoute] string startDate, [FromRoute]  string endDate)
        {
            try
            {
                var validator = new ParameterValidator(_fpdsServiceConfigs.Value, _logger);
                validator.ValidateClientNameNationalIdController(clientName, nationalId, "labsorders");

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

                var response = await Task.FromResult((FPDSResponseMessage<LabOrders>)_fpdsService
                    .GetLabDataAsync(clientName, nationalId, RequestSessionId, startDate, endDate)
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

                return new FPDSResponseMessage<LabOrders> { ErrorMessage = ex.Message, ErrorOccurred = true, DebugInfo = ex.ToString() };
            }
        }
    }
}
