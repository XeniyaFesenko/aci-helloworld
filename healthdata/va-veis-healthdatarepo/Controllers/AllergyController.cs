using Microsoft.AspNetCore.Mvc;
using va_veis_healthdatarepo.Extentions;
using va_veis_healthdatarepo.Models.Entities;
using va_veis_healthdatarepo.Models.Requests;
using va_veis_healthdatarepo.Models.Responses;
using va_veis_healthdatarepo.Services.Interfaces;
using va_veis_healthdatarepo.Utilities;

namespace va_veis_healthdatarepo.Controllers
{
    /// <summary>
    /// Allergy Controller to get allergy data from HDR
    /// </summary>
    public class AllergyController(ICDSDataService<Allergy> cdsDataService, IDependencyAggregate<AllergyController> aggregate) : BaseController<AllergyController>(aggregate)
    {
        private readonly ICDSDataService<Allergy> _cdsDataService = cdsDataService;

        /// <summary>
        /// GET method to get allergy data
        /// </summary>
        /// <param name="clientName">require client name</param>
        /// <param name="nationalId">require veteran national ID</param>
        /// <returns>Return allergy data in JSON format</returns>
        [HttpGet(), Route("api/Allergy/1.0/json/{clientName}/{nationalId}")]
        [Produces("application/json")]
        public async Task<ActionResult<CDSResponseMessage<CDSAllergy>>> Get(string clientName, string nationalId)
        {
            try
            {
                //check for valid client name and national id
                var validator = new ParameterValidator(_cdsServiceConfigs.Value, _logger);
                validator.ValidateClientNameNationalIdController(clientName, nationalId, "allergy");
                //get allergy data from cds service
                var response = await Task.FromResult(_cdsDataService
                .GetAllergyDataAsync(clientName, nationalId)
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

                return new CDSResponseMessage<CDSAllergy> { ErrorMessage = ex.Message, ErrorOccurred = true, Status = "Error", DebugInfo = ex.ToString() };
            }
        }

        /// <summary>
        /// GET method to get allergy data
        /// </summary>
        /// <param name="request">user should provide client name and national ID in request parameter"</param>
        /// <returns>Return allergy data in JSON format</returns>
        [HttpGet(), Route("Allergy")]
        [Produces("application/json")]
        public async Task<ActionResult<CDSResponseMessage<CDSAllergy>>> Get([FromQuery] AllergyDataRequest request)
        {
            try
            {
                //check for valid client name and national id
                var validator = new ParameterValidator(_cdsServiceConfigs.Value, _logger);
                validator.ValidateClientNameNationalIdController(request.ClientName, request.NationalId, "allergy");

                var startDate = request.StartDate.ToDateTimeString(ApiDateFormat);
                var endDate = request.EndDate.ToDateTimeString(ApiDateFormat);

                //get vital data from cds service
                var response = await Task.FromResult(_cdsDataService
                    .GetAllergyDataAsync(request.ClientName, request.NationalId, startDate, endDate)
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

                return new CDSResponseMessage<CDSAllergy> { ErrorMessage = ex.Message, ErrorOccurred = true, Status = "Error", DebugInfo = ex.ToString() };
            }
        }
    }
}
