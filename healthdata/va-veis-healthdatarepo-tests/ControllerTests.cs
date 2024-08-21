using Microsoft.Extensions.Logging.Fakes;
using Microsoft.Extensions.Options.Fakes;
using Microsoft.QualityTools.Testing.Fakes;
using Microsoft.QualityTools.Testing.Fakes.Stubs;
using va_veis_healthdatarepo.Controllers;
using va_veis_healthdatarepo.Models;
using va_veis_healthdatarepo.Models.Entities;
using va_veis_healthdatarepo.Models.Requests;
using va_veis_healthdatarepo.Models.Responses;
using va_veis_healthdatarepo.Services;
using va_veis_healthdatarepo.Services.Fakes;
using va_veis_healthdatarepo.Utilities.Fakes;

namespace va_veis_healthdatarepo_tests
{
    public class ControllerTests : HealthDataRepoTestsBase
    {
        [Fact]
        public void AlertControllerTest()
        {
            var appSettings = new StubIOptions<Settings>()
            {
                InstanceBehavior = StubBehaviors.CurrentProxy,
                ValueGet = () =>
                {
                    return new Settings()
                    {
                        EndPoint = "http://www.test.com"
                    };
                }
            };

            var logger = new StubILogger<AlertsController>();

            using (ShimsContext.Create())
            {
                ShimParameterValidator.AllInstances.ValidateClientNameStringStringString = (v, s1, s2, s3) =>
                {
                    return new CustomViewDetail();
                };

                ShimCDSDataService<Alert>.AllInstances.GetAlertDataAsyncStringGuidString = (svc, s, g, u) =>
                {
                    return Task.FromResult(new CDSResponseMessage<CDSAlert>());
                };

                var controller = new AlertsController(appSettings, logger, new CDSDataService<Alert>(appSettings.ValueGet(), new StubILogger()));

                var resp = Task.FromResult(controller.Post(new AlertsRequest()
                {
                    ClientName = "1",
                    Users = new List<va_veis_healthdatarepo.Models.Common.User>
                    {
                       new va_veis_healthdatarepo.Models.Common.User
                       {
                           AssigningFacility = "test",
                           Identity="test"
                       }
                    }
                })).Result;

                Assert.NotNull(resp.Result.Value);
            }
        }

        [Fact]
        public void AppointmentControllerTest()
        {
            var appSettings = new StubIOptions<PathwaySettings>()
            {
                InstanceBehavior = StubBehaviors.CurrentProxy,
                ValueGet = () =>
                {
                    return new PathwaySettings()
                    {
                        EndPoint = "http://www.test.com"
                    };
                }
            };

            var logger = new StubILogger<AppointmentsController>();

            using (ShimsContext.Create())
            {
                ShimParameterValidator.AllInstances.ValidateClientNameStringStringString = (v, s1, s2, s3) =>
                {
                    return new CustomViewDetail();
                };

                ShimPWSDataService<PWSAppointment>.AllInstances.GetAppointmentsAsyncStringStringStringString = (svc, s1, s2, s3, s4) =>
                {
                    return Task.FromResult(new PWSResponseMessage<PWSAppointment>());
                };

                var controller = new AppointmentsController(appSettings, logger, new PWSDataService<PWSAppointment>(appSettings.ValueGet(), new StubILogger()));

                var resp = Task.FromResult(controller.Get("1", "1")).Result;

                Assert.NotNull(resp.Result.Value);
            }
        }

        [Fact]
        public void AppointmentControllerAppointmentRequestTest()
        {
            var appSettings = new StubIOptions<PathwaySettings>()
            {
                InstanceBehavior = StubBehaviors.CurrentProxy,
                ValueGet = () =>
                {
                    return new PathwaySettings()
                    {
                        EndPoint = "http://www.test.com"
                    };
                }
            };

            var logger = new StubILogger<AppointmentsController>();

            using (ShimsContext.Create())
            {
                ShimParameterValidator.AllInstances.ValidateClientNameStringStringString = (v, s1, s2, s3) =>
                {
                    return new CustomViewDetail();
                };

                ShimPWSDataService<PWSAppointment>.AllInstances.GetAppointmentsAsyncStringStringStringString = (svc, s1, s2, s3, s4) =>
                {
                    return Task.FromResult(new PWSResponseMessage<PWSAppointment>());
                };

                var controller = new AppointmentsController(appSettings, logger, new PWSDataService<PWSAppointment>(appSettings.ValueGet(), new StubILogger()));

                var resp = Task.FromResult(controller.Get(new AppointmentRequest()
                {
                    ClientName = "1",
                    NationalID = "1"
                })).Result;

                Assert.NotNull(resp.Result.Value);
            }
        }

