using Microsoft.Extensions.Logging.Fakes;
using Microsoft.Extensions.Options.Fakes;
using Microsoft.QualityTools.Testing.Fakes;
using va_veis_healthdatarepo.Models;
using va_veis_healthdatarepo.Models.Entities;
using va_veis_healthdatarepo.Models.Responses;

namespace va_veis_healthdatarepo_tests
{
    public class HealthDataRepoTestsBase
    {
        IDisposable _context;
        StubILogger _logger;
        StubIOptions<Settings> _settings;
        StubIOptions<FPDS_Settings> _fpdsSettings;
        StubIOptions<PathwaySettings> _pathwaySettings;

        public IDisposable Context
        {
            get { if (_context != null) ShimsContext.Reset(); else { _context = ShimsContext.Create(); } return _context; }
        }

        public StubILogger Logger { get { return _logger; } }

        public StubIOptions<Settings> Settings { get { return _settings; } }
        public StubIOptions<FPDS_Settings> FPDSSettings { get { return _fpdsSettings; } }
        public StubIOptions<PathwaySettings> PathwaySettings { get { return _pathwaySettings; } }

        public HealthDataRepoTestsBase()
        {
            _context ??= ShimsContext.Create();
            _logger = new StubILogger();
            _settings = new StubIOptions<Settings>()
            {
                ValueGet = () =>
                {
                    return new Settings
                    {
                        EndPoint = "http://www.test.com"
                    };
                }
            };
            _fpdsSettings = new StubIOptions<FPDS_Settings>()
            {
                ValueGet = () =>
                {
                    return new FPDS_Settings
                    {
                        EndPoint = "http://www.test.com"
                    };
                }
            };
            _pathwaySettings = new StubIOptions<PathwaySettings>()
            {
                ValueGet = () =>
                {
                    return new PathwaySettings
                    {
                        EndPoint = "http://www.test.com"
                    };
                }
            };
        }

        internal Task<T> GetFPDSResponse<T>(string responseType) where T : class, new()
        {
            T response;
            switch (responseType)
            {
                case "consult":
                    response = new FPDSResponseMessage<ConsultDocument>() as T;
                    break;
                case "flag":
                    response = new FPDSResponseMessage<Flag>() as T;
                    break;
                case "lab":
                    response = new FPDSResponseMessage<LabOrders>() as T;
                    break;
                case "med":
                    response = new FPDSResponseMessage<Medication>() as T;
                    break;
                case "note":
                    response = new FPDSResponseMessage<Note>() as T;
                    break;
                case "order":
                    response = new FPDSResponseMessage<Order>() as T;
                    break;
                case "radiologyreports":
                    response = new FPDSResponseMessage<RadiologyDocument>() as T;
                    break;
                default:
                    response = new T();
                    break;
            }

            return Task.FromResult(response);
        }

