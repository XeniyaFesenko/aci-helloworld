namespace va_veis_healthdatarepo.Models.Entities
{
    public class PWSAppointment
    {
        public PWSAppointment()
        {
            FacilityName = "";
            FacilityCode = "";
            ClinicName = "";
            ClinicCode = "";
            StatusCode = "";
            StatusName = "";
            TypeName = "";
            LocalID = "";
            DateTime = new DateTime(1970, 1, 1).ToShortDateString();
            TypeCode = "";
        }
        public string DateTime { get; set; }
        public DateTime DateTimeDate { get; set; }

        public string AssigningFacility { get; set; }

        public string ClinicCode { get; set; }
        public string ClinicName { get; set; }

        public string FacilityCode { get; set; }
        public string FacilityName { get; set; }

        public string OtherInformation { get; set; }

        public string StatusCode { get; set; }
        public string StatusName { get; set; }

        public string TypeCode { get; set; }
        public string TypeName { get; set; }

        public string AppointmentStatusCode { get; set; }
        public string AppointmentStatusName { get; set; }

        public string LocalID { get; set; }
    }
}
