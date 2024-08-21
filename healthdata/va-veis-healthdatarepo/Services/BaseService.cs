using va_veis_healthdatarepo.Models;
using Microsoft.Extensions.Options;
using va_veis_healthdatarepo.Services.Interfaces;

namespace va_veis_healthdatarepo.Services
{
    public abstract class BaseService<T> where T : class
    {    
        public BaseService(IDependencyAggregate<T> aggregate)
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