        internal Task<HttpResponseMessage> GetResponseMessage(string uriPath)
        {
            if (uriPath.Contains("alert"))
            {
                return Task.FromResult(new HttpResponseMessage()
                {
                    Content = new StringContent("<soap:Envelope xmlns:soap=\"http://schemas.xmlsoap.org/soap/envelope/\"><soap:Body><ns1:readClinicalData1Response xmlns:ns1=\"http://client.cds.med.va.gov\"><out>&lt;?xml version=\"1.0\" encoding=\"UTF-8\"?&gt;&lt;clinicaldata:ClinicalData xmlns:clinicaldata=\"Clinicaldata\"&gt;&lt;templateId&gt;UserAlertRead&lt;/templateId&gt;&lt;requestId&gt;70149e4e-db23-4c18-8914-f1e7186e82a8&lt;/requestId&gt;&lt;UserAlertRead&gt;&lt;users&gt;&lt;user&gt;&lt;duz&gt;2&lt;/duz&gt;&lt;name&gt;TYSON,CASSANDRA R&lt;/name&gt;&lt;lastSignOnDateTime&gt;20050418153112&lt;/lastSignOnDateTime&gt;&lt;facility&gt;984&lt;/facility&gt;&lt;alerts&gt;&lt;alert&gt;&lt;alertId&gt;23996081&lt;/alertId&gt;&lt;patient&gt;LIM&lt;/patient&gt;&lt;dateTime&gt;20050527151851&lt;/dateTime&gt;&lt;message&gt;Review description for LR*5.2*302 use KIDS:Utilities:Build File Print&lt;/message&gt;&lt;urgency&gt;n/a&lt;/urgency&gt;&lt;/alert&gt;&lt;/alerts&gt;&lt;/user&gt;&lt;/users&gt;&lt;/UserAlertRead&gt;&lt;/clinicaldata:ClinicalData&gt;</out></ns1:readClinicalData1Response></soap:Body></soap:Envelope>")
                });
            }
            else if (uriPath.Contains("appointment"))
            {
                return Task.FromResult(new HttpResponseMessage()
                {
                    Content = new StringContent("<soap:Envelope xmlns:soap=\"http://schemas.xmlsoap.org/soap/envelope/\"><soap:Body><ns1:readDataResponse xmlns:ns1=\"http://repositories.med.va.gov/pathways\"><out>&lt;?xml version=\"1.0\" encoding=\"UTF-8\"?&gt;&lt;appointmentsdata:AppointmentsData xmlns:appointmentsdata=\"Appointmentsdata\"&gt;&lt;templateId&gt;AppointmentsRead1&lt;/templateId&gt;&lt;requestId&gt;HDR-24026&lt;/requestId&gt;&lt;patients&gt;&lt;patient&gt;&lt;requestedNationalId&gt;1012881470V086192&lt;/requestedNationalId&gt;&lt;resultantIdentifiers&gt;&lt;resultantIdentifier&gt;&lt;identity&gt;3943&lt;/identity&gt;&lt;assigningFacility&gt;451&lt;/assigningFacility&gt;&lt;assigningAuthority&gt;USVHA&lt;/assigningAuthority&gt;&lt;/resultantIdentifier&gt;&lt;/resultantIdentifiers&gt;&lt;appointments&gt;&lt;appointment&gt;&lt;recordIdentifier&gt;&lt;identity&gt;3220507.1&lt;/identity&gt;&lt;namespaceId&gt;613_2.98&lt;/namespaceId&gt;&lt;/recordIdentifier&gt;&lt;patient&gt;&lt;identifier&gt;&lt;identity&gt;202243&lt;/identity&gt;&lt;assigningFacility&gt;613&lt;/assigningFacility&gt;&lt;/identifier&gt;&lt;/patient&gt;&lt;appointmentDateTime&gt;&lt;literal&gt;202205071000&lt;/literal&gt;&lt;/appointmentDateTime&gt;&lt;location&gt;&lt;identifier&gt;&lt;identity&gt;777&lt;/identity&gt;&lt;name&gt;AUDIO/HEARING/1ST FLOOR/1D160&lt;/name&gt;&lt;/identifier&gt;&lt;telephone&gt;3288&lt;/telephone&gt;&lt;institution&gt;&lt;identifier&gt;&lt;identity&gt;613&lt;/identity&gt;&lt;name&gt;MARTINSBURG VAMC&lt;/name&gt;&lt;/identifier&gt;&lt;officialVAName&gt;MARTINSBURG VAMC&lt;/officialVAName&gt;&lt;/institution&gt;&lt;/location&gt;&lt;clinicMeetsAtThisFacility&gt;YES&lt;/clinicMeetsAtThisFacility&gt;&lt;service&gt;NONE&lt;/service&gt;&lt;division&gt;MARTINSBURG&lt;/division&gt;&lt;telephoneExtension&gt;13579&lt;/telephoneExtension&gt;&lt;providers&gt;&lt;provider&gt;&lt;name_given&gt;JOHN&lt;/name_given&gt;&lt;name_middle&gt;G&lt;/name_middle&gt;&lt;name_family&gt;TESTER&lt;/name_family&gt;&lt;name_title&gt;PHYSICIAN&lt;/name_title&gt;&lt;display_name&gt;TESTER,JOHN G&lt;/display_name&gt;&lt;/provider&gt;&lt;/providers&gt;&lt;appointmentStatus&gt;&lt;code&gt;I&lt;/code&gt;&lt;displayText&gt;INPATIENT&lt;/displayText&gt;&lt;/appointmentStatus&gt;&lt;appointmentType&gt;&lt;code&gt;9&lt;/code&gt;&lt;displayText&gt;REGULAR&lt;/displayText&gt;&lt;/appointmentType&gt;&lt;appointmentRequestComment&gt;AutoComm:null|FILLED by Scheduling&lt;/appointmentRequestComment&gt;&lt;purposeOfVisitId&gt;3&lt;/purposeOfVisitId&gt;&lt;purposeOfVisitName&gt;SCHEDULED VISIT&lt;/purposeOfVisitName&gt;&lt;status&gt;&lt;code&gt;8&lt;/code&gt;&lt;displayText&gt;INPATIENT/NO ACT TAKN&lt;/displayText&gt;&lt;/status&gt;&lt;primaryStopCode&gt;203&lt;/primaryStopCode&gt;&lt;/appointment&gt;&lt;/appointments&gt;&lt;/patient&gt;&lt;/patients&gt;&lt;/appointmentsdata:AppointmentsData&gt;</out></ns1:readDataResponse></soap:Body></soap:Envelope>")
                });
            }
            else if (uriPath.Contains("consult"))
            {
                return Task.FromResult(new HttpResponseMessage()
                {
                    Content = new StringContent("{\"sites\":[{\"apiVersion\":\"1.01\",\"params\":{\"domain\":\"TEST.CHEYENNE.MED.VA.GOV\",\"systemId\":\"F253\"},\"data\":{\"totalItems\":0,\"items\":[]}},{\"apiVersion\":\"1.01\",\"params\":{\"domain\":\"TEST.DAYTON.MED.VA.GOV\",\"systemId\":\"8B73\"},\"data\":{\"totalItems\":0,\"items\":[]}}]}")
                });
            }
            else if (uriPath.Contains("document"))//Note
            {
                return Task.FromResult(new HttpResponseMessage()
                {
                    Content = new StringContent("{\"sites\":[{\"apiVersion\":\"1.01\",\"params\":{\"domain\":\"TEST\",\"systemId\":\"12345\"},\"data\":{\"totalItems\":0,\"items\":[]}},{\"apiVersion\":\"1.01\",\"params\":{\"domain\":\"SLC4.FO-BAYPINES.MED.VA.GOV\",\"systemId\":\"F0E0\"},\"data\":{\"updated\":\"20231213151031\",\"totalItems\":458,\"items\":[{\"documentClass\":\"PROGRESS NOTES\",\"documentTypeCode\":\"PN\",\"documentTypeName\":\"Progress Note\",\"encounterName\":\"TEST\",\"encounterUid\":\"TEST\",\"entered\":20200228094236,\"facilityCode\":123,\"facilityName\":\"TEST\",\"localId\":1235,\"localTitle\":\"TEST\",\"referenceDateTime\":20200228094235,\"statusName\":\"completed\",\"text\":[{\"clinicians\":[{\"name\":\"TEST\",\"role\":\"A\",\"uid\":\"TEST\"},{\"name\":\"TEST\",\"role\":\"S\",\"signature\":\"TEST \",\"signedDateTime\":20200228094237,\"uid\":\"TEST\"}],\"content\":\"TEST\",\"dateTime\":20200228094235,\"status\":\"completed\",\"uid\":\"TEST\"},{\"clinicians\":[{\"name\":\"TEST\",\"role\":\"A\",\"uid\":\"TEST\"},{\"name\":\"TEST\",\"role\":\"S\",\"signature\":\"TEST \",\"signedDateTime\":20200228094739,\"uid\":\"TEST\"}],\"content\":\"TEST TEST TEST\",\"dateTime\":20200228094738,\"status\":\"COMPLETED\",\"uid\":\"TEST\"}],\"uid\":\"TEST\"}]}}]}")
                });
            }
            else if (uriPath.Contains("flag"))
            {
                return Task.FromResult(new HttpResponseMessage()
                {
                    Content = new StringContent("{\"sites\":[{\"results\":{\"flags\":{\"total\":1,\"flag\":{\"origSite\":{\"code\":578,\"name\":\"TEST\"},\"ownSite\":{\"code\":578,\"name\":\"TEST\"},\"approvedBy\":{\"code\":\".5\",\"name\":\"CHIEF OF STAFF\"},\"name\":{\"value\":\"TEST\"},\"assigned\":{\"value\":123456789.98765433},\"id\":{\"value\":\"50684~1;DGPF(26.15,\"},\"category\":{\"value\":\"I (NATIONAL)\"},\"type\":{\"value\":\"TEST\"},\"content\":{\"xml:space\":\"preserve\",\"content\":\"test test test test\"}}},\"timeZone\":\"-0600\",\"version\":1.13}},{\"results\":{\"flags\":{\"total\":1,\"flag\":{\"origSite\":{\"code\":578,\"name\":\"TEST)\"},\"ownSite\":{\"code\":578,\"name\":\"TEST\"},\"approvedBy\":{\"code\":\".5\",\"name\":\"CHIEF OF STAFF\"},\"name\":{\"value\":\"TEST\"},\"assigned\":{\"value\":123456789.98765433},\"id\":{\"value\":\"304932~1;DGPF(26.15,\"},\"category\":{\"value\":\"I (NATIONAL)\"},\"type\":{\"value\":\"TEST\"},\"content\":{\"xml:space\":\"preserve\",\"content\":\"test test test test test test\"}}},\"timeZone\":\"-0500\",\"version\":1.13}}]}")
                });
            }
            else if (uriPath.Contains("lab"))
            {
                return Task.FromResult(new HttpResponseMessage()
                {
                    Content = new StringContent("{\"sites\":[{\"apiVersion\":\"1.01\",\"params\":{\"domain\":\"TEST.MARTINSBURG.MED.VA.GOV\",\"systemId\":\"9ABC\"},\"data\":{\"updated\":\"20231208120107\",\"totalItems\":1,\"items\":[{\"categoryCode\":\"urn:va:lab-category:CH\",\"categoryName\":\"Laboratory\",\"comment\":\"~For Test: HEMOGLOBIN A1C ~for testing only Ordering Provider: Dinh Ngo Report Released Date/Time: Aug 11, 2016@10:35\\r\\n Performing Lab: MARTINSBURG VAMC\\r\\n                510 BUTLER AVENUE NO SECOND STREET DESIGNATION MARTINSBURG, WV 25401\\r\\n \",\"displayName\":\"HGB-A1C\",\"displayOrder\":998.0613185,\"facilityCode\":613,\"facilityName\":\"MARTINSBURG VAMC\",\"groupName\":\"A1c 0811 1\",\"groupUid\":\"urn:va:accession:9ABC:202243:CH;6839187.896569\",\"high\":6.1,\"interpretationCode\":\"urn:hl7:observation-interpretation:H\",\"interpretationName\":\"High\",\"labOrderId\":1519723,\"localId\":\"CH;6839187.896569;613185\",\"low\":4.3,\"observed\":201608111034,\"orderUid\":\"urn:va:order:9ABC:202243:15247382\",\"result\":100,\"resulted\":201608111035,\"sample\":\"BLOOD\",\"specimen\":\"BLOOD\",\"statusCode\":\"urn:va:lab-status:completed\",\"statusName\":\"completed\",\"typeCode\":\"urn:lnc:17856-6\",\"typeId\":5064,\"typeName\":\"HEMOGLOBIN A1C \",\"uid\":\"urn:va:lab:9ABC:202243:CH;6839187.896569;613185\",\"units\":\"%\",\"vuid\":\"urn:va:vuid:4659591\"}]}}]}")
                });
            }
            else if (uriPath.Contains("med"))
            {
                return Task.FromResult(new HttpResponseMessage()
                {
                    Content = new StringContent("{\"sites\":[{\"apiVersion\":\"1.01\",\"params\":{\"domain\":\"TEST.MARTINSBURG.MED.VA.GOV\",\"systemId\":\"9ABC\"},\"data\":{\"updated\":\"20231206090039\",\"totalItems\":4,\"items\":[{\"dosages\":[{\"dose\":\"400 MG\",\"relativeStart\":0,\"relativeStop\":717120,\"routeName\":\"PO\",\"scheduleFreq\":720,\"scheduleName\":\"BID(09-17)\",\"scheduleType\":\"CONTINUOUS\",\"start\":20150401,\"stop\":20160811,\"units\":\"MG\"}],\"facilityCode\":613,\"facilityName\":\"MARTINSBURG VAMC\",\"fills\":[{\"daysSupplyDispensed\":90,\"dispenseDate\":20150401,\"quantityDispensed\":180,\"releaseDate\":\"\",\"routing\":\"M\"}],\"lastFilled\":20150401,\"localId\":\"6001944;O\",\"medStatus\":\"urn:sct:73425007\",\"medStatusName\":\"not active\",\"medType\":\"urn:sct:73639000\",\"name\":\"CIMETIDINE TAB\",\"orders\":[{\"daysSupply\":90,\"fillCost\":8.64,\"fillsAllowed\":0,\"fillsRemaining\":0,\"locationName\":\"CCS/HOME VISIT\",\"locationUid\":\"urn:va:location:9ABC:2559\",\"orderUid\":\"urn:va:order:9ABC:202243:15247378\",\"ordered\":201608110916,\"pharmacistName\":\"NGO,DINH\",\"pharmacistUid\":\"urn:va:user:9ABC:65722\",\"prescriptionId\":7134319,\"providerName\":\"NGO,DINH\",\"providerUid\":\"urn:va:user:9ABC:65722\",\"quantityOrdered\":180,\"successor\":\"urn:va:med:9ABC:202243:15247381\",\"vaRouting\":\"M\"}],\"overallStart\":20150401,\"overallStop\":20150630,\"patientInstruction\":\"\",\"productFormName\":\"TAB\",\"products\":[{\"drugClassCode\":\"urn:vadc:GA301\",\"drugClassName\":\"HISTAMINE ANTAGONISTS\",\"ingredientCode\":\"urn:va:vuid:4018845\",\"ingredientCodeName\":\"CIMETIDINE\",\"ingredientName\":\"CIMETIDINE TAB\",\"ingredientRole\":\"urn:sct:410942007\",\"strength\":\"400 MG\",\"suppliedCode\":\"urn:va:vuid:4006821\",\"suppliedName\":\"CIMETIDINE 400MG TAB\"}],\"qualifiedName\":\"CIMETIDINE TAB\",\"sig\":\"TAKE ONE TABLET BY MOUTH TWICE A DAY\",\"stopped\":20160811,\"type\":\"Prescription\",\"uid\":\"urn:va:med:9ABC:202243:15247378\",\"vaStatus\":\"DISCONTINUED/EDIT\",\"vaType\":\"O\"}]}}]}")
                });
            }
            else if (uriPath.Contains("nonvet"))
            {
                return Task.FromResult(new HttpResponseMessage()
                {
                    Content = new StringContent("<soap:Envelope xmlns:soap=\"http://schemas.xmlsoap.org/soap/envelope/\"><soap:Body><ns1:readDataResponse xmlns:ns1=\"http://repositories.med.va.gov/pathways\"><out>&lt;?xml version=\"1.0\" encoding=\"UTF-8\"?&gt;&lt;nonVeteranEmployeedata:NonVeteranEmployeeData xmlns:nonVeteranEmployeedata=\"NonVeteranEmployeedata\"&gt;&lt;templateId&gt;NonVeteranEmployeeRead1&lt;/templateId&gt;&lt;requestId&gt;e5859e0f-c2a8-43f4-81a0-2e5ae6922367&lt;/requestId&gt;&lt;patients&gt;&lt;patient&gt;&lt;requestedNationalId&gt;1008692994V108316&lt;/requestedNationalId&gt;&lt;resultantIdentifiers&gt;&lt;resultantIdentifier&gt;&lt;identity&gt;485464&lt;/identity&gt;&lt;assigningFacility&gt;528&lt;/assigningFacility&gt;&lt;assigningAuthority&gt;USVHA&lt;/assigningAuthority&gt;&lt;/resultantIdentifier&gt;&lt;/resultantIdentifiers&gt;&lt;nonVeteranEmployees&gt;&lt;nonVeteranEmployee&gt;&lt;identifier&gt;&lt;dfn&gt;485464&lt;/dfn&gt;&lt;assigningFacility&gt;528&lt;/assigningFacility&gt;&lt;/identifier&gt;&lt;veteranYN&gt;YES&lt;/veteranYN&gt;&lt;primaryEligibility&gt;SC LESS THAN 50%&lt;/primaryEligibility&gt;&lt;/nonVeteranEmployee&gt;&lt;/nonVeteranEmployees&gt;&lt;/patient&gt;&lt;/patients&gt;&lt;/nonVeteranEmployeedata:NonVeteranEmployeeData&gt;</out></ns1:readDataResponse></soap:Body></soap:Envelope>")
                });
            }
            else if (uriPath.Contains("order"))
            {
                return Task.FromResult(new HttpResponseMessage()
                {
                    Content = new StringContent("{\"sites\":[{\"apiVersion\":\"1.01\",\"params\":{\"domain\":\"TEST.MARTINSBURG.MED.VA.GOV\",\"systemId\":\"9ABC\"},\"data\":{\"updated\":\"20231208120015\",\"totalItems\":12,\"items\":[{\"clinicians\":[{\"name\":\"DAVIS,NANCY\",\"role\":\"S\",\"signedDateTime\":201609071553,\"uid\":\"urn:va:user:9ABC:66054\"}],\"content\":\"ARTHROGRAM OF WRIST 2ND FINGER\\r\\n\",\"displayGroup\":\"RAD\",\"entered\":201609071553,\"facilityCode\":613,\"facilityName\":\"MARTINSBURG VAMC\",\"localId\":15247426,\"locationName\":\"4M_OBSERVATION_MEDICAL\",\"locationUid\":\"urn:va:location:9ABC:1619\",\"name\":\"ARTHROGRAM OF WRIST\",\"oiCode\":\"urn:va:oi:4179\",\"oiName\":\"ARTHROGRAM OF WRIST\",\"oiPackageRef\":\"646;99RAP\",\"providerName\":\"DAVIS,NANCY\",\"providerUid\":\"urn:va:user:9ABC:66054\",\"service\":\"RA\",\"start\":20160907,\"statusCode\":\"urn:va:order-status:pend\",\"statusName\":\"PENDING\",\"statusVuid\":\"urn:va:vuid:4501114\",\"stop\":\"\",\"uid\":\"urn:va:order:9ABC:202243:15247426\"}]}}]}")
                });
            }
            else
            {
                return Task.FromResult(new HttpResponseMessage()
                {
                    Content = new StringContent("Invalid URL")
                });
            }
        }
    }
}
