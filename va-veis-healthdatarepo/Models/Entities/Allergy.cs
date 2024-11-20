using System.Xml.Serialization;

namespace va_veis_healthdatarepo.Models.Entities
{
    public class Allergy
    {
        public AllergyPatient Patient { get; set; }
    }

    [XmlRoot(ElementName = "resultantIdentifiers")]
    public class AllergyResultantIdentifiers
    {
        [XmlElement(ElementName = "identity")]
        public string Identity { get; set; }

        [XmlElement(ElementName = "assigningFacility")]
        public string AssigningFacility { get; set; }

        [XmlElement(ElementName = "assigningAuthority")]
        public string AssigningAuthority { get; set; }
    }

    [XmlRoot(ElementName = "recordIdentifier")]
    public class AllergyRecordIdentifier
    {
        [XmlElement(ElementName = "identity")]
        public string Identity { get; set; }

        [XmlElement(ElementName = "namespaceId")]
        public string NamespaceId { get; set; }
    }

    [XmlRoot(ElementName = "identifier")]
    public class AllergyIdentifier
    {
        [XmlElement(ElementName = "identity")]
        public string Identity { get; set; }

        [XmlElement(ElementName = "assigningFacility")]
        public string AssigningFacility { get; set; }

        [XmlElement(ElementName = "assigningAuthority")]
        public string AssigningAuthority { get; set; }
    }

    [XmlRoot(ElementName = "name")]
    public class AllergyUserName
    {
        [XmlElement(ElementName = "given")]
        public string Given { get; set; }

        [XmlElement(ElementName = "middle")]
        public string Middle { get; set; }

        [XmlElement(ElementName = "family")]
        public string Family { get; set; }

        [XmlElement(ElementName = "prefix")]
        public string Prefix { get; set; }

        [XmlElement(ElementName = "title")]
        public string Title { get; set; }
    }

    [XmlRoot(ElementName = "patient")]
    public class AllergyPatient
    {
        [XmlElement(ElementName = "identifier")]
        public AllergyIdentifier AllergyIdentifier { get; set; }

        [XmlElement(ElementName = "name")]
        public AllergyUserName Name { get; set; }

        public List<IntoleranceConditions> IntoleranceConditions { get; set; }
    }

    [XmlRoot(ElementName = "agent")]
    public class Agent
    {
        [XmlElement(ElementName = "code")]
        public string Code { get; set; }
    }

    [XmlRoot(ElementName = "allergyType")]
    public class AllergyType
    {
        [XmlElement(ElementName = "code")]
        public string Code { get; set; }

        [XmlElement(ElementName = "displayText")]
        public string DisplayText { get; set; }

        [XmlElement(ElementName = "codingSystem")]
        public string CodingSystem { get; set; }
    }

    [XmlRoot(ElementName = "gmrAllergyAgent")]
    public class GmrAllergyAgent
    {
        [XmlElement(ElementName = "code")]
        public string Code { get; set; }

        [XmlElement(ElementName = "displayText")]
        public string DisplayText { get; set; }

        [XmlElement(ElementName = "codingSystem")]
        public string CodingSystem { get; set; }
    }

    [XmlRoot(ElementName = "informationSourceCategory")]
    public class InformationSourceCategory
    {
        [XmlElement(ElementName = "code")]
        public string Code { get; set; }

        [XmlElement(ElementName = "displayText")]
        public string DisplayText { get; set; }

        [XmlElement(ElementName = "codingSystem")]
        public string CodingSystem { get; set; }
    }

    [XmlRoot(ElementName = "reaction")]
    public class Reaction
    {
        [XmlElement(ElementName = "code")]
        public string Code { get; set; }

        [XmlElement(ElementName = "displayText")]
        public string DisplayText { get; set; }

        [XmlElement(ElementName = "codingSystem")]
        public string CodingSystem { get; set; }
    }

    [XmlRoot(ElementName = "commentEvents")]
    public class CommentEvents
    {
        [XmlElement(ElementName = "comments")]
        public string Comments { get; set; }
    }

    [XmlRoot(ElementName = "mechanism")]
    public class Mechanism
    {
        [XmlElement(ElementName = "code")]
        public string Code { get; set; }

        [XmlElement(ElementName = "displayText")]
        public string DisplayText { get; set; }

        [XmlElement(ElementName = "codingSystem")]
        public string CodingSystem { get; set; }
    }

    [XmlRoot(ElementName = "drugClass")]
    public class DrugClass
    {
        [XmlElement(ElementName = "code")]
        public DrugClassCode Code { get; set; }
    }

    [XmlRoot(ElementName = "code")]
    public class DrugClassCode
    {
        [XmlElement(ElementName = "code")]
        public string Code { get; set; }

