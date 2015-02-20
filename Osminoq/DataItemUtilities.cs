using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TTRider.Osminoq
{
    public static class DataItemUtilities
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





        public static IEnumerable<string> GetPropertyNames(this IDataItem dataItem)
        {
            if (dataItem != null)
            {
                var type = dataItem.GetType();
                foreach (var propName in type.GetProperties(BindingFlags.Public).Select(pi => pi.Name))
                {
                    yield return propName;
                }
            }
        }

        public static object GetPropertyValue(this IDataItem dataItem, string name)
        {
            if (dataItem == null) throw new ArgumentNullException("dataItem");
            if (name == null) throw new ArgumentNullException("name");

            var type = dataItem.GetType();
            var pi = type.GetProperty(name);
            return pi.GetValue(dataItem);
        }


        public static IEnumerable<IDataItem> ReadDataItems(this IDataItemReader dataItemReader)
        {
            if (dataItemReader != null)
            {
                var item = dataItemReader.ReadDataItem();
                while (item != null)
                {
                    yield return item;
                    item = dataItemReader.ReadDataItem();
                }
            }
        }
    }
}
