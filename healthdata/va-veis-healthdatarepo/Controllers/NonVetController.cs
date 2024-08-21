using Microsoft.AspNetCore.Mvc;
using va_veis_healthdatarepo.Models.Entities;
using va_veis_healthdatarepo.Models.Requests;
using va_veis_healthdatarepo.Models.Responses;
using va_veis_healthdatarepo.Services.Interfaces;
using va_veis_healthdatarepo.Utilities;

namespace va_veis_healthdatarepo.Controllers
{
    public class NonVetController : BaseController<NonVetController>
    {


        private readonly IPWSDataService<NonVeteranEmployeeData> _pathwaysData;

        public NonVetController(IPWSDataService<NonVeteranEmployeeData> pWSDataService, IDependencyAggregate<NonVetController> aggregate) : base(aggregate)
        {
            //_logger = logger;
            //_appServiceConfigs = appServiceConfigs;
            _pathwaysData = pWSDataService;
        }


        // POST api/GetNonVetEmployeeData
        [HttpPost(), Route("api/Pathways/NonVet")]
        [Produces("application/json")]
        public async Task<ActionResult<PWSResponseMessage<NonVeteranEmployeeData>>> Get([FromBody] NonVetEmployeeRequest request)
        {
            try
            {
                var validator = new ParameterValidator(_ipwServiceConfigs.Value, _logger);
                var clientDetail = validator.ValidateClientNameNationalIdController(request.ClientName, request.NationalIds.FirstOrDefault(), "nonvet");

                var response = await Task.FromResult((PWSResponseMessage<NonVeteranEmployeeData>)_pathwaysData
                    .GetNonVetRecordAsync(request.ClientName, request.NationalIds)
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

                return new PWSResponseMessage<NonVeteranEmployeeData> { ErrorMessage = ex.Message, ErrorOccurred = true, DebugInfo = ex.ToString() };
            }
        }
    }
}
