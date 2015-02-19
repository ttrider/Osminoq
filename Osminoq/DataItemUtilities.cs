using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TTRider.Osminoq
{
    public class DataItemUtilities
    {
        public static string ProcessPattern(string value, Regex pattern)
        {
            if (pattern == null)
            {
                return value;
            }
            var match = pattern.Match(value);
            if (!match.Success)
            {
                return value;
            }

            var group = match.Groups["value"];
            return group.Success ? group.Value : value;
        }
    }
}
