using System;
using System.ComponentModel.Composition;
using System.Reflection;

namespace TTRider.Osminoq.TypeHandlers
{
    [TypeHandler(TypeName = "bit")]
    [TypeHandler(TypeName = "bool")]
    [TypeHandler(TypeName = "boolean")]
    [TypeHandler(TypeName = "System.Boolean")]
    [Export(typeof(IDataItemTypeHandler))]
    public class BooleanTypeHandler : IDataItemTypeHandler
    {
        private static readonly MethodInfo ConverterMethod = typeof(BooleanTypeHandler).GetMethod("Convert", BindingFlags.Static | BindingFlags.Public);

        public MethodInfo ConverterInfo { get { return ConverterMethod; } }

        public static bool? Convert(string value)
        {
            if (string.IsNullOrWhiteSpace(value)) return null;

            bool val;
            if (bool.TryParse(value, out val))
            {
                return val;
            }

            // let's try numeric form
            int valInt;
            if (int.TryParse(value, out valInt))
            {
                return valInt!=0;
            }

            // let's try yes/no
            if (string.Equals("yes", value, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
            if (string.Equals("no", value, StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }

            return null;
        }
    }
}