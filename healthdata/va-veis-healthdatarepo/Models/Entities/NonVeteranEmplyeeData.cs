using System.Xml.Serialization;
using va_veis_healthdatarepo.Models.Errors.PWS;

namespace va_veis_healthdatarepo.Models.Entities
{
    [XmlType(AnonymousType = true, Namespace = "")]
    [XmlRoot(Namespace = "", IsNullable = false)]
    public class NonVeteranEmployeeData
    {
        [XmlElement(Namespace = "", ElementName = "templateId")]
        public string TemplateId { get; set; }

        [XmlElement(Namespace = "", ElementName = "requestId")]
        public string RequestId { get; set; }

        [XmlArray(Namespace = "", ElementName = "patients")]
        [XmlArrayItem("patient", IsNullable = false)]
        public PatientsPatient[] patients { get; set; }
    }

    [XmlType(AnonymousType = true)]
    public class PatientsPatient
    {
        [XmlElement(ElementName = "errorSection")]
        public ErrorSection ErrorSection { get; set; }

        [XmlElement(Namespace = "", ElementName = "requestedNationalId")]
        public string RequestedNationalId { get; set; }

        [XmlArrayItem("resultantIdentifier", IsNullable = false)]
        public PatientsPatientResultantIdentifier[] resultantIdentifiers { get; set; }

        [XmlArrayItem("nonVeteranEmployee", IsNullable = false)]
        public PatientsPatientNonVeteranEmployee[] nonVeteranEmployees { get; set; }
    }

    [XmlType(AnonymousType = true)]
    public class PatientsPatientResultantIdentifier
    {
        [XmlElement(Namespace = "", ElementName = "identity")]
        public string Identity { get; set; }

        [XmlElement(Namespace = "", ElementName = "assigningFacility")]
        public string AssigningFacility { get; set; }

        [XmlElement(Namespace = "", ElementName = "assigningAuthority")]
        public string AssigningAuthority { get; set; }
    }

    [XmlType(AnonymousType = true)]
    public class PatientsPatientNonVeteranEmployee
    {
        public PatientsPatientNonVeteranEmployee()
        {
            identifier = new PatientsPatientNonVeteranEmployeeIdentifier();
            NewPersonIndicator = "";
            VeteranYN = "";
            PrimaryEligibility = "";
        }

        [XmlElement(Namespace = "", ElementName = "identifier")]
        public PatientsPatientNonVeteranEmployeeIdentifier identifier { get; set; }

        [XmlElement(Namespace = "", ElementName = "newPersonIndicator")]
        public string NewPersonIndicator { get; set; }

        [XmlElement(Namespace = "", ElementName = "veteranYN")]
        public string VeteranYN { get; set; }

        [XmlElement(Namespace = "", ElementName = "primaryEligibility")]
        public string PrimaryEligibility { get; set; }
    }

    [XmlType(AnonymousType = true)]
    public class PatientsPatientNonVeteranEmployeeIdentifier
    {
        [XmlElement(Namespace = "", ElementName = "dfn")]
        public uint Dfn { get; set; }

        [XmlElement(Namespace = "", ElementName = "assigningFacility")]
        public int AssigningFacility { get; set; }
    }
}
