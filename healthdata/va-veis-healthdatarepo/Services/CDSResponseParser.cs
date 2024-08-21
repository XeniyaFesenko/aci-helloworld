using System.Xml.Linq;
using System.Xml.Serialization;
using System.Xml.XPath;
using va_veis_healthdatarepo.Models.Entities;
using va_veis_healthdatarepo.Services.Interfaces;

namespace va_veis_healthdatarepo.Services
{
    public class CDSResponseParser : ICDSResponseParser
    {
        // copied from old health data repo
        public List<CDSAllergy> ParseAllergyFromXMLResponse(string dataRawXMLResponse) 
        {
            CheckForExceptions(dataRawXMLResponse);
            var patientIntoleranceConditionList = new List<IntoleranceConditions>();
            try
            {
                // parse allergies out of response
                var respXML = XDocument.Parse(dataRawXMLResponse);
                if (respXML.Root != null)
                {
                    var outputXML = XDocument.Parse(respXML.Root.Value);
                    if (outputXML.Root != null)
                    {
                        var conditions = outputXML.Root.Descendants("intoleranceConditions");
                        foreach (var condition in conditions)
                        {
                            // build new allergies xmldoc
                            var xDeclare = new XDeclaration("1.0", "UTF-8", "yes");
                            var patientXMLDoc = new XDocument(xDeclare, null);

                            // handle nested reaction elements
                            var reactions = condition.Elements("reaction");
                            var parentReactionsToRemove = new List<XElement>();

                            foreach (var parentReaction in reactions)
                            {
                                if (parentReaction != null)
                                {
                                    var childReaction = parentReaction.Element("reaction");
                                    if (childReaction != null)
                                    {
                                        parentReactionsToRemove.Add(parentReaction);
                                        condition.Add(childReaction);
                                    }
                                }
                            }

                            // Don't remove these in the loop above. It messes up the iteration process.
                            foreach (var parentReaction in parentReactionsToRemove)
                            {
                                parentReaction.Remove();
                            }

                            patientXMLDoc.Add(condition);

                            // serialize into allergy
                            var serializer = new XmlSerializer(typeof(IntoleranceConditions));
                            using TextReader reader = new StringReader(patientXMLDoc.ToString());
                            var patientIntoleranceCondition = (IntoleranceConditions)serializer.Deserialize(reader);
                            patientIntoleranceConditionList.Add(patientIntoleranceCondition);
                        }
                    }
                }
                var serviceResponse = new List<Allergy>
                {
                    new() {
                        Patient = new()
                        {
                            IntoleranceConditions = patientIntoleranceConditionList
                        }
                    }
                };
                var data = TransformAllergies(serviceResponse);

                return data;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private List<CDSAllergy> TransformAllergies(List<Allergy> allergyList)
        {
            var cdsAllergyList = new List<CDSAllergy>();
            foreach (var allergy in allergyList)
            {
                foreach (var condition in allergy.Patient.IntoleranceConditions)
                {
                    var item = new CDSAllergy { Status = condition.Status };
                    const string notApplicable = "n/a";

                    if (condition.Patient != null && condition.Patient.AllergyIdentifier != null)
                    {
                        item.PatientAssigningFacility = condition.Patient.AllergyIdentifier.AssigningFacility;
                        item.PatientAssigningAuthority = condition.Patient.AllergyIdentifier.AssigningAuthority;
                    }
                    else
                    {
                        item.PatientAssigningFacility = notApplicable;
                        item.PatientAssigningAuthority = notApplicable;
                    }

                    if (condition.AllergyType != null)
                    {
                        item.TypeCode = condition.AllergyType.Code;
                        item.TypeName = condition.AllergyType.DisplayText;
                    }
                    else
                    {
                        item.TypeCode = notApplicable;
                        item.TypeName = notApplicable;
                    }

                    if (condition.GmrAllergyAgent != null)
                    {
                        item.AllergyAgentCode = condition.GmrAllergyAgent.Code;
                        item.AllergyAgentName = condition.GmrAllergyAgent.DisplayText;
                    }
                    else
                    {
                        item.AllergyAgentCode = notApplicable;
                        item.AllergyAgentName = notApplicable;
                    }

                    if (condition.InformationSourceCategory != null)
                    {
                        item.SourceCategoryCode = condition.InformationSourceCategory.Code;
                        item.SourceCategoryName = condition.InformationSourceCategory.DisplayText;
                    }
                    else
                    {
                        item.SourceCategoryCode = notApplicable;
                        item.SourceCategoryName = notApplicable;
                    }

                    if (condition.FacilityIdentifier != null)
                    {
                        item.FacilityCode = condition.FacilityIdentifier.Identity;
                        item.FacilityName = condition.FacilityIdentifier.Name;
                    }
                    else
                    {
                        item.FacilityCode = notApplicable;
                        item.FacilityName = notApplicable;
                    }

                    if (condition.Author != null && condition.Author.Practitioner != null && condition.Author.Practitioner.Name != null)
                    {
                        var first = condition.Author.Practitioner.Name.Given;
                        var middle = condition.Author.Practitioner.Name.Middle;
                        var last = condition.Author.Practitioner.Name.Family;
                        var prefix = condition.Author.Practitioner.Name.Prefix;
                        var authorName = last + ", ";

                        if (!string.IsNullOrWhiteSpace(prefix))
                        {
                            authorName += prefix + " ";
                        }

                        authorName += first;

                        if (!string.IsNullOrWhiteSpace(middle))
                        {
                            authorName += " " + middle;
                        }

                        item.AuthorName = authorName;
                        item.AuthorTitle = condition.Author.Practitioner.Name.Title;
                    }
                    else
                    {
                        item.AuthorName = notApplicable;
                    }

                    if (condition.Reactions != null && condition.Reactions.Any())
                    {
                        item.Reactions = string.Join("; ",
                            condition.Reactions
                                .Where(r => !string.IsNullOrWhiteSpace(r.DisplayText))
                                .Select(r => r.DisplayText));
                    }
                    else
                    {
                        item.Reactions = notApplicable;
                    }

                    if (condition.DrugClasses != null && condition.DrugClasses.Any())
                    {
                        item.DrugClasses = string.Join("; ",
                            condition.DrugClasses
                                .Where(dc => dc.Code != null && !string.IsNullOrWhiteSpace(dc.Code.DisplayText))
                                .Select(dc => dc.Code.DisplayText));
                    }
                    else
                    {
                        item.DrugClasses = notApplicable;
                    }

                    item.AgentCode = condition.Agent != null ? condition.Agent.Code : notApplicable;
                    item.Comments = condition.CommentEvents != null ? condition.CommentEvents.Comments : notApplicable;
                    item.Mechanism = condition.Mechanism != null ? condition.Mechanism.DisplayText : notApplicable;
                    item.ObservationTime = condition.ObservationTime != null ? condition.ObservationTime.Literal : notApplicable;
                    item.RecordUpdateTime = condition.RecordUpdateTime != null ? condition.RecordUpdateTime.Literal : notApplicable;
                    item.Verified = condition.Verified;

                    cdsAllergyList.Add(item);
                }
            }

            return cdsAllergyList;
        }

        private void CheckForExceptions(string dataRawXMLResponse)
        {
            var content = dataRawXMLResponse;
            if (content.Contains("IDM_SERVICE_EXCEPTION"))
            {
                throw new ApplicationException("CDS Returned Service Exception [IDM_SERVICE_EXCEPTION].");
            }

            if (content.Contains("READ_REQUEST_ALL_DATASOURCES_FAILED"))
            {
                throw new ApplicationException("CDS Returned Service Exception [READ_REQUEST_ALL_DATASOURCES_FAILED].");
            }
            if (content.Contains("System Exception recorded") && content.Contains("An Error has occured please notifiy HDR Administror if it continues"))
            {
                var respXML = XDocument.Parse(dataRawXMLResponse);
                if (respXML.Root != null)
                {
                    var respRawNav = respXML.CreateNavigator();

                    respRawNav.MoveToRoot();
                    var outputNode = respRawNav.SelectDescendants("out", "", true).Current;
                    var outputContent = outputNode.Value;
                    var start = outputContent.IndexOf("<errorCode>");
                    var end = outputContent.IndexOf("<", start + 12);
                    var errorCode = outputContent.Substring(start + 11, end - (start + 11));
                    if (!string.IsNullOrEmpty(errorCode))
                    {
                        throw new ApplicationException($"{errorCode}");
                    }
                    else
                    {
                        throw new ApplicationException("An Error has occured please notifiy HDR Administror if it continues");
                    }
                }
            }
        }
    }
}
