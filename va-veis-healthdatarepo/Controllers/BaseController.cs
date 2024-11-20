using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using va_veis_healthdatarepo.Models;
using va_veis_healthdatarepo.Services.Interfaces;

namespace va_veis_healthdatarepo.Controllers
{
     public abstract class BaseController<T> : ControllerBase where T : BaseController<T>
    {
        public const string ApiDateFormat = "yyyy-MM-dd";
        public string RequestSessionId { get; set; } = Guid.NewGuid().ToString();
        public BaseController(IDependencyAggregate<T> aggregate)
        {
            _logger = aggregate._logger;
            _cdsServiceConfigs = aggregate._cdsServiceConfigs;
            _fpdsServiceConfigs = aggregate._fpdsServiceConfigs;
            _ipwServiceConfigs = aggregate._ipwServiceConfigs;
            _httpClientFactory = aggregate._client;
        }

        protected ILogger<T> _logger { get; }
        protected IHttpClientFactory _httpClientFactory { get; }
        protected IOptions<Settings> _cdsServiceConfigs { get; }
        protected IOptions<FPDS_Settings> _fpdsServiceConfigs { get; }
        protected IOptions<PathwaySettings> _ipwServiceConfigs { get; }
    }

}
