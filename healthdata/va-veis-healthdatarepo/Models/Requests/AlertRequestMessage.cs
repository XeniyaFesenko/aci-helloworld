namespace va_veis_healthdatarepo.Models.Requests
{
    public class AlertRequestMessage : CDSRequestMessage
    {
        public override string BuildSoapRequest()
        {
            var filterID = "USER_ALERTS";

            // dates are optional?
            var startDate = RequestParams.clientRequestInitiationTime;
            var soapRequest =
                string.Format(@"<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:cli=""http://client.cds.med.va.gov"">
<soapenv:Header/>
    <soapenv:Body>
        <cli:readClinicalData1>
            <in0>UserAlertRead</in0>
            <in1><![CDATA[<?xml version=""1.0"" encoding=""UTF - 8""?>
<filter:filter xmlns:filter=""Filter"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" vhimVersion=""Vhim_4_00"" xsi:schemaLocation=""Filter USER_ALERT_FILTER.xsd"">
    <filterId>{0}</filterId>
    <clientName>{1}</clientName>
    <clientRequestInitiationTime>{2}</clientRequestInitiationTime>
        <users>
            {3}
        </users>
    <entryPointFilter queryName=""ID1"" pageSize=""1"" isPatientCentric=""false"">
        <domainEntryPoint>UserAlerts</domainEntryPoint>
        <queryTimeoutSeconds>1000</queryTimeoutSeconds>
    </entryPointFilter>
</filter:filter>
]]></in1>
            <in2>{0}</in2>
            <in3>{4}</in3>
        </cli:readClinicalData1>
    </soapenv:Body>
</soapenv:Envelope>", filterID, RequestParams.clientName, startDate, RequestParams.users, RequestParams.messageId);
            return soapRequest;
        }
    }
}