        [XmlElement(ElementName = "displayText")]
        public string DisplayText { get; set; }

        [XmlElement(ElementName = "codingSystem")]
        public string CodingSystem { get; set; }

        [XmlElement(ElementName = "alternateCode")]
        public string AlternateCode { get; set; }

        [XmlElement(ElementName = "alternateDisplayText")]
        public string AlternateDisplayText { get; set; }

        [XmlElement(ElementName = "alternateCodingSystem")]
        public string AlternateCodingSystem { get; set; }
    }

    [XmlRoot(ElementName = "facilityIdentifier")]
    public class FacilityIdentifier
    {
        [XmlElement(ElementName = "identity")]
        public string Identity { get; set; }

        [XmlElement(ElementName = "name")]
        public string Name { get; set; }

        [XmlElement(ElementName = "assigningAuthority")]
        public string AssigningAuthority { get; set; }
    }

    [XmlRoot(ElementName = "recordSource")]
    public class RecordSource
    {
        [XmlElement(ElementName = "namespaceId")]
        public string NamespaceId { get; set; }

        [XmlElement(ElementName = "universalId")]
        public string UniversalId { get; set; }

        [XmlElement(ElementName = "universalIdType")]
        public string UniversalIdType { get; set; }
    }

    [XmlRoot(ElementName = "recordUpdateTime")]
    public class RecordUpdateTime
    {
        [XmlElement(ElementName = "literal")]
        public string Literal { get; set; }
    }

    [XmlRoot(ElementName = "intoleranceConditions")]
    public class IntoleranceConditions
    {
        [XmlElement(ElementName = "recordIdentifier")]
        public AllergyRecordIdentifier AllergyRecordIdentifier { get; set; }

        [XmlElement(ElementName = "patient")]
        public AllergyPatient Patient { get; set; }

        [XmlElement(ElementName = "agent")]
        public Agent Agent { get; set; }

        [XmlElement(ElementName = "allergyType")]
        public AllergyType AllergyType { get; set; }

        [XmlElement(ElementName = "gmrAllergyAgent")]
        public GmrAllergyAgent GmrAllergyAgent { get; set; }

        [XmlElement(ElementName = "informationSourceCategory")]
        public InformationSourceCategory InformationSourceCategory { get; set; }

        [XmlElement(ElementName = "status")]
        public string Status { get; set; }

        [XmlElement(ElementName = "commentEvents")]
        public CommentEvents CommentEvents { get; set; }

        [XmlElement(ElementName = "facilityIdentifier")]
        public FacilityIdentifier FacilityIdentifier { get; set; }

        [XmlElement(ElementName = "recordSource")]
        public RecordSource RecordSource { get; set; }

        [XmlElement(ElementName = "recordVersion")]
        public string RecordVersion { get; set; }

        [XmlElement(ElementName = "recordUpdateTime")]
        public RecordUpdateTime RecordUpdateTime { get; set; }

        [XmlElement(ElementName = "observationTime")]
        public ObservationTime ObservationTime { get; set; }

        [XmlElement(ElementName = "author")]
        public Author Author { get; set; }

        [XmlElement(ElementName = "verified")]
        public string Verified { get; set; }

        [XmlElement(ElementName = "mechanism")]
        public Mechanism Mechanism { get; set; }

        [XmlElement(ElementName = "reaction")]
        public List<Reaction> Reactions { get; set; }

        [XmlElement(ElementName = "drugClass")]
        public List<DrugClass> DrugClasses { get; set; }
    }

    [XmlRoot(ElementName = "observationTime")]
    public class ObservationTime
    {
        [XmlElement(ElementName = "literal")]
        public string Literal { get; set; }

        [XmlElement(ElementName = "date")]
        public DateTime Date { get; set; }
    }

    [XmlRoot(ElementName = "practitioner")]
    public class Practitioner
    {
        [XmlElement(ElementName = "identifier")]
        public AllergyIdentifier AllergyIdentifier { get; set; }

        [XmlElement(ElementName = "name")]
        public AllergyUserName Name { get; set; }
    }

    [XmlRoot(ElementName = "author")]
    public class Author
    {
        [XmlElement(ElementName = "practitioner")]
        public Practitioner Practitioner { get; set; }
    }

    [XmlRoot(ElementName = "ClinicalData", Namespace = "Clinicaldata")]
    public class ClinicalData
    {
        [XmlElement(ElementName = "templateId")]
        public string TemplateId { get; set; }

        [XmlElement(ElementName = "requestId")]
        public string RequestId { get; set; }

        [XmlElement(ElementName = "patient")]
        public AllergyPatient Patient { get; set; }

        [XmlAttribute(AttributeName = "clinicaldata", Namespace = "http://www.w3.org/2000/xmlns/")]
        public string Clinicaldata { get; set; }
    }
}
