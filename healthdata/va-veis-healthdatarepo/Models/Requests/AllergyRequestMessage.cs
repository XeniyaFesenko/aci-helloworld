namespace va_veis_healthdatarepo.Models.Requests
{
    public class AllergyRequestMessage : CDSRequestMessage
    {
        public override string BuildSoapRequest()
        {


            //var soap = SoapMaker.MakeSoapFilter();
            var filterID = "ALLERGY_SINGLE_PATIENT_ALL_DATA_FILTER";
            var nationalID = RequestParams.nationalId;
            var clientName = RequestParams.clientName;
            //soap.NationalId = nationalID;

            // dates are optional?
            var startDate = RequestParams.startDate; // CCYY-MM-DD
            var endDate = RequestParams.endDate;
            var soapRequest =
                string.Format(@"<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:cli=""http://client.cds.med.va.gov"">
                <soapenv:Header/>
                <soapenv:Body>
                <cli:readClinicalData1>
                <in0>AllergiesRead40010</in0>
                <in1><![CDATA[
                <filter:filter xmlns:filter=""Filter"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" vhimVersion=""Vhim_4_00"" xsi:schemaLocation=""Filter Untitled5.xml"">
                <filterId>{0}</filterId>
                <clientName>{1}</clientName>
                <patients>
                <NationalId>{2}</NationalId>
                </patients>
                <entryPointFilter queryName=""ID1"">
                <domainEntryPoint>IntoleranceCondition</domainEntryPoint>                                   
                </entryPointFilter>
                <entryPointFilter queryName=""ID2"">
                <domainEntryPoint>AllergyAssessment</domainEntryPoint>      
                </entryPointFilter>
                </filter:filter>]]>
                </in1>
                <in2>{0}</in2>
                <in3>LP_DEBUG</in3>
                </cli:readClinicalData1>
                </soapenv:Body>
                </soapenv:Envelope>", filterID, clientName, nationalID, startDate, endDate);
            return soapRequest;
        }
    }
}