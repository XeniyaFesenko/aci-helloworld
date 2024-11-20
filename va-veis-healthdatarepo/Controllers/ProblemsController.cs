using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using va_veis_healthdatarepo.Mappers;
using va_veis_healthdatarepo.Models.Entities;
using va_veis_healthdatarepo.Models.Requests;
using va_veis_healthdatarepo.Models.Responses;
using va_veis_healthdatarepo.Services.Interfaces;
using va_veis_healthdatarepo.Utilities;

namespace va_veis_healthdatarepo.Controllers
{
    /// <summary>
    /// Problem Controller to get veteran problem data from HDR
    /// </summary>
    public class ProblemsController : BaseController<ProblemsController>
    {
        private readonly IFPDSDataService<Problem> _fpdsService;
        public ProblemsController(IFPDSDataService<Problem> fpdsService, IDependencyAggregate<ProblemsController> aggregate) : base(aggregate)
        {
            _fpdsService = fpdsService;
        }

        /// <summary>
        /// GET method to get veteran problem data
        /// </summary>
        /// <param name="request">user should provide client name and national ID in request parameter"</param>
        /// <returns>Return veteran problem data in JSON format</returns>
        [HttpGet(), Route("Problems")]
        [Produces("application/json")]
        public async Task<ActionResult<FPDSResponseMessage<Problem>>> Get([FromQuery] ProblemRequest request)
        {
            try
            {
                //check for valid client name and national id
                var validator = new ParameterValidator(_fpdsServiceConfigs.Value, _logger);
                validator.ValidateClientNameNationalIdController(request.ClientName, request.NationalID, "problems");
                //get problem data from fpds service
                var response = await Task.FromResult(_fpdsService
                    .GetDataAsync(request.ClientName, request.NationalID)
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

                return new FPDSResponseMessage<Problem> { ErrorMessage = ex.Message, ErrorOccurred = true, DebugInfo = ex.ToString() };
            }
        }

        /// <summary>
        /// Legacy GET method to get veteran problem data
        /// </summary>
        /// <param name="clientName">require client name</param>
        /// <param name="nationalId">require veteran national ID</param>
        /// <returns>return problem data in JSON format</returns>
        [HttpGet]
        [Route("api/Problems/1.0/{clientName}/{nationalId}")]
        public async Task<ActionResult<FPDSResponseMessage<Problem>>> Get([FromRoute] string clientName, [FromRoute] string nationalId)
        {
            try
            {
                //check for valid client name and national id
                var validator = new ParameterValidator(_fpdsServiceConfigs.Value, _logger);
                validator.ValidateClientNameNationalIdController(clientName, nationalId, "problems");
                //get problem data from fpds service
                var response = await Task.FromResult(_fpdsService
                    .GetDataAsync(clientName, nationalId)
                    .ConfigureAwait(false)
                    .GetAwaiter()
                    .GetResult());

                if(response.Data != null && response.Data.Count > 0)
                {
                    response.Data = TransformProblems(response.Data);
                }

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    string.Format(
                    "ParameterValidator.ValidateClientNameNationalIdController(): Error Msg:{0}", ex.Message), ex);

                return new FPDSResponseMessage<Problem> { ErrorMessage = ex.Message, ErrorOccurred = true, DebugInfo = ex.ToString() };
            }
        }

        private List<Problem> TransformProblems(List<Problem> problemList)
        {
            if (problemList == null) throw new ArgumentNullException("problemList");

            List<Problem> result = new List<Problem>();
            string ACTIVE_STATUS = "ACTIVE";

            foreach (var problem in problemList)
            {
                if (!string.IsNullOrEmpty(problem.Entered))
                {
                    var newDate = DateTransformer.ParseDateFromString(problem.Entered);
                    problem.EnteredDate = newDate;
                }
                else
                {
                    problem.EnteredDate = DateTransformer.DefaultConvertedDate;
                }
                if(problem.StatusName.ToUpper() == ACTIVE_STATUS)
                {
                    result.Add(problem);
                }
            }
            return result;
        }
        
    }
}
