using System;
using System.ComponentModel.Composition;
using System.Reflection;

namespace TTRider.Osminoq.TypeHandlers
{
    [TypeHandler(TypeName = "date")]
    [TypeHandler(TypeName = "time")]
    [TypeHandler(TypeName = "datetime")]
    [TypeHandler(TypeName = "System.DateTime")]
    [Export(typeof(IDataItemTypeHandler))]
    public class DateTimeTypeHandler : IDataItemTypeHandler
    {
        private static readonly MethodInfo ConverterMethod = typeof(DateTimeTypeHandler).GetMethod("Convert", BindingFlags.Static | BindingFlags.Public);

        public MethodInfo ConverterInfo { get { return ConverterMethod; } }

        public static DateTime? Convert(string value)
        {
            if (string.IsNullOrWhiteSpace(value)) return null;

            DateTime val;
            if (DateTime.TryParse(value, out val))
            {
                return val;
            }
            return null;
        }
    }
}