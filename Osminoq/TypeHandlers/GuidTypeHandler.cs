using System;
using System.ComponentModel.Composition;
using System.Reflection;

namespace TTRider.Osminoq.TypeHandlers
{
    [TypeHandler(TypeName = "guid")]
    [TypeHandler(TypeName = "uuid")]
    [TypeHandler(TypeName = "uniqueidentifier")]
    [TypeHandler(TypeName = "System.Guid")]
    [Export(typeof(IDataItemTypeHandler))]
    public class GuidTypeHandler : IDataItemTypeHandler
    {
        private static readonly MethodInfo ConverterMethod = typeof(GuidTypeHandler).GetMethod("Convert", BindingFlags.Static | BindingFlags.Public);

        public MethodInfo ConverterInfo { get { return ConverterMethod; } }

        public static Guid? Convert(string value)
        {
            if (string.IsNullOrWhiteSpace(value)) return null;

            Guid val;
            if (Guid.TryParse(value, out val))
            {
                return val;
            }
            return null;
        }
    }
}