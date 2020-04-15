using System;
using System.Globalization;

namespace BDCO.Domain.Utility
{
    public static class DateTimeExtensions
    {
        public static DateTime? AddTimeInDate(this DateTime? dt, string time)
        {
           
            var ts = DateTime.ParseExact(time.PadLeft(8, '0'), "hh:mm tt", CultureInfo.InvariantCulture).TimeOfDay;
            return dt.GetValueOrDefault().Add(ts);
        }
        public static DateTime ToDateTime(this string s,
                  string format = "yyyy-MM-dd", string cultureString = "tr-TR")
        {
            try
            {
                var r = DateTime.ParseExact(
                    s: s,
                    format: format,
                    provider: CultureInfo.GetCultureInfo(cultureString));
                return r;
            }
            catch (FormatException)
            {
                throw;
            }
            catch (CultureNotFoundException)
            {
                throw; // Given Culture is not supported culture
            }
        }

        public static DateTime ToDateTime(this string s,
                    string format, CultureInfo culture)
        {
            try
            {
                var r = DateTime.ParseExact(s: s, format: format,
                                        provider: culture);
                return r;
            }
            catch (FormatException)
            {
                throw;
            }
            catch (CultureNotFoundException)
            {
                throw; // Given Culture is not supported culture
            }

        }

    }
}
