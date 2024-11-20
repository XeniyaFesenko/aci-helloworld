using System.Xml.Serialization;
using va_veis_healthdatarepo.Models.Errors.PWS;

namespace va_veis_healthdatarepo.Models.Entities
{
    [XmlRoot(ElementName = "resultantIdentifier")]
    public class ResultantIdentifier
    {

        [XmlElement(ElementName = "identity")]
        public string Identity { get; set; }

        [XmlElement(ElementName = "assigningFacility")]
        public string AssigningFacility { get; set; }

        [XmlElement(ElementName = "assigningAuthority")]
        public string AssigningAuthority { get; set; }
    }

    [XmlRoot(ElementName = "resultantIdentifiers")]
    public class ResultantIdentifiers
    {

        [XmlElement(ElementName = "resultantIdentifier")]
        public List<ResultantIdentifier> ResultantIdentifier { get; set; }
    }

    [XmlRoot(ElementName = "recordIdentifier")]
    public class RecordIdentifier
    {

        [XmlElement(ElementName = "identity")]
        public double Identity { get; set; }

        [XmlElement(ElementName = "namespaceId")]
        public string NamespaceId { get; set; }
    }

    [XmlRoot(ElementName = "identifier")]
    public class Identifier
    {

        [XmlElement(ElementName = "identity")]
        public string Identity { get; set; }

        [XmlElement(ElementName = "assigningFacility")]
        public string AssigningFacility { get; set; }

        [XmlElement(ElementName = "name")]
        public string Name { get; set; }
    }

    [XmlRoot(ElementName = "patient")]
    public class Patient
    {

        [XmlElement(ElementName = "identifier")]
        public Identifier Identifier { get; set; }

        [XmlElement(ElementName = "requestedNationalId")]
        public string RequestedNationalId { get; set; }

        [XmlElement(ElementName = "resultantIdentifiers")]
        public ResultantIdentifiers ResultantIdentifiers { get; set; }

        [XmlElement(ElementName = "appointments")]
        public Appointments Appointments { get; set; }
    }

    [XmlRoot(ElementName = "appointmentDateTime")]
    public class AppointmentDateTime
    {

        [XmlElement(ElementName = "literal")]
        public long Literal { get; set; }
    }

    [XmlRoot(ElementName = "institution")]
    public class Institution
    {

        [XmlElement(ElementName = "identifier")]
        public Identifier Identifier { get; set; }

        [XmlElement(ElementName = "officialVAName")]
        public string OfficialVAName { get; set; }
    }

    [XmlRoot(ElementName = "location")]
    public class Location
    {

        [XmlElement(ElementName = "identifier")]
        public Identifier Identifier { get; set; }

        [XmlElement(ElementName = "telephone")]
        public string Telephone { get; set; }

        [XmlElement(ElementName = "institution")]
        public Institution Institution { get; set; }
    }

    [XmlRoot(ElementName = "provider")]
    public class AppointmentProvider
    {

        [XmlElement(ElementName = "name_given")]
        public string NameGiven { get; set; }

        [XmlElement(ElementName = "name_middle")]
        public string NameMiddle { get; set; }

        [XmlElement(ElementName = "name_family")]
        public string NameFamily { get; set; }

        [XmlElement(ElementName = "name_title")]
        public string NameTitle { get; set; }

        [XmlElement(ElementName = "display_name")]
        public string DisplayName { get; set; }

        [XmlElement(ElementName = "default_provider")]
        public string DefaultProvider { get; set; }

        [XmlElement(ElementName = "name_suffix")]
        public string NameSuffix { get; set; }
    }

    [XmlRoot(ElementName = "providers")]
    public class Providers
    {

        [XmlElement(ElementName = "provider")]
        public List<AppointmentProvider> Provider { get; set; }
    }

    [XmlRoot(ElementName = "appointmentStatus")]
    public class AppointmentStatus
    {

        [XmlElement(ElementName = "code")]
        public string Code { get; set; }

        [XmlElement(ElementName = "displayText")]
        public string DisplayText { get; set; }
    }

    [XmlRoot(ElementName = "appointmentType")]
    public class AppointmentType
    {

        [XmlElement(ElementName = "code")]
        public int Code { get; set; }

        [XmlElement(ElementName = "displayText")]
        public string DisplayText { get; set; }
    }

    [XmlRoot(ElementName = "status")]
    public class Status
    {

        [XmlElement(ElementName = "code")]
        public int Code { get; set; }

        [XmlElement(ElementName = "displayText")]
        public string DisplayText { get; set; }
    }

    [XmlRoot(ElementName = "appointment")]
    public class Appointment
    {
        [XmlElement(ElementName = "recordIdentifier")]
        public RecordIdentifier RecordIdentifier { get; set; }

        [XmlElement(ElementName = "patient")]
        public Patient Patient { get; set; }

        [XmlElement(ElementName = "appointmentDateTime")]
        public AppointmentDateTime AppointmentDateTime { get; set; }

        [XmlElement(ElementName = "location")]
        public Location Location { get; set; }

        [XmlElement(ElementName = "clinicMeetsAtThisFacility")]
        public string ClinicMeetsAtThisFacility { get; set; }

        [XmlElement(ElementName = "service")]
        public string Service { get; set; }

        [XmlElement(ElementName = "division")]
        public string Division { get; set; }

        [XmlElement(ElementName = "telephoneExtension")]
        public string TelephoneExtension { get; set; }

        [XmlElement(ElementName = "providers")]
        public Providers Providers { get; set; }

        [XmlElement(ElementName = "appointmentStatus")]
        public AppointmentStatus AppointmentStatus { get; set; }

        [XmlElement(ElementName = "appointmentType")]
        public AppointmentType AppointmentType { get; set; }

        [XmlElement(ElementName = "appointmentRequestComment")]
        public string AppointmentRequestComment { get; set; }

        [XmlElement(ElementName = "purposeOfVisitId")]
        public int PurposeOfVisitId { get; set; }

        [XmlElement(ElementName = "purposeOfVisitName")]
        public string PurposeOfVisitName { get; set; }

        [XmlElement(ElementName = "status")]
        public Status Status { get; set; }

        [XmlElement(ElementName = "primaryStopCode")]
        public int PrimaryStopCode { get; set; }

        [XmlElement(ElementName = "secondaryStopCode")]
        public int SecondaryStopCode { get; set; }

        [XmlElement(ElementName = "displayClinicApptToPatient")]
        public string DisplayClinicApptToPatient { get; set; }

        [XmlElement(ElementName = "allowDirectPatientScheduling")]
        public string AllowDirectPatientScheduling { get; set; }

        [XmlElement(ElementName = "other")]
        public string OtherInformation { get; set; }
    }

    [XmlRoot(ElementName = "appointments")]
    public class Appointments
    {

        [XmlElement(ElementName = "appointment")]
        public List<Appointment> Appointment { get; set; }
    }

    [XmlRoot(ElementName = "patients")]
    public class Patients
    {

        [XmlElement(ElementName = "patient")]
        public Patient Patient { get; set; }
    }

    [XmlRoot(ElementName = "AppointmentsData")]
    public class AppointmentsData
    {
        [XmlElement(ElementName = "errorSection")]
        public ErrorSection ErrorSection { get; set; }

        [XmlElement(ElementName = "templateId")]
        public string TemplateId { get; set; }

        [XmlElement(ElementName = "requestId")]
        public string RequestId { get; set; }

        [XmlElement(ElementName = "patients")]
        public Patients Patients { get; set; }
    }
}
