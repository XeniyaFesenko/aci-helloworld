using va_veis_healthdatarepo.Models;
using Microsoft.Extensions.Options;

namespace va_veis_healthdatarepo.Services.Interfaces
{
        public interface IDependencyAggregate<T> where T : class
        {
        IOptions<Settings> _cdsServiceConfigs { get; }
        IOptions<FPDS_Settings> _fpdsServiceConfigs { get; }
        IOptions<PathwaySettings> _ipwServiceConfigs { get; }

        ILogger<T> _logger { get; }
        IHttpClientFactory _client { get; }

    }

    public class DependencyAggregate<T> : IDependencyAggregate<T> where T : class
    {
       
        public DependencyAggregate(IOptions<FPDS_Settings> fpdsServiceConfigs, IOptions<PathwaySettings> ipwServiceConfigs, IOptions<Settings> cdsServiceConfigs, ILogger<T> logger, IHttpClientFactory httpClientFactory)
        {
            _cdsServiceConfigs = cdsServiceConfigs;
            _fpdsServiceConfigs = fpdsServiceConfigs;
            _ipwServiceConfigs = ipwServiceConfigs;
            _logger = logger;
            _client = httpClientFactory;        
        }

        public ILogger<T> _logger { get; }
        public IHttpClientFactory _client { get; }
        public IOptions<Settings> _cdsServiceConfigs { get; }
        public IOptions<FPDS_Settings> _fpdsServiceConfigs { get; }
        public IOptions<PathwaySettings> _ipwServiceConfigs { get; }
    }

}