        [Fact]
        public void ConsultsControllerTest()
        {
            var appSettings = new StubIOptions<FPDS_Settings>()
            {
                InstanceBehavior = StubBehaviors.CurrentProxy,
                ValueGet = () =>
                {
                    return new FPDS_Settings()
                    {
                        EndPoint = "http://www.test.com"
                    };
                }
            };

            var logger = new StubILogger<ConsultsController>();

            using (ShimsContext.Create())
            {
                ShimParameterValidator.AllInstances.ValidateClientNameStringStringString = (v, s1, s2, s3) =>
                {
                    return new CustomViewDetail();
                };

                ShimFPDSDataService<ConsultDocument>.AllInstances.GetDataAsyncStringStringStringStringString = (svc, s1, s2, s3, s4, s5) =>
                {
                    return GetFPDSResponse<FPDSResponseMessage<ConsultDocument>>(s1);
                };

                var controller = new ConsultsController(appSettings, logger, new FPDSDataService<ConsultDocument>(appSettings.ValueGet(), new StubILogger()));

                var resp = Task.FromResult(controller.Get("consult", "1")).Result;

                Assert.NotNull(resp.Result.Value);
            }
        }

        [Fact]
        public void ConsultsControllerConsultRequestTest()
        {
            var appSettings = new StubIOptions<FPDS_Settings>()
            {
                InstanceBehavior = StubBehaviors.CurrentProxy,
                ValueGet = () =>
                {
                    return new FPDS_Settings()
                    {
                        EndPoint = "http://www.test.com"
                    };
                }
            };

            var logger = new StubILogger<ConsultsController>();

            using (ShimsContext.Create())
            {
                ShimParameterValidator.AllInstances.ValidateClientNameStringStringString = (v, s1, s2, s3) =>
                {
                    return new CustomViewDetail();
                };

                ShimFPDSDataService<ConsultDocument>.AllInstances.GetDataAsyncStringStringStringStringString = (svc, s1, s2, s3, s4, s5) =>
                {
                    return GetFPDSResponse<FPDSResponseMessage<ConsultDocument>>(s1);
                };

                var controller = new ConsultsController(appSettings, logger, new FPDSDataService<ConsultDocument>(appSettings.ValueGet(), new StubILogger()));

                var resp = Task.FromResult(controller.Get(new ConsultRequest()
                {
                    ClientName = "consult",
                    NationalID = "1"
                })).Result;

                Assert.NotNull(resp.Result.Value);
            }
        }

        [Fact]
        public void FlagsControllerTest()
        {
            var appSettings = new StubIOptions<FPDS_Settings>()
            {
                InstanceBehavior = StubBehaviors.CurrentProxy,
                ValueGet = () =>
                {
                    return new FPDS_Settings()
                    {
                        EndPoint = "http://www.test.com"
                    };
                }
            };

            var logger = new StubILogger<FlagsController>();

            using (ShimsContext.Create())
            {
                ShimParameterValidator.AllInstances.ValidateClientNameStringStringString = (v, s1, s2, s3) =>
                {
                    return new CustomViewDetail();
                };

                ShimFPDSDataService<Flag>.AllInstances.GetFlagDataAsyncStringStringString = (svc, s1, s2, s3) =>
                {
                    return GetFPDSResponse<FPDSResponseMessage<Flag>>(s1);
                };

                var controller = new FlagsController(appSettings, logger, new FPDSDataService<Flag>(appSettings.ValueGet(), new StubILogger()));

                var resp = Task.FromResult(controller.Get("flag", "1")).Result;

                Assert.NotNull(resp.Result.Value);
            }
        }

