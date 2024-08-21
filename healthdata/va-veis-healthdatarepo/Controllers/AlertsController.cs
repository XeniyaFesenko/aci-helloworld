using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;
using va_veis_healthdatarepo.Models.Entities;
using va_veis_healthdatarepo.Models.Requests;
using va_veis_healthdatarepo.Models.Responses;
using va_veis_healthdatarepo.Services.Interfaces;
using va_veis_healthdatarepo.Utilities;

namespace va_veis_healthdatarepo.Controllers
{
    public class AlertsController : BaseController<AlertsController>
    {
        private readonly ICDSDataService<Alert> _cdsService;

        public AlertsController(ICDSDataService<Alert> cDSDataService, IDependencyAggregate<AlertsController> aggregate) : base(aggregate)
        {
            _cdsService = cDSDataService;
        }

        // POST: api/GetAlerts
        [HttpPost(), Route("Alerts")]
        [Produces("application/json")]
        public async Task<ActionResult<CDSResponseMessage<CDSAlert>>> Post([FromBody] AlertsRequest request)
        {
            _logger.LogDebug("Enter Alerts Controller");
            try
            {
                var validator = new ParameterValidator(_logger);
                validator.ValidateClientName(request.ClientName, "alerts", null);

                var response = new CDSResponseMessage<CDSAlert>();
                var requestTasks = new List<Task<CDSResponseMessage<CDSAlert>>>();
                var requestUsers = request.Users.Take(10);
                var users = string.Empty;
                var skipCount = 10;

                do
                {
                    int start, end = 0;
                    users = XElement.Parse(Serialization.XmlSerializeInstance(requestUsers.ToList())).ToString();
                    start = users.IndexOf("<User>");
                    end = users.IndexOf("</ArrayOfUser");
                    users = start > 0 ? users.Substring(start, end - start) : "";
                    users = users.Replace("User", "user").Replace("Identity", "identity").Replace("AssigningFacility", "assigningFacility");

                    if (!string.IsNullOrEmpty(users))
                    {
                        requestTasks.Add(Task.FromResult(_cdsService
                            .GetAlertDataAsync(request.ClientName, request.MessageId, users)
                            .ConfigureAwait(false)
                            .GetAwaiter()
                            .GetResult())); 
                    }

                    requestUsers = request.Users.Skip(skipCount).Take(10);
                    skipCount += 10;
                    if (skipCount > request.Users.Count) skipCount = request.Users.Count;
                } while (requestUsers.Count() > 0);

                var resp = await Task.WhenAll(requestTasks);

                resp.ToList().ForEach(r =>
                {
                    response.Data.AddRange(r.Data);
                    response.ErrorMessage = r.ErrorMessage;
                    response.Status = r.Status;
                    response.DebugInfo = r.DebugInfo;
                });

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    string.Format(
                    "ParameterValidator.ValidateClientNameNationalIdController(): Error Msg:{0}", ex.Message), ex);

                return new CDSResponseMessage<CDSAlert> { ErrorMessage = ex.Message, ErrorOccurred = true, DebugInfo = ex.ToString() };
            }
            finally
            {
                _logger.LogDebug("Exit Alerts Controller");
            }
        }
    }
}
