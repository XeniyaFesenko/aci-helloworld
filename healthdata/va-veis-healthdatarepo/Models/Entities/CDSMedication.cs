namespace va_veis_healthdatarepo.Models.Entities
{
    public class CDSMedication
    {
        public string PrescriptionId { get; set; }
        public string CopayIndicator { get; set; }
        public string Name { get; set; }
        public string QuantityOrdered { get; set; }
        public string VaStatus { get; set; }
        public string DaysExpired { get; internal set; }
        public string IssueDate { get; set; }
        public string LastFilledDate { get; set; }
        public string VaRouting { get; set; }
        public string FillsRemaining { get; set; }
        public string DaysSupply { get; set; }
        public string FillDate { get; set; }
        public string Sig { get; set; }
        public string Expires { get; set; }
        public string ProviderName { get; set; }
        public string Clinic { get; set; }
        public string FacilityName { get; set; }
        public string FacilityCode { get; set; }
        public string Remarks { get; set; }
    }
}