        [Fact]
        public void FlagsControllerFlagRequestTest()
        {
            var appSettings = new StubIOptions<FPDS_Settings>()
            {
                InstanceBehavior = StubBehaviors.CurrentProxy,
                ValueGet = () =>
                {
                    return new FPDS_Settings()
                    {
                        EndPoint = "http://www.test.com"
                    };
                }
            };

            var logger = new StubILogger<FlagsController>();

            using (ShimsContext.Create())
            {
                ShimParameterValidator.AllInstances.ValidateClientNameStringStringString = (v, s1, s2, s3) =>
                {
                    return new CustomViewDetail();
                };

                ShimFPDSDataService<Flag>.AllInstances.GetFlagDataAsyncStringStringString = (svc, s1, s2, s3) =>
                {
                    return GetFPDSResponse<FPDSResponseMessage<Flag>>(s1);
                };

                var controller = new FlagsController(appSettings, logger, new FPDSDataService<Flag>(appSettings.ValueGet(), new StubILogger()));

                var resp = Task.FromResult(controller.Get(new FlagsRequest()
                {
                    ClientName = "flag",
                    NationalID = "1"
                })).Result;

                Assert.NotNull(resp.Result.Value);
            }
        }

        [Fact]
        public void LabOrdersControllerTest()
        {
            var appSettings = new StubIOptions<FPDS_Settings>()
            {
                InstanceBehavior = StubBehaviors.CurrentProxy,
                ValueGet = () =>
                {
                    return new FPDS_Settings()
                    {
                        EndPoint = "http://www.test.com"
                    };
                }
            };

            var logger = new StubILogger<LabsOrdersController>();

            using (ShimsContext.Create())
            {
                ShimParameterValidator.AllInstances.ValidateClientNameStringStringString = (v, s1, s2, s3) =>
                {
                    return new CustomViewDetail();
                };

                ShimFPDSDataService<LabOrders>.AllInstances.GetLabDataAsyncStringStringStringStringString = (svc, s1, s2, s3, s4, s5) =>
                {
                    return GetFPDSResponse<FPDSResponseMessage<LabOrders>>(s1);
                };

                var controller = new LabsOrdersController(appSettings, logger, new FPDSDataService<LabOrders>(appSettings.ValueGet(), new StubILogger()));

                var resp = Task.FromResult(controller.Get("lab", "1", "1/1/2010", "1/1/2020")).Result;

                Assert.NotNull(resp.Result.Value);
            }
        }

        [Fact]
        public void LabOrdersControllerLabOrdersRequestTest()
        {
            var appSettings = new StubIOptions<FPDS_Settings>()
            {
                InstanceBehavior = StubBehaviors.CurrentProxy,
                ValueGet = () =>
                {
                    return new FPDS_Settings()
                    {
                        EndPoint = "http://www.test.com"
                    };
                }
            };

            var logger = new StubILogger<LabsOrdersController>();

            using (ShimsContext.Create())
            {
                ShimParameterValidator.AllInstances.ValidateClientNameStringStringString = (v, s1, s2, s3) =>
                {
                    return new CustomViewDetail();
                };

                ShimFPDSDataService<LabOrders>.AllInstances.GetLabDataAsyncStringStringStringStringString = (svc, s1, s2, s3, s4, s5) =>
                {
                    return GetFPDSResponse<FPDSResponseMessage<LabOrders>>(s1);
                };

                var controller = new LabsOrdersController(appSettings, logger, new FPDSDataService<LabOrders>(appSettings.ValueGet(), new StubILogger()));

                var resp = Task.FromResult(controller.Get(new LabsOrdersRequest()
                {
                    ClientName = "lab",
                    NationalID = "1"
                })).Result;

                Assert.NotNull(resp.Result.Value);
            }
        }

        [Fact]
        public void MedicationsControllerTest()
        {
            var appSettings = new StubIOptions<FPDS_Settings>()
            {
                InstanceBehavior = StubBehaviors.CurrentProxy,
                ValueGet = () =>
                {
                    return new FPDS_Settings()
                    {
                        EndPoint = "http://www.test.com"
                    };
                }
            };

            var logger = new StubILogger<MedicationsController>();

            using (ShimsContext.Create())
            {
                ShimParameterValidator.AllInstances.ValidateClientNameStringStringString = (v, s1, s2, s3) =>
                {
                    return new CustomViewDetail();
                };

                ShimFPDSDataService<Medication>.AllInstances.GetDataAsyncStringStringStringStringString = (svc, s1, s2, s3, s4, s5) =>
                {
                    return GetFPDSResponse<FPDSResponseMessage<Medication>>(s1);
                };

                var controller = new MedicationsController(appSettings, logger, new FPDSDataService<Medication>(appSettings.ValueGet(), new StubILogger()));

                var resp = Task.FromResult(controller.Get("med", "1")).Result;

                Assert.NotNull(resp.Result.Value);
            }
        }

