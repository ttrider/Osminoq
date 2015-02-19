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
        private static int TypeIndex = 0;

        static ConcurrentDictionary<IExtractorPartition, Type> partitionDatItemTypes = new ConcurrentDictionary<IExtractorPartition, Type>(PartitionComparer.Default);

        internal static Lazy<TypeHandlerFactory> typeHandlerFactory = new Lazy<TypeHandlerFactory>(() =>
        {
            var thf = new TypeHandlerFactory();
            CompositionContainer.Value.SatisfyImportsOnce(thf);
            return thf;
        }, LazyThreadSafetyMode.ExecutionAndPublication);

        public static Lazy<AssemblyBuilder> assBuilder = new Lazy<AssemblyBuilder>(() =>
        {
            var dynamicAssembly = AppDomain.CurrentDomain.DefineDynamicAssembly(
                new AssemblyName("TTRider.Osminoq.Dynamic"), AssemblyBuilderAccess.RunAndSave);
            return dynamicAssembly;

        }, LazyThreadSafetyMode.ExecutionAndPublication);

        public static Lazy<ModuleBuilder> moduleBuilder = new Lazy<ModuleBuilder>(() =>
        {
            return assBuilder.Value.DefineDynamicModule("TTRider.Osminoq.Dynamic");
            
        }, LazyThreadSafetyMode.ExecutionAndPublication);

        public static Lazy<CompositionContainer> CompositionContainer = new Lazy<CompositionContainer>(() =>
        {
            var catalog = new AggregateCatalog();
            catalog.Catalogs.Add(new AssemblyCatalog(Assembly.GetExecutingAssembly()));
            catalog.Catalogs.Add(new ApplicationCatalog());
            return new CompositionContainer(catalog);
        }, LazyThreadSafetyMode.ExecutionAndPublication);



        static DataItemFactory()
        {
        }


        public static ConstructorInfo GetRegexCtor()
        {
            return typeof(Regex).GetConstructor(new Type[] { typeof(string), typeof(RegexOptions) });
        }

        public static MethodInfo GetTypeHandler(string dataType)
        {
            MethodInfo handler;
            if (!typeHandlerFactory.Value.TryGetTypeHandler(dataType, out handler))
            {
                throw new InvalidDataException("Unknown Data type:" + dataType);
            }
            return handler;
        }

        public static TypeBuilder GetTypeBuilder(string name, Type baseType)
        {
            var module = moduleBuilder.Value;

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

        public static Type GetDataItemType(IExtractorPartition currentPartition)
        {
            if (currentPartition == null) throw new ArgumentNullException("currentPartition");

            return partitionDatItemTypes.GetOrAdd(currentPartition, (partition =>
            {
                var module = moduleBuilder.Value;

                var sb = new StringBuilder("Dynamic_");
                if (!string.IsNullOrWhiteSpace(partition.Id))
                {
                    sb.Append(partition.Id);
                    sb.Append("_");
                }
                sb.Append(Interlocked.Increment(ref TypeIndex));
                sb.Append("_DataItem");

                var typeBuilder = module.DefineType(sb.ToString(), TypeAttributes.Class | TypeAttributes.Public, typeof(DataItem));

                var initializeMethod = typeBuilder.DefineMethod("Initialize", MethodAttributes.Family | MethodAttributes.Virtual, null, new Type[] { typeof(string[]) });
                var init = initializeMethod.GetILGenerator();
                int index = 0;
                foreach (var field in partition.Fields)
                {
                    var name = field.Name;
                    //TODO: cleanup name

                    MethodInfo handler;
                    if (!typeHandlerFactory.Value.TryGetTypeHandler(field.DataType, out handler))
                    {
                        throw new InvalidDataException("Unknown Data type:" + field.DataType);
                    }

                    var fieldDef = typeBuilder.DefineField("_" + name, handler.ReturnType, FieldAttributes.Public);


                    init.Emit(OpCodes.Ldarg_0);
                    init.Emit(OpCodes.Ldarg_1);
                    init.Emit(OpCodes.Ldc_I4_S, index++);
                    init.Emit(OpCodes.Ldelem_Ref);
                    init.Emit(OpCodes.Call, handler);
                    init.Emit(OpCodes.Stfld, fieldDef);

                }




                init.Emit(OpCodes.Ret);
                return typeBuilder.CreateType();
            }));



            //var nfl0 = tb.DefineField("Name", typeof(string), FieldAttributes.Public);
            //var nfl1 = tb.DefineField("PositionTitle", typeof(string), FieldAttributes.Public);
            //var nfl2 = tb.DefineField("Department", typeof(string), FieldAttributes.Public);
            //var nfl3 = tb.DefineField("EmployeeAnnualSalary", typeof(double), FieldAttributes.Public);

            //var rdm = tb.DefineMethod("Read", MethodAttributes.Public | MethodAttributes.Virtual, null, new Type[] { typeof(string[]) });
            //ILGenerator ILout = rdm.GetILGenerator();

            //ILout.Emit(OpCodes.Ldarg_0);
            //ILout.Emit(OpCodes.Ldarg_1);
            //ILout.Emit(OpCodes.Ldc_I4_0);
            //ILout.Emit(OpCodes.Ldelem_Ref);
            //ILout.Emit(OpCodes.Call, ps);
            //ILout.Emit(OpCodes.Stfld, nfl0);

            //ILout.Emit(OpCodes.Ldarg_0);
            //ILout.Emit(OpCodes.Ldarg_1);
            //ILout.Emit(OpCodes.Ldc_I4_1);
            //ILout.Emit(OpCodes.Ldelem_Ref);
            //ILout.Emit(OpCodes.Call, ps);
            //ILout.Emit(OpCodes.Stfld, nfl1);

            //ILout.Emit(OpCodes.Ldarg_0);
            //ILout.Emit(OpCodes.Ldarg_1);
            //ILout.Emit(OpCodes.Ldc_I4_2);
            //ILout.Emit(OpCodes.Ldelem_Ref);
            //ILout.Emit(OpCodes.Call, ps);
            //ILout.Emit(OpCodes.Stfld, nfl2);


            //ILout.Emit(OpCodes.Ldarg_0);
            //ILout.Emit(OpCodes.Ldarg_1);
            //ILout.Emit(OpCodes.Ldc_I4_S, 3);
            //ILout.Emit(OpCodes.Ldelem_Ref);
            //ILout.Emit(OpCodes.Call, pm);
            //ILout.Emit(OpCodes.Stfld, nfl3);

            //ILout.Emit(OpCodes.Ret);
        }

        public static string CleanupPropertyName(string name)
        {
            return name;
        }



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

        public static readonly MethodInfo ProcessPatternMethod =
            typeof (DataItemFactory).GetMethod("ProcessPattern", BindingFlags.Public|BindingFlags.Static);
    }

    class PartitionComparer : IEqualityComparer<IExtractorPartition>
    {
        public readonly static PartitionComparer Default = new PartitionComparer();


        public bool Equals(IExtractorPartition x, IExtractorPartition y)
        {
            if ((x == null) && (y == null)) return true;
            if (x == null) return false;
            if (y == null) return false;

            if (x.Fields.Count != y.Fields.Count) return false;

            //TODO: correct compare

            return false;
        }

        public int GetHashCode(IExtractorPartition obj)
        {
            if (obj == null) return 0;
            return obj.Fields.Count;
        }
    }

}
