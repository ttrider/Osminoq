using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using TTRider.Osminoq.TypeHandlers;

namespace TTRider.Osminoq
{
    public static class DataItemFactory
    {
        private const string DynamicAssemblyName = "TTRider.Osminoq.Dynamic";

        private static readonly ModuleBuilder moduleBuilder;
        private static readonly Dictionary<string, MethodInfo> typeHandlers;

        static DataItemFactory()
        {
            // dynamic types support
            var assemblyBuilder = AppDomain.CurrentDomain.DefineDynamicAssembly(
                new AssemblyName(DynamicAssemblyName), AssemblyBuilderAccess.RunAndSave);
            moduleBuilder = assemblyBuilder.DefineDynamicModule(DynamicAssemblyName);

            // MEF
            var catalog = new AggregateCatalog();
            catalog.Catalogs.Add(new AssemblyCatalog(Assembly.GetExecutingAssembly()));
            catalog.Catalogs.Add(new ApplicationCatalog());
            var compositionContainer = new CompositionContainer(catalog);

            // externals
            var externals = new Externals();
            compositionContainer.SatisfyImportsOnce(externals);

            // type handlers
            typeHandlers = new Dictionary<string, MethodInfo>(StringComparer.OrdinalIgnoreCase);
            if (externals.Handlers != null)
            {
                foreach (var handler in externals.Handlers)
                {
                    var handlerType = handler.GetType();
                    foreach (var attr in handlerType.GetCustomAttributes<TypeHandlerAttribute>())
                    {
                        typeHandlers[attr.TypeName] = handler.ConverterInfo;
                    }
                }
            }
        }



        private static int TypeIndex = 0;

        


       

        public static ConstructorInfo GetRegexCtor()
        {
            return typeof(Regex).GetConstructor(new Type[] { typeof(string), typeof(RegexOptions) });
        }

        public static MethodInfo GetTypeHandler(string dataType)
        {
            MethodInfo handler;
            if (!typeHandlers.TryGetValue(dataType, out handler))
            {
                throw new InvalidDataException("Unknown Data type:" + dataType);
            }
            return handler;
        }

        public static TypeBuilder GetTypeBuilder(string name, Type baseType)
        {
            var module = moduleBuilder;

            var sb = new StringBuilder("Dynamic_");
            if (!string.IsNullOrWhiteSpace(name))
            {
                sb.Append(CleanupPropertyName(name));
                sb.Append("_");
            }
            sb.Append(Interlocked.Increment(ref TypeIndex));
            sb.Append("_DataItem");

            return module.DefineType(sb.ToString(), TypeAttributes.Class | TypeAttributes.Public, baseType);
        }


        public static string CleanupPropertyName(string name)
        {
            return name;
        }



        

        public static readonly MethodInfo ProcessPatternMethod =
            typeof(DataItemUtilities).GetMethod("ProcessPattern", BindingFlags.Public | BindingFlags.Static);






        private class Externals
        {
            [ImportMany(typeof (IDataItemTypeHandler))]
            #pragma warning disable 649
            public List<IDataItemTypeHandler> Handlers;
            #pragma warning restore 649
        }
    }
}