        [Fact]
        public void MedicationsControllerMedicationRequestTest()
        {
            var appSettings = new StubIOptions<FPDS_Settings>()
            {
                InstanceBehavior = StubBehaviors.CurrentProxy,
                ValueGet = () =>
                {
                    return new FPDS_Settings()
                    {
                        EndPoint = "http://www.test.com"
                    };
                }
            };

            var logger = new StubILogger<MedicationsController>();

            using (ShimsContext.Create())
            {
                ShimParameterValidator.AllInstances.ValidateClientNameStringStringString = (v, s1, s2, s3) =>
                {
                    return new CustomViewDetail();
                };

                ShimFPDSDataService<Medication>.AllInstances.GetDataAsyncStringStringStringStringString = (svc, s1, s2, s3, s4, s5) =>
                {
                    return GetFPDSResponse<FPDSResponseMessage<Medication>>(s1);
                };

                var controller = new MedicationsController(appSettings, logger, new FPDSDataService<Medication>(appSettings.ValueGet(), new StubILogger()));

                var resp = Task.FromResult(controller.Get(new MedicationsRequest()
                {
                    ClientName = "med",
                    NationalID = "1"
                })).Result;

                Assert.NotNull(resp.Result.Value);
            }
        }

        [Fact]
        public void NonVetControllerTest()
        {
            var appSettings = new StubIOptions<PathwaySettings>()
            {
                InstanceBehavior = StubBehaviors.CurrentProxy,
                ValueGet = () =>
                {
                    return new PathwaySettings()
                    {
                        EndPoint = "http://www.test.com"
                    };
                }
            };

            var logger = new StubILogger<NonVetController>();

            using (ShimsContext.Create())
            {
                ShimParameterValidator.AllInstances.ValidateClientNameStringStringString = (v, s1, s2, s3) =>
                {
                    return new CustomViewDetail();
                };

                ShimPWSDataService<NonVeteranEmployeeData>.AllInstances.GetNonVetRecordAsyncStringListOfString = (svc, s1, sl) =>
                {
                    return Task.FromResult(new PWSResponseMessage<NonVeteranEmployeeData>());
                };

                var controller = new NonVetController(appSettings, logger, new PWSDataService<NonVeteranEmployeeData>(appSettings.ValueGet(), new StubILogger()));

                var resp = Task.FromResult(controller.Get(new NonVetEmployeeRequest()
                {
                    ClientName = "1",
                    NationalIds = new List<string> { "1" }
                })).Result;

                Assert.NotNull(resp.Result.Value);
            }
        }

        [Fact]
        public void NotesControllerTest()
        {
            var appSettings = new StubIOptions<FPDS_Settings>()
            {
                InstanceBehavior = StubBehaviors.CurrentProxy,
                ValueGet = () =>
                {
                    return new FPDS_Settings()
                    {
                        EndPoint = "http://www.test.com"
                    };
                }
            };

            var logger = new StubILogger<NotesController>();

            using (ShimsContext.Create())
            {
                ShimParameterValidator.AllInstances.ValidateClientNameStringStringString = (v, s1, s2, s3) =>
                {
                    return new CustomViewDetail();
                };

                ShimFPDSDataService<Note>.AllInstances.GetDataAsyncStringStringStringStringString = (svc, s1, s2, s3, s4, s5) =>
                {
                    return GetFPDSResponse<FPDSResponseMessage<Note>>(s1);
                };

                var controller = new NotesController(appSettings, logger, new FPDSDataService<Note>(appSettings.ValueGet(), new StubILogger()));

                var resp = Task.FromResult(controller.Get("note", "1")).Result;

                Assert.NotNull(resp.Result.Value);
            }
        }

