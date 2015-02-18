using System;
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

        public string Id { get { return "string"; } }
        
        public Type TargetType { get { return typeof (string); } }

        public MethodInfo ConverterInfo { get { return ConverterMethod; } }


        public static string Convert(string value)
        {
            return value;
        }
    }

    [TypeHandler(TypeName = "int")]
    [TypeHandler(TypeName = "integer")]
    [TypeHandler(TypeName = "int32")]
    [TypeHandler(TypeName = "System.Int32")]
    [Export(typeof(IDataItemTypeHandler))]
    public class IntegerTypeHandler : IDataItemTypeHandler
    {
        private static readonly MethodInfo ConverterMethod = typeof(StringTypeHandler).GetMethod("Convert", BindingFlags.Static | BindingFlags.Public);

        public string Id { get { return "integer"; } }

        public Type TargetType { get { return typeof(int?); } }

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

    [TypeHandler(TypeName = "number")]
    [TypeHandler(TypeName = "float")]
    [TypeHandler(TypeName = "double")]
    [TypeHandler(TypeName = "System.Double")]
    [TypeHandler(TypeName = "System.Float")]
    [Export(typeof(IDataItemTypeHandler))]
    public class DoubleTypeHandler : IDataItemTypeHandler
    {
        private static readonly MethodInfo ConverterMethod = typeof(StringTypeHandler).GetMethod("Convert", BindingFlags.Static | BindingFlags.Public);

        public string Id { get { return "double"; } }

        public Type TargetType { get { return typeof(double?); } }

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

    [TypeHandler(TypeName = "bit")]
    [TypeHandler(TypeName = "bool")]
    [TypeHandler(TypeName = "boolean")]
    [TypeHandler(TypeName = "System.Boolean")]
    [Export(typeof(IDataItemTypeHandler))]
    public class BooleanTypeHandler : IDataItemTypeHandler
    {
        private static readonly MethodInfo ConverterMethod = typeof(StringTypeHandler).GetMethod("Convert", BindingFlags.Static | BindingFlags.Public);

        public string Id { get { return "boolean"; } }

        public Type TargetType { get { return typeof(bool?); } }

        public MethodInfo ConverterInfo { get { return ConverterMethod; } }

        public static bool? Convert(string value)
        {
            if (string.IsNullOrWhiteSpace(value)) return null;

            bool val;
            if (bool.TryParse(value, out val))
            {
                return val;
            }
            return null;
        }
    }

    [TypeHandler(TypeName = "date")]
    [TypeHandler(TypeName = "time")]
    [TypeHandler(TypeName = "datetime")]
    [TypeHandler(TypeName = "System.DateTime")]
    [Export(typeof(IDataItemTypeHandler))]
    public class DateTimeTypeHandler : IDataItemTypeHandler
    {
        private static readonly MethodInfo ConverterMethod = typeof(StringTypeHandler).GetMethod("Convert", BindingFlags.Static | BindingFlags.Public);

        public string Id { get { return "datetime"; } }

        public Type TargetType { get { return typeof(DateTime?); } }

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

    [TypeHandler(TypeName = "guid")]
    [TypeHandler(TypeName = "uuid")]
    [TypeHandler(TypeName = "uniqueidentifier")]
    [TypeHandler(TypeName = "System.Guid")]
    [Export(typeof(IDataItemTypeHandler))]
    public class GuidTypeHandler : IDataItemTypeHandler
    {
        private static readonly MethodInfo ConverterMethod = typeof(StringTypeHandler).GetMethod("Convert", BindingFlags.Static | BindingFlags.Public);

        public string Id { get { return "guid"; } }

        public Type TargetType { get { return typeof(Guid?); } }

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