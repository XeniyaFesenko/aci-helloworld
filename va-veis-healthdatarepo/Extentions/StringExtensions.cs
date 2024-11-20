using System.Text.RegularExpressions;

namespace va_veis_healthdatarepo.Extentions
{
    public static class StringExtensions
    {
        public static string ToDateTimeString(this string dateTimeString, string outputFormat, DateTime? defaultDateTime = null)
        {
            var defaultString = defaultDateTime != null
                ? ((DateTime)defaultDateTime).ToString(outputFormat)
                : "";

            return dateTimeString.ToDateTimeString(outputFormat, defaultString);
        }

        public static string ToDateTimeString(this string dateTimeString, string outputFormat, string defaultString)
        {
            if (string.IsNullOrEmpty(dateTimeString))
            {
                return defaultString;
            }

            var regex = new Regex(@"\d{8}");

            // Some FPDS dates come back as yyyyMMdd, which isn't valid according to DateTime.Parse.
            // This turns it them into yyyy-MM-dd format, which is valid.
            if (regex.IsMatch(dateTimeString))
            {
                // Date
                if (dateTimeString.Length < 12)
                {
                    dateTimeString = string.Format("{0}-{1}-{2}",
                        dateTimeString.Substring(0, 4),
                        dateTimeString.Substring(4, 2),
                        dateTimeString.Substring(6, 2));
                }
                // DateTime
                else
                {
                    dateTimeString = string.Format("{0}-{1}-{2} {3}:{4}",
                        dateTimeString.Substring(0, 4),
                        dateTimeString.Substring(4, 2),
                        dateTimeString.Substring(6, 2),
                        dateTimeString.Substring(8, 2),
                        dateTimeString.Substring(10, 2));
                }
            }

            DateTime dateTime;
            var parsedDate = DateTime.TryParse(dateTimeString, out dateTime);

            // We only get the min DateTime value when a non-nullable DateTime isn't given a value,
            // so we want that case to fail and resort to the default that's passed in.
            return !parsedDate || dateTime == DateTime.MinValue
                ? defaultString
                : dateTime.ToString(outputFormat);
        }

        public static string Truncate(this string originalString, int charactersToDisplay, bool addEllipses = false)
        {
            if (string.IsNullOrEmpty(originalString) ||
                originalString.Length <= charactersToDisplay ||
                charactersToDisplay < 1)
            {
                return originalString;
            }
            var pattern = @",\S";
            var regex = new Regex(pattern);
            if (regex.IsMatch(originalString))
            {
                originalString = originalString.Replace(",", ", ");
            }
            return originalString
                       .Substring(0, charactersToDisplay) +
                   (addEllipses ? "..." : "");
        }
    }
}