        [Fact]
        public void NotesControllerNotesRequestTest()
        {
            var appSettings = new StubIOptions<FPDS_Settings>()
            {
                InstanceBehavior = StubBehaviors.CurrentProxy,
                ValueGet = () =>
                {
                    return new FPDS_Settings()
                    {
                        EndPoint = "http://www.test.com"
                    };
                }
            };

            var logger = new StubILogger<NotesController>();

            using (ShimsContext.Create())
            {
                ShimParameterValidator.AllInstances.ValidateClientNameStringStringString = (v, s1, s2, s3) =>
                {
                    return new CustomViewDetail();
                };

                ShimFPDSDataService<Note>.AllInstances.GetDataAsyncStringStringStringStringString = (svc, s1, s2, s3, s4, s5) =>
                {
                    return GetFPDSResponse<FPDSResponseMessage<Note>>(s1);
                };

                var controller = new NotesController(appSettings, logger, new FPDSDataService<Note>(appSettings.ValueGet(), new StubILogger()));

                var resp = Task.FromResult(controller.Get(new NotesRequest()
                {
                    ClientName = "note",
                    NationalID = "1"
                })).Result;

                Assert.NotNull(resp.Result.Value);
            }
        }

        [Fact]
        public void OrdersControllerTest()
        {
            var appSettings = new StubIOptions<FPDS_Settings>()
            {
                InstanceBehavior = StubBehaviors.CurrentProxy,
                ValueGet = () =>
                {
                    return new FPDS_Settings()
                    {
                        EndPoint = "http://www.test.com"
                    };
                }
            };

            var logger = new StubILogger<OrdersController>();

            using (ShimsContext.Create())
            {
                ShimParameterValidator.AllInstances.ValidateClientNameStringStringString = (v, s1, s2, s3) =>
                {
                    return new CustomViewDetail();
                };

                ShimFPDSDataService<Order>.AllInstances.GetDataAsyncStringStringStringStringString = (svc, s1, s2, s3, s4, s5) =>
                {
                    return GetFPDSResponse<FPDSResponseMessage<Order>>(s1);
                };

                var controller = new OrdersController(appSettings, logger, new FPDSDataService<Order>(appSettings.ValueGet(), new StubILogger()));

                var resp = Task.FromResult(controller.Get("order", "1")).Result;

                Assert.NotNull(resp.Result.Value);
            }
        }

        [Fact]
        public void OrdersControllerOrdersRequestTest()
        {
            var appSettings = new StubIOptions<FPDS_Settings>()
            {
                InstanceBehavior = StubBehaviors.CurrentProxy,
                ValueGet = () =>
                {
                    return new FPDS_Settings()
                    {
                        EndPoint = "http://www.test.com"
                    };
                }
            };

            var logger = new StubILogger<OrdersController>();

            using (ShimsContext.Create())
            {
                ShimParameterValidator.AllInstances.ValidateClientNameStringStringString = (v, s1, s2, s3) =>
                {
                    return new CustomViewDetail();
                };

                ShimFPDSDataService<Order>.AllInstances.GetDataAsyncStringStringStringStringString = (svc, s1, s2, s3, s4, s5) =>
                {
                    return GetFPDSResponse<FPDSResponseMessage<Order>>(s1);
                };

                var controller = new OrdersController(appSettings, logger, new FPDSDataService<Order>(appSettings.ValueGet(), new StubILogger()));

                var resp = Task.FromResult(controller.Get(new OrderRequest()
                {
                    ClientName = "order",
                    NationalID = "1"
                })).Result;

                Assert.NotNull(resp.Result.Value);
            }
        }

        [Fact]
        public void PostingsControllerTest()
        {
            var appSettings = new StubIOptions<FPDS_Settings>()
            {
                InstanceBehavior = StubBehaviors.CurrentProxy,
                ValueGet = () =>
                {
                    return new FPDS_Settings()
                    {
                        EndPoint = "http://www.test.com"
                    };
                }
            };

            var logger = new StubILogger<PostingsController>();

            using (ShimsContext.Create())
            {
                ShimParameterValidator.AllInstances.ValidateClientNameStringStringString = (v, s1, s2, s3) =>
                {
                    return new CustomViewDetail();
                };

                ShimFPDSDataService<Posting>.AllInstances.GetDataAsyncStringStringStringStringString = (svc, s1, s2, s3, s4, s5) =>
                {
                    return GetFPDSResponse<FPDSResponseMessage<Posting>>(s1);
                };

                var controller = new PostingsController(appSettings, logger, new FPDSDataService<Posting>(appSettings.ValueGet(), new StubILogger()));

                var resp = Task.FromResult(controller.Get("posting", "1")).Result;

                Assert.NotNull(resp.Result.Value);
            }
        }

