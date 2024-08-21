using System.Text.Json.Serialization;

namespace va_veis_healthdatarepo.Models.Entities
{
    public class FPDSAppointment
    {
        public string AppointmentStatus { get; set; }
        public string CategoryCode { get; set; }
        public string CategoryName { get; set; }
        public string CheckOut { get; set; }
        public string DateTime { get; set; }
        public DateTime DateTimeDate { get; set; }
        public string FacilityCode { get; set; }
        public string FacilityName { get; set; }
        public string LocalId { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string LocationName { get; set; }
        public string LocationUid { get; set; }
        public string PatientClassCode { get; set; }
        public string PatientClassName { get; set; }
        public string Service { get; set; }
        public string StopCodeName { get; set; }
        public string StopCodeUid { get; set; }
        public string Summary { get; set; }
        public string TypeCode { get; set; }
        public string TypeName { get; set; }
        public string Uid { get; set; }
    }
}