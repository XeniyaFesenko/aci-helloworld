using va_veis_healthdatarepo.Models.Entities;

namespace va_veis_healthdatarepo.Mappers
{
    public class AlertTransformer
    {
        public List<CDSAlert> TransformAlerts(List<AlertUser> alertsList)
        {
            var cdsAlertList = new List<CDSAlert>();
            foreach (var alert in alertsList)
            {
                var cdsAlert = new CDSAlert
                {
                    User = new CDSUser
                    {
                        Duz = alert.Duz.ToString(),
                        Name = alert.Name,
                        Facility = alert.Facility.ToString(),
                        LastSignOnDateTime = alert.LastSignOnDateTime.ToString(),
                        Alerts = new List<CDSUserAlert>()
                    }
                };

                if (alert.Alerts != null)
                {
                    foreach (var userAlert in alert.Alerts.Alert)
                    {
                        var cdsUserAlert = new CDSUserAlert
                        {
                            AlertId = userAlert.AlertId,
                            DateTime = userAlert.DateTime.ToString(),
                            Message = userAlert.Message,
                            Patient = userAlert.Patient,
                            OrderingProvider = userAlert.OrderingProvider,
                            Urgency = userAlert.Urgency
                        };

                        cdsAlert.User.Alerts.Add(cdsUserAlert);
                    }
                }

                cdsAlertList.Add(cdsAlert);
            }

            return cdsAlertList;
        }
    }
}