        [Fact]
        public void PostingsControllerPostingsRequestTest()
        {
            var appSettings = new StubIOptions<FPDS_Settings>()
            {
                InstanceBehavior = StubBehaviors.CurrentProxy,
                ValueGet = () =>
                {
                    return new FPDS_Settings()
                    {
                        EndPoint = "http://www.test.com"
                    };
                }
            };

            var logger = new StubILogger<PostingsController>();

            using (ShimsContext.Create())
            {
                ShimParameterValidator.AllInstances.ValidateClientNameStringStringString = (v, s1, s2, s3) =>
                {
                    return new CustomViewDetail();
                };

                ShimFPDSDataService<Posting>.AllInstances.GetDataAsyncStringStringStringStringString = (svc, s1, s2, s3, s4, s5) =>
                {
                    return GetFPDSResponse<FPDSResponseMessage<Posting>>(s1);
                };

                var controller = new PostingsController(appSettings, logger, new FPDSDataService<Posting>(appSettings.ValueGet(), new StubILogger()));

                var resp = Task.FromResult(controller.Get(new PostingRequest()
                {
                    ClientName = "postings",
                    NationalID = "1"
                })).Result;

                Assert.NotNull(resp.Result.Value);
            }
        }

        [Fact]
        public void RadiologyReportsControllerTest()
        {
            var appSettings = new StubIOptions<FPDS_Settings>()
            {
                InstanceBehavior = StubBehaviors.CurrentProxy,
                ValueGet = () =>
                {
                    return new FPDS_Settings()
                    {
                        EndPoint = "http://www.test.com"
                    };
                }
            };

            var logger = new StubILogger<RadiologyReportsController>();

            using (ShimsContext.Create())
            {
                ShimParameterValidator.AllInstances.ValidateClientNameStringStringString = (v, s1, s2, s3) =>
                {
                    return new CustomViewDetail();
                };

                ShimFPDSDataService<RadiologyDocument>.AllInstances.GetDataAsyncStringStringStringStringString = (svc, s1, s2, s3, s4, s5) =>
                {
                    return GetFPDSResponse<FPDSResponseMessage<RadiologyDocument>>(s1);
                };

                var controller = new RadiologyReportsController(appSettings, logger, new FPDSDataService<RadiologyDocument>(appSettings.ValueGet(), new StubILogger()));

                var resp = Task.FromResult(controller.Get("radiologyreports", "1", "01/01/1950", "01/01/2050")).Result;

                Assert.NotNull(resp.Result.Value);
            }
        }

        [Fact]
        public void RadiologyReportsControllerRadiologyReportsRequestTest()
        {
            var appSettings = new StubIOptions<FPDS_Settings>()
            {
                InstanceBehavior = StubBehaviors.CurrentProxy,
                ValueGet = () =>
                {
                    return new FPDS_Settings()
                    {
                        EndPoint = "http://www.test.com"
                    };
                }
            };

            var logger = new StubILogger<RadiologyReportsController>();

            using (ShimsContext.Create())
            {
                ShimParameterValidator.AllInstances.ValidateClientNameStringStringString = (v, s1, s2, s3) =>
                {
                    return new CustomViewDetail();
                };

                ShimFPDSDataService<RadiologyDocument>.AllInstances.GetDataAsyncStringStringStringStringString = (svc, s1, s2, s3, s4, s5) =>
                {
                    return GetFPDSResponse<FPDSResponseMessage<RadiologyDocument>>(s1);
                };

                var controller = new RadiologyReportsController(appSettings, logger, new FPDSDataService<RadiologyDocument>(appSettings.ValueGet(), new StubILogger()));

                var resp = Task.FromResult(controller.Get(new RadiologyReportsRequest()
                {
                    ClientName = "radiologyreports",
                    NationalID = "1",
                    StartDate = "01/01/1950",
                    EndDate = "01/01/2050"
                })).Result;

                Assert.NotNull(resp.Result.Value);
            }
        }
    }
}