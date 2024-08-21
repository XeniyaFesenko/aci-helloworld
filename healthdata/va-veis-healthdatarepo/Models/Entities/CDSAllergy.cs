namespace va_veis_healthdatarepo.Models.Entities
{
    public class CDSAllergy
    {
        public string AgentCode { get; set; }
        public string AllergyAgentCode { get; set; }
        public string AllergyAgentName { get; set; }
        public string AuthorName { get; set; }
        public string AuthorTitle { get; set; }
        public string Comments { get; set; }
        public string DrugClasses { get; set; }
        public string FacilityCode { get; set; }
        public string FacilityName { get; set; }
        public string Mechanism { get; set; }
        public string ObservationTime { get; set; }
        public DateTime ObservationTimeDate { get; set; }
        public string PatientAssigningAuthority { get; set; }
        public string PatientAssigningFacility { get; set; }
        public string Reactions { get; set; }
        public string RecordUpdateTime { get; set; }
        public DateTime RecordUpdateTimeDate { get; set; }
        public string SourceCategoryCode { get; set; }
        public string SourceCategoryName { get; set; }
        public string Status { get; set; }
        public string TypeCode { get; set; }
        public string TypeName { get; set; }
        public string Verified { get; set; }
    }
}
