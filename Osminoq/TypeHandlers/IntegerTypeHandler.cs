using System.ComponentModel.Composition;
using System.Reflection;

namespace TTRider.Osminoq.TypeHandlers
{
    [TypeHandler(TypeName = "int")]
    [TypeHandler(TypeName = "integer")]
    [TypeHandler(TypeName = "int32")]
    [TypeHandler(TypeName = "System.Int32")]
    [Export(typeof(IDataItemTypeHandler))]
    public class IntegerTypeHandler : IDataItemTypeHandler
    {
        private static readonly MethodInfo ConverterMethod = typeof(IntegerTypeHandler).GetMethod("Convert", BindingFlags.Static | BindingFlags.Public);

        public MethodInfo ConverterInfo { get { return ConverterMethod; } }

        public static int? Convert(string value)
        {
            if (string.IsNullOrWhiteSpace(value)) return null;

            int val;
            if (int.TryParse(value, out val))
            {
                return val;
            }
            return null;
        }
    }
}