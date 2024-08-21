using Microsoft.QualityTools.Testing.Fakes;
using System.Net.Http.Fakes;
using va_veis_healthdatarepo.Models;
using va_veis_healthdatarepo.Models.Entities;
using va_veis_healthdatarepo.Services;
using va_veis_healthdatarepo.Utilities.Fakes;

namespace va_veis_healthdatarepo_tests
{
    public class CDSServiceTests : HealthDataRepoTestsBase
    {
        [Fact]
        public void GetAlertDataDeserializationTest()
        {
            using (ShimsContext.Create())
            {
                ShimHttpClient.AllInstances.SendAsyncHttpRequestMessage = (u, s) =>
                {
                    return GetResponseMessage(s.RequestUri.PathAndQuery);
                };

                ShimRequestUtilities.AllInstances.CreateHttpClientStringString = (u, s, s2) =>
                {
                    return new StubHttpClient()
                    {
                        BaseAddress = new Uri("http://www.test.com/alert")
                    };
                };

                Settings.ValueGet = () =>
                {
                    return new Settings
                    {
                        BaseUrl = "http://www.test.com/alert",
                        EndPoint = "test"
                    };
                };

                var service = new CDSDataService<Alert>(Settings.ValueGet(), Logger);

                var resp = service.GetAlertDataAsync("Test", Guid.NewGuid(), "test");

                Assert.NotNull(resp);
                Assert.True(resp.Result.Data.Count() > 0);
            }
        }
    }
}