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
    public class FPDSDataServiceTests : HealthDataRepoTestsBase
    {
        [Fact]
        public async void GetDataAsyncTest()
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
                        BaseAddress = new Uri("http://www.test.com")
                    };
                };

                var appSettings = new StubIOptions<FPDS_Settings>()
                {
                    InstanceBehavior = StubBehaviors.CurrentProxy,
                    ValueGet = () =>
                    {
                        return new FPDS_Settings()
                        {
                            EndPoint = "http://www.test.com",
                            FPDSEndpointURL = "http://www.test.com",
                            FPDSParamFilterId = "1",
                            FPDSParamTemplateId = "1",
                            FPDSParamText = "1"
                        };
                    }
                };

                var service = new FPDSDataService<ConsultDocument>(appSettings.ValueGet(), Logger);

                var resp = await service.GetDataAsync("Test", "test");

                Assert.NotNull(resp);
            }
        }

        [Fact]
        public async void GetFlagDataAsyncTest()
        {
            using (Context)
            {
                ShimHttpClient.AllInstances.SendAsyncHttpRequestMessage = (u, s) =>
                {
                    return GetResponseMessage(s.RequestUri.PathAndQuery);
                };

                ShimRequestUtilities.AllInstances.CreateHttpClientStringString = (u, s, s2) =>
                {
                    return new StubHttpClient()
                    {
                        BaseAddress = new Uri("http://www.test.com")
                    };
                };

                var appSettings = new StubIOptions<FPDS_Settings>()
                {
                    InstanceBehavior = StubBehaviors.CurrentProxy,
                    ValueGet = () =>
                    {
                        return new FPDS_Settings()
                        {
                            EndPoint = "http://www.test.com",
                            FPDSEndpointURL = "http://www.test.com",
                            FPDSParamFilterId = "1",
                            FPDSParamTemplateId = "1",
                            FPDSParamText = "1"
                        };
                    }
                };

                var service = new FPDSDataService<Flag>(appSettings.ValueGet(), Logger);

                var resp = await service.GetFlagDataAsync("Test", "test", "test");

                Assert.NotNull(resp);
                Assert.True(resp.Data.Count() > 0);
            }
        }

        [Fact]
        public async void GetLabDataAsyncTest()
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
                        BaseAddress = new Uri("http://www.test.com")
                    };
                };

                var appSettings = new StubIOptions<FPDS_Settings>()
                {
                    InstanceBehavior = StubBehaviors.CurrentProxy,
                    ValueGet = () =>
                    {
                        return new FPDS_Settings()
                        {
                            EndPoint = "http://www.test.com",
                            FPDSEndpointURL = "http://www.test.com",
                            FPDSParamFilterId = "1",
                            FPDSParamTemplateId = "1",
                            FPDSParamText = "1"
                        };
                    }
                };

                var service = new FPDSDataService<LabOrders>(appSettings.ValueGet(), Logger);

                var resp = await service.GetLabDataAsync("Test", "test", "test", "test", "test");

                Assert.NotNull(resp);
                Assert.True(resp.Data.Count() > 0);
            }
        }

        [Fact]
        public async void GetLabDataByTypeAsyncTest()
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
                        BaseAddress = new Uri("http://www.test.com")
                    };
                };

                var appSettings = new StubIOptions<FPDS_Settings>()
                {
                    InstanceBehavior = StubBehaviors.CurrentProxy,
                    ValueGet = () =>
                    {
                        return new FPDS_Settings()
                        {
                            EndPoint = "http://www.test.com",
                            FPDSEndpointURL = "http://www.test.com",
                            FPDSParamFilterId = "1",
                            FPDSParamTemplateId = "1",
                            FPDSParamText = "1"
                        };
                    }
                };

                var service = new FPDSDataService<LabOrders>(appSettings.ValueGet(), Logger);

                var resp = await service.GetDataByTypeAsync<Lab>("Test", "test", "test", "test", "test");

                Assert.NotNull(resp);
                Assert.True(resp.Data.Count() > 0);
            }
        }

        [Fact]
        public async void GetMedicationsDataAsyncTest()
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
                        BaseAddress = new Uri("http://www.test.com")
                    };
                };

                var appSettings = new StubIOptions<FPDS_Settings>()
                {
                    InstanceBehavior = StubBehaviors.CurrentProxy,
                    ValueGet = () =>
                    {
                        return new FPDS_Settings()
                        {
                            EndPoint = "http://www.test.com",
                            FPDSEndpointURL = "http://www.test.com",
                            FPDSParamFilterId = "1",
                            FPDSParamTemplateId = "1",
                            FPDSParamText = "1"
                        };
                    }
                };

                var service = new FPDSDataService<Medication>(appSettings.ValueGet(), Logger);

                var resp = await service.GetDataAsync("Test", "test");

                Assert.NotNull(resp);
                Assert.True(resp.Data.Count() > 0);
            }
        }

        [Fact]
        public async void GetNotesDataAsyncTest()
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
                        BaseAddress = new Uri("http://www.test.com")
                    };
                };

                var appSettings = new StubIOptions<FPDS_Settings>()
                {
                    InstanceBehavior = StubBehaviors.CurrentProxy,
                    ValueGet = () =>
                    {
                        return new FPDS_Settings()
                        {
                            EndPoint = "http://www.test.com",
                            FPDSEndpointURL = "http://www.test.com",
                            FPDSParamFilterId = "1",
                            FPDSParamTemplateId = "1",
                            FPDSParamText = "1"
                        };
                    }
                };

                var service = new FPDSDataService<Note>(appSettings.ValueGet(), Logger);

                var resp = await service.GetDataAsync("Test", "test");

                Assert.NotNull(resp);
                Assert.True(resp.Data.Count() > 0);
            }
        }

        [Fact]
        public async void GetOrderDataByTypeAsyncTest()
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
                        BaseAddress = new Uri("http://www.test.com")
                    };
                };

                var appSettings = new StubIOptions<FPDS_Settings>()
                {
                    InstanceBehavior = StubBehaviors.CurrentProxy,
                    ValueGet = () =>
                    {
                        return new FPDS_Settings()
                        {
                            EndPoint = "http://www.test.com",
                            FPDSEndpointURL = "http://www.test.com",
                            FPDSParamFilterId = "1",
                            FPDSParamTemplateId = "1",
                            FPDSParamText = "1"
                        };
                    }
                };

                var service = new FPDSDataService<LabOrders>(appSettings.ValueGet(), Logger);

                var resp = await service.GetDataByTypeAsync<Order>("Test", "test", "test", "test", "test");

                Assert.NotNull(resp);
                Assert.True(resp.Data.Count() > 0);
            }
        }

        [Fact]
        public async void GetPostingsDataAsyncTest()
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
                        BaseAddress = new Uri("http://www.test.com")
                    };
                };

                var appSettings = new StubIOptions<FPDS_Settings>()
                {
                    InstanceBehavior = StubBehaviors.CurrentProxy,
                    ValueGet = () =>
                    {
                        return new FPDS_Settings()
                        {
                            EndPoint = "http://www.test.com",
                            FPDSEndpointURL = "http://www.test.com",
                            FPDSParamFilterId = "1",
                            FPDSParamTemplateId = "1",
                            FPDSParamText = "1"
                        };
                    }
                };

                var service = new FPDSDataService<Posting>(appSettings.ValueGet(), Logger);

                var resp = await service.GetDataAsync("Test", "test");

                Assert.NotNull(resp);
                Assert.True(resp.Data.Count() > 0);
            }
        }

        [Fact]
        public async void GetRadiologyReportsDataAsyncTest()
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
                        BaseAddress = new Uri("http://www.test.com")
                    };
                };

                var appSettings = new StubIOptions<FPDS_Settings>()
                {
                    InstanceBehavior = StubBehaviors.CurrentProxy,
                    ValueGet = () =>
                    {
                        return new FPDS_Settings()
                        {
                            EndPoint = "http://www.test.com",
                            FPDSEndpointURL = "http://www.test.com",
                            FPDSParamFilterId = "1",
                            FPDSParamTemplateId = "1",
                            FPDSParamText = "1"
                        };
                    }
                };

                var service = new FPDSDataService<RadiologyDocument>(appSettings.ValueGet(), Logger);

                var resp = await service.GetDataAsync("Test", "test");

                Assert.NotNull(resp);
                Assert.True(resp.Data.Count() > 0);
            }
        }
    }
}
