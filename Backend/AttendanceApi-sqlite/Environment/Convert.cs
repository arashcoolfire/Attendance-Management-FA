using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AttendanceApi.Environment
{
    public class Convert
    {
        public static string TimeSpanToReadableString(TimeSpan span)
        {
            string formatted = string.Format("{0}{1}{2}",
             span.Duration().Hours > 0 ? string.Format("{0:0}{1}h: ", span.Hours, span.Hours == 1 ? String.Empty : "") : string.Empty,
             span.Duration().Minutes > 0 ? string.Format("{0:0}{1}m:", span.Minutes, span.Minutes == 1 ? String.Empty : "") : string.Empty,
             span.Duration().Seconds > 0 ? string.Format("{0:0}{1}s", span.Seconds, span.Seconds == 1 ? String.Empty : "") : string.Empty);


            if (formatted.EndsWith(", ")) formatted = formatted.Substring(0, formatted.Length - 2);

            if (string.IsNullOrEmpty(formatted)) formatted = "0";

            return formatted;
        }
    }
}
