namespace va_veis_healthdatarepo.Models.Requests
{
    public class AppointmentRequestMessage : PathwaysRequestMessage
    {
        private readonly string _clientName;
        private readonly string _endDate;
        private readonly string _nationalId;
        private readonly string _startDate;

        public AppointmentRequestMessage(string clientName, string nationalId, string startDate, string endDate) : base(clientName)
        {
            if (string.IsNullOrEmpty(nationalId)) throw new ArgumentNullException("nationalId");
            if (string.IsNullOrEmpty(startDate)) throw new ArgumentNullException("startDate");
            if (string.IsNullOrEmpty(endDate)) throw new ArgumentNullException("endDate");
            DateTime dateTime;
            if (!DateTime.TryParseExact(startDate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.NoCurrentDateDefault, out dateTime))
            {
                throw new ApplicationException("Invalid date format for StartDate. Please use yyyy-MM-dd format.");
            }

            if (!DateTime.TryParseExact(endDate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.NoCurrentDateDefault, out dateTime))
            {
                throw new ApplicationException("Invalid date format for EndDate. Please use yyyy-MM-dd format.");
            }

            ContentType = "application/xml";

            _nationalId = nationalId;
            _startDate = startDate;
            _endDate = endDate;
            _clientName = clientName;
        }

        public override string GetPathwaysSoapBody()
        {
            var serviceCallDate = DateTime.Now.ToString("yyyy-MM-dd'T'hh:mm:ss");
            var filterID = "APPOINTMENTS_SINGLE_PATIENT_FILTER";
            var soapRequest = string.Format(
                @"<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:pat=""http://repositories.med.va.gov/pathways"">
                   <soapenv:Header/>
                   <soapenv:Body>
                      <pat:readData>
                         <in0>AppointmentsRead1</in0>
                         <in1>        
                            <![CDATA[
                                <filter:filter xmlns:filter=""Filter"" vhimVersion=""Vhim_4_00"">
                                    <filterId>{0}</filterId>
                                    <clientName>{5}</clientName>
                                    <clientRequestInitiationTime>{1}</clientRequestInitiationTime>
                                    <patients>
                                        <NationalId>{2}</NationalId>
                                    </patients>
                                    <entryPointFilter queryName=""Appointment-Standardized"">
                                        <domainEntryPoint>Appointment</domainEntryPoint>
                                        <startDate>{3}</startDate>
                                        <endDate>{4}</endDate>
                                    </entryPointFilter>
                                </filter:filter>
                            ]]>
                         </in1>
                         <in2>{0}</in2>
                         <in3>{5}</in3>
                      </pat:readData>
                   </soapenv:Body>
                 </soapenv:Envelope>", filterID, serviceCallDate, _nationalId, _startDate, _endDate, _clientName);
            return soapRequest;
        }
    }
}
