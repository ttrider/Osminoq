using System.ComponentModel.Composition;
using System.Reflection;

namespace TTRider.Osminoq.TypeHandlers
{
    [Export(typeof(IDataItemTypeHandler))]
    [TypeHandler(TypeName = "string")]
    [TypeHandler(TypeName = "System.String")]
    public class StringTypeHandler : IDataItemTypeHandler
    {
        private static readonly MethodInfo ConverterMethod = typeof(StringTypeHandler).GetMethod("Convert", BindingFlags.Static | BindingFlags.Public);

        public MethodInfo ConverterInfo { get { return ConverterMethod; } }


        public static string Convert(string value)
        {
            return value;
        }
    }
}