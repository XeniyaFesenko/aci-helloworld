namespace va_veis_healthdatarepo.Models.Requests
{
    public class NonVetRequestMessage : PathwaysRequestMessage
    {
        public NonVetRequestMessage(string clientName, List<string> nationalIds, PathwaySettings settings) : base(clientName, settings)
        {
            if (nationalIds == null)
            {
                throw new ArgumentNullException("nationalIds");
            }

            ContentType = "application/xml";

            NationalIdList = nationalIds;
            ServiceCallParameters = new Dictionary<string, string>
            {
                {"clientRequestInitiationTime", DateTime.Now.ToString("yyyy-MM-dd'T'hh:mm:ss")},
                {"requestId", Guid.NewGuid().ToString()},
                {"clientApplicationID", clientName}
            };
        }

        public NonVetRequestMessage(string clientName, List<string> nationalIds) : base(clientName)
        {
            if (nationalIds == null)
            {
                throw new ArgumentNullException("nationalIds");
            }

            ContentType = "application/xml";

            NationalIdList = nationalIds;
            ServiceCallParameters = new Dictionary<string, string>
            {
                {"clientRequestInitiationTime", DateTime.Now.ToString("yyyy-MM-dd'T'hh:mm:ss")},
                {"requestId", Guid.NewGuid().ToString()},
                {"clientApplicationID", clientName}
            };
        }

        public Dictionary<string, string> ServiceCallParameters { get; set; }
        public List<string> NationalIdList { get; set; }

        public override string GetPathwaysSoapBody()
        {
            if (NationalIdList.Count == 0)
            {
                throw new ApplicationException("Patient National ID not set!");
            }
            var serviceCallDate = ServiceCallParameters["clientRequestInitiationTime"];
            var clientApplicationId = ServiceCallParameters["clientApplicationID"];
            var requestId = ServiceCallParameters["requestId"];
            var filterID = "NonVeteranEmployee";
            var patientXML = "";
            foreach (var i in NationalIdList)
            {
                patientXML = patientXML + "<NationalId>" + i + "</NationalId>";
            }
            var soapRequest = string.Format(
                @"<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:pat=""http://repositories.med.va.gov/pathways"">
                   <soapenv:Header/>
                   <soapenv:Body>
                      <pat:readData>
                         <in0>NonVeteranEmployeeRead1</in0>
                         <in1>        
                           <![CDATA[<?xml version=""1.0"" encoding=""UTF-8""?>
                                 <filter:filter xmlns:filter=""Filter"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" vhimVersion=""Vhim_4_00"">
                                    <filterId>{0}</filterId>
                                    <clientName>{1}</clientName>
                                    <clientRequestInitiationTime>{3}</clientRequestInitiationTime>
                                    <patients>
                                        {2}
                                    </patients>
                                    <entryPointFilter queryName=""nonVeteranEmployee"">
                                        <domainEntryPoint>{0}</domainEntryPoint>
                                        <queryTimeoutSeconds>600</queryTimeoutSeconds>
                                    </entryPointFilter>
                                </filter:filter>
                            ]]>
                         </in1>
                         <in2>NonVeteranEmployee_Filter</in2>
                         <in3>{4}</in3>
                      </pat:readData>
                   </soapenv:Body>
                 </soapenv:Envelope>", filterID, clientApplicationId, patientXML, serviceCallDate, requestId);
            return soapRequest;
        }
    }
}
