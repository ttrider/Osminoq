using System.ComponentModel.Composition;
using System.Reflection;

namespace TTRider.Osminoq.TypeHandlers
{
    [TypeHandler(TypeName = "number")]
    [TypeHandler(TypeName = "float")]
    [TypeHandler(TypeName = "double")]
    [TypeHandler(TypeName = "System.Double")]
    [TypeHandler(TypeName = "System.Float")]
    [Export(typeof(IDataItemTypeHandler))]
    public class DoubleTypeHandler : IDataItemTypeHandler
    {
        private static readonly MethodInfo ConverterMethod = typeof(DoubleTypeHandler).GetMethod("Convert", BindingFlags.Static | BindingFlags.Public);

        public MethodInfo ConverterInfo { get { return ConverterMethod; } }

        public static double? Convert(string value)
        {
            if (string.IsNullOrWhiteSpace(value)) return null;

            double val;
            if (double.TryParse(value, out val))
            {
                return val;
            }
            return null;
        }
    }
}