using Microsoft.Extensions.Options.Fakes;
using Microsoft.QualityTools.Testing.Fakes;
using Microsoft.QualityTools.Testing.Fakes.Stubs;
using System.Net.Http.Fakes;
using va_veis_healthdatarepo.Models;
using va_veis_healthdatarepo.Models.Entities;
using va_veis_healthdatarepo.Services;
using va_veis_healthdatarepo.Utilities.Fakes;

namespace va_veis_healthdatarepo_tests
{
    public class PWSDataServiceTests : HealthDataRepoTestsBase
    {
        [Fact]
        public void GetAppointmentsAsyncTest()
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
                        BaseAddress = new Uri("http://www.test.com/appointment")
                    };
                };

                var appSettings = new StubIOptions<PathwaySettings>()
                {
                    InstanceBehavior = StubBehaviors.CurrentProxy,
                    ValueGet = () =>
                    {
                        return new PathwaySettings()
                        {
                            BaseUrl = "http://www.test.com",
                            PWSEndpointURL = "appointment",
                            PWSNameSpace = "test",
                            PWSNameSpacePrefix = "test"
                        };
                    }
                };

                var service = new PWSDataService<PWSAppointment>(appSettings.ValueGet(), Logger);

                var resp = service.GetAppointmentsAsync("Test", "test", "2023-12-01", "2023-12-02");

                Assert.NotNull(resp);
                Assert.True(resp.Result.Data.Count() > 0);
            }
        }

        [Fact]
        public void GetNonVetRecordAsyncTest()
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
                        BaseAddress = new Uri("http://www.test.com/nonvet")
                    };
                };

                var appSettings = new StubIOptions<PathwaySettings>()
                {
                    InstanceBehavior = StubBehaviors.CurrentProxy,
                    ValueGet = () =>
                    {
                        return new PathwaySettings()
                        {
                            BaseUrl = "http://www.test.com",
                            PWSEndpointURL = "nonvet",
                            PWSNameSpace = "test",
                            PWSNameSpacePrefix = "test"
                        };
                    }
                };

                var service = new PWSDataService<NonVeteranEmployeeData>(appSettings.ValueGet(), Logger);

                var resp = service.GetNonVetRecordAsync("Test", new List<string> { "Test" });

                Assert.NotNull(resp);
                Assert.True(resp.Result.Data.Count() > 0);
            }
        }
    }
}
