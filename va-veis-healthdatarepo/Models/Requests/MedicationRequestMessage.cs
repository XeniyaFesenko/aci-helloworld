namespace va_veis_healthdatarepo.Models.Requests
{
    public class MedicationRequestMessage : CDSRequestMessage
    {
        public override string BuildSoapRequest()
        {
            // params
            var nationalID = RequestParams.nationalId;
            var clientRequestInitTime = RequestParams.clientRequestInitiationTime;
            var startDate = RequestParams.startDate;
            var endDate = RequestParams.endDate;
            var timeout = RequestParams.timeout;
            var soapRequest =
                string.Format(@"<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:cli=""http://client.cds.med.va.gov"">
                     <soapenv:Header/>
                     <soapenv:Body>
                          <cli:readClinicalData1>
                              <in0>PharmacyRead40010</in0>
                              <in1><![CDATA[
								<?xml version=""1.0"" encoding=""UTF-8""?>
								<filter:filter xmlns:filter=""Filter"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" vhimVersion=""Vhim_4_00"" xsi:schemaLocation=""Filter Untitled1.xml"">
									<filterId>RX_SINGLE_PATIENT_ALL_DATA_FILTER</filterId>
									<clientName>SoapUI_SQA1</clientName>
									<clientRequestInitiationTime>{3}</clientRequestInitiationTime>
								    <patients>
										<NationalId>{0}</NationalId>
									</patients>
									<entryPointFilter queryName=""ID1"">
										<domainEntryPoint>OutpatientMedicationPromise</domainEntryPoint>
										<startDate>{1}</startDate>
										<endDate>{2}</endDate>
										<queryTimeoutSeconds>{4}</queryTimeoutSeconds>
									</entryPointFilter>
                                </filter:filter>]]>
                              </in1>
                              <in2>RX_SINGLE_PATIENT_ALL_DATA_FILTER</in2>
                              <in3>REQUESTID_CDS311_SQA1_HDR_ONLY</in3>
                          </cli:readClinicalData1>
                     </soapenv:Body>
                </soapenv:Envelope>", nationalID, startDate, endDate, clientRequestInitTime, timeout);
            return soapRequest;
        }
    }
}
