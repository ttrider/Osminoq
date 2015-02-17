using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TTRider.Osminoq
{
    public static class FieldExtractor
    {
        static string ProcessPattern(string value, Regex pattern)
        {
            if (pattern != null)
            {
                var match = pattern.Match(value);
                if (match.Success)
                {
                    var group = match.Groups["value"];
                    if (group.Success)
                    {
                        return group.Value;
                    }
                    return null;
                }
                return null;
            }
            return value;
        }

        public static string ToString(string value, Regex pattern)
        {
            value = ProcessPattern(value, pattern);
            return value;
        }
        public static int? ToInt(string value, Regex pattern)
        {
            value = ProcessPattern(value, pattern);
            int val;
            if (int.TryParse(value, NumberStyles.Number, CultureInfo.CurrentCulture, out val))
            {
                return val;
            }
            return null;
        }
        public static long? ToLong(string value, Regex pattern)
        {
            value = ProcessPattern(value, pattern);
            long val;
            if (long.TryParse(value, NumberStyles.Number, CultureInfo.CurrentCulture, out val))
            {
                return val;
            }
            return null;
        }
        public static double? ToDouble(string value, Regex pattern)
        {
            value = ProcessPattern(value, pattern);
            double val;
            if (double.TryParse(value, NumberStyles.Number, CultureInfo.CurrentCulture, out val))
            {
                return val;
            }
            return null;
        }
        public static Guid? ToGuid(string value, Regex pattern)
        {
            value = ProcessPattern(value, pattern);
            Guid val;
            if (Guid.TryParse(value, out val))
            {
                return val;
            }
            return null;
        }
    }
}
