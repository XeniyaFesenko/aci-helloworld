using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;
using va_veis_healthdatarepo.Models;
using va_veis_healthdatarepo.Models.Entities;

namespace va_veis_healthdatarepo.Utilities
{
    public class ParameterValidator
    {
        private Settings _appServiceConfigs;
        private readonly ILogger _logger;

        public ParameterValidator(ILogger logger)
        {
            _logger = logger;
        }

        public ParameterValidator(Settings appServiceConfigs, ILogger logger)
        {
            _appServiceConfigs = appServiceConfigs;
            _logger = logger;
        }

        public CustomViewDetail ValidateClientNameNationalIdController(string clientName, string nationalId, string ControllerName, string message = null)
        {
            ValidateNationalId(nationalId, message);
            return ValidateClientName(clientName.ToUpper(), ControllerName.ToLower(), message);
        }

        public void ValidateClientNameAndNationalId(string clientName, string nationalId, string message = null)
        {
            ValidateClientName(clientName, message);
            ValidateNationalId(nationalId, message);
        }

        public ClientData ValidateClientName(string clientName, string message = null)
        {
            string fileContents = string.Empty;

            string cachedFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Configuration", "ControllerClientNameConfiguration.json");
            _logger.LogDebug($"Load Controller Client Name Configuration file using path: {cachedFilePath}");

            try
            {
                fileContents = File.ReadAllText(cachedFilePath);

                JsonSerializerOptions options = new JsonSerializerOptions() { ReferenceHandler = ReferenceHandler.Preserve };

                var clientData = JsonSerializer.Deserialize<ClientData>(fileContents, options);

                var checkClientName = clientData != null && clientData.ClientNames != null && !clientData.ClientNames.Count.Equals(0) ? clientData.ClientNames.Where(vals => vals.Values.Contains(clientName.ToUpper())) : new List<ClientNames>();
                if (checkClientName.Count() == 0)
                {
                    throw new WebException(message ?? "Invalid HDR URL parameters. Invalid client name.");
                }
                return clientData;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to load Controller Client Name Configuration File using path: {cachedFilePath}");
                throw;
            }
        }

        public CustomViewDetail ValidateClientName(string clientName, string controllerName, string message = null)
        {
            if (string.IsNullOrEmpty(clientName))
            {
                throw new WebException(message ?? "Invalid HDR URL parameters. Invalid client name.");
            }
            else
            {
                ClientData clientdata = ValidateClientName(clientName, message);
                var ctrlNames = clientdata.Controllers.Select(ctrl => ctrl.Name);

                var ctrlMatch = ctrlNames.Where(ctrl => ctrl.Contains(controllerName.ToLower()));
                if (ctrlMatch != null)
                {
                    var refController = clientdata.Controllers.Where(nm => nm.Name == controllerName.ToLower()).First();
                    var match = refController.ValidClientGrp.Values.Where(cname => cname.Contains(clientName.ToUpper()));
                    if (match.Count() > 0)
                    {
                        if (refController.CustomViews.CustomViewDetail.Count > 0)
                        {
                            var custView = refController.CustomViews.CustomViewDetail.Where(assoc => assoc.AssocClientGrp.Values.Contains(clientName.ToUpper())).First();
                            return custView;
                        }
                        else
                        {
                            return null;
                        }
                    }
                    else
                    {
                        _logger.LogError(message ?? $"Invalid HDR URL parameters. Invalid client name: {clientName}.");
                        throw new WebException(message ?? "Invalid HDR URL parameters. Invalid client name.");
                    }
                }
                else
                {
                    _logger.LogError(message ?? $"The API controller {controllerName} is not configured for this validation.");
                    throw new WebException(string.Format(message ?? "The API controller {0} is not configured for this validation.", controllerName.ToUpper()));
                }
            }
        }

        public void ValidateNationalId(string nationalId, string message = null)
        {
            if (string.IsNullOrEmpty(nationalId))
            {
                _logger.LogError(message ?? "Invalid HDR URL parameters. Invalid nationalId.");
                throw new WebException(message ?? "Invalid HDR URL parameters. Invalid nationalId.");
            }
        }
    }
}
