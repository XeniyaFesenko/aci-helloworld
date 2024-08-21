namespace va_veis_healthdatarepo.Mappers
{
    public static class DateTransformer
    {
        public static DateTime DefaultConvertedDate { get; set; }

        /*
         * Evaluate dateString parameter to determine
         * which supported DateParser to use
         * 
         * If not a numeric string or unrecognizable 
         * format, return DEFAULT_CONVERTED_DATE
         * 
         * Supported Date Formats
         * MM/DD/CCYY : 01/01/1999
         * CCYYMMDD : 19990804
         * CCYYMMDDHHMM : 199908040526
         * CCYYMMDDMHMM : 199908041726
         * CCYYMMDDHHMMSS : 19990804052645
         * CCYYMMDDMHMMSS : 19990804172645
         * YYYMMDD.HHMM : 2941206.1421 - Vista date format
         */

        public static DateTime ParseDateFromString(string dateString)
        {
            if (string.IsNullOrEmpty(dateString)) throw new ArgumentNullException("dateString");

            if (dateString.IndexOf("/", StringComparison.Ordinal) == 2 && dateString.Length == 10)
            {
                return ParseDateFromStringMMDDCCYY(dateString);
            }

            // if vista date format 2941206.1421
            if (dateString.IndexOf(".", StringComparison.Ordinal) == 7)
            {
                return ParseDateFromStringYYYMMDDHHMM(dateString);
            }


            if (!IsNumeric(dateString))
            {
                // dateString not numeric
                return DefaultConvertedDate;
            }

            // CCYYMMDD
            switch (dateString.Length)
            {
                case 8:
                    return ParseDateFromStringCCYYMMDD(dateString);
                case 12:
                    var hourString = dateString.Substring(8, 2);
                    int hour;
                    var hourParsedOK = int.TryParse(hourString, out hour);

                    if (hourParsedOK)
                    {
                        return hour >= 12 ? ParseDateFromStringCCYYMMDDMHMM(dateString) : ParseDateFromStringCCYYMMDDHHMM(dateString);
                    }
                    break;
            }

            // CCYYMMDDHHMM or CCYYMMDDMHMM

            // CCYYMMDDHHMMSS or CCYYMMDDMHMMSS
            if (dateString.Length != 14) return DefaultConvertedDate;
            {
                var hourString = dateString.Substring(8, 2);
                int hour;
                var hourParsedOK = int.TryParse(hourString, out hour);
                if (!hourParsedOK) return DefaultConvertedDate;
                return hour >= 12 ? ParseDateFromStringCCYYMMDDMHMMSS(dateString) : ParseDateFromStringCCYYMMDDHHMMSS(dateString);
            }

        }

        // check if dateString is numeric
        internal static bool IsNumeric(string dateString)
        {
            double numericDate;
            var dateIsANumber = double.TryParse(dateString, out numericDate);

            return dateIsANumber;
        }

        #region Military Clock
        internal static DateTime ParseDateFromStringCCYYMMDDMHMMSS(string dateString)
        {
            if (string.IsNullOrEmpty(dateString)) throw new ArgumentNullException("dateString");

            var dateFormat = "yyyyMMddHHmmss";

            return TryParseDate(dateString, dateFormat);
        }

        internal static DateTime ParseDateFromStringCCYYMMDDMHMM(string dateString)
        {
            if (string.IsNullOrEmpty(dateString)) throw new ArgumentNullException("dateString");

            var dateFormat = "yyyyMMddHHmm";

            return TryParseDate(dateString, dateFormat);
        }

        #endregion

        #region 12 Hour Clock

        internal static DateTime ParseDateFromStringCCYYMMDDHHMMSS(string dateString)
        {
            if (string.IsNullOrEmpty(dateString)) throw new ArgumentNullException("dateString");

            var dateFormat = "yyyyMMddhhmmss";

            return TryParseDate(dateString, dateFormat);
        }

        internal static DateTime ParseDateFromStringCCYYMMDDHHMM(string dateString)
        {
            if (string.IsNullOrEmpty(dateString)) throw new ArgumentNullException("dateString");

            var dateFormat = "yyyyMMddhhmm";

            return TryParseDate(dateString, dateFormat);
        }

        internal static DateTime ParseDateFromStringYYYMMDDHHMM(string dateString)
        {
            if (string.IsNullOrEmpty(dateString)) throw new ArgumentNullException("dateString");

            //
            // convert from <yyy>MMDDHHMM (vista date) to yyyyMMddhhmm
            //
            var dateFormat = "yyyyMMddHHmm";
            var newYearValue = string.Empty;
            var newTimeValue = string.Empty;
            var monthDayValue = dateString.Substring(3, 4);

            //convert from Vista year to CCYY
            var year = dateString.Substring(0, 3);
            int yearNumber;

            var isDate = int.TryParse(year, out yearNumber);
            if (isDate)
            {
                newYearValue = (1700 + yearNumber).ToString();
            }
            else
            {
                throw new ApplicationException(string.Format("Error converting Vista year value: {0}", dateString));
            }

            // right pad time with 0 as the date is stored as a numeric in vista and traling 0s are dropped
            if (dateString.IndexOf(".", StringComparison.Ordinal) > 0)
            {
                var time = dateString.Substring(dateString.IndexOf(".", StringComparison.Ordinal) + 1);
                newTimeValue = time.PadRight(4, '0');
            }
            else
            {
                throw new ApplicationException(string.Format("Error converting Vista year value: {0}", dateString));
            }

            return TryParseDate(string.Format("{0}{1}{2}", newYearValue, monthDayValue, newTimeValue), dateFormat);
        }

        #endregion

        #region Date Only
        internal static DateTime ParseDateFromStringCCYYMMDD(string dateString)
        {
            if (string.IsNullOrEmpty(dateString)) throw new ArgumentNullException("dateString");

            var dateFormat = "yyyyMMdd";

            return TryParseDate(dateString, dateFormat);
        }

        internal static DateTime ParseDateFromStringMMDDCCYY(string dateString)
        {
            if (string.IsNullOrEmpty(dateString)) throw new ArgumentNullException("dateString");

            var dateFormat = "MM/dd/yyyy";

            return TryParseDate(dateString, dateFormat);
        }


        internal static DateTime TryParseDate(string dateString, string dateFormat)
        {
            DateTime parsedDate;

            var parsedOK = DateTime.TryParseExact(dateString,
                dateFormat,
                System.Globalization.CultureInfo.InvariantCulture,
                System.Globalization.DateTimeStyles.None,
                out parsedDate);

            return parsedOK ? parsedDate : DefaultConvertedDate;
        }

        #endregion
    }
}
