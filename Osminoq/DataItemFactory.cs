using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TTRider.Osminoq.TypeHandlers;

namespace TTRider.Osminoq
{
    public static class DataItemFactory
    {
        private static volatile int TypeIndex = 0;

        private static DataItemTypeHandlers TypeHandlers = new DataItemTypeHandlers();

        static ConcurrentDictionary<IExtractorPartition, Type> partitionDatItemTypes = new ConcurrentDictionary<IExtractorPartition, Type>(PartitionComparer.Default); 

        static Lazy<ModuleBuilder> moduleBuilder = new Lazy<ModuleBuilder>(() =>
        {
            var dynamicAssembly = AppDomain.CurrentDomain.DefineDynamicAssembly(
                new AssemblyName("TTRider.Osminoq.Dynamic"), AssemblyBuilderAccess.Run);
            return dynamicAssembly.DefineDynamicModule("TTRider.Osminoq.Dynamic");
        }, LazyThreadSafetyMode.ExecutionAndPublication);

        static DataItemFactory()
        {
            TypeHandlers.Add(new StringTypeHandler());
            TypeHandlers.Add(new IntegerTypeHandler());
            TypeHandlers.Add(new DoubleTypeHandler());
            TypeHandlers.Add(new BooleanTypeHandler());
            TypeHandlers.Add(new DateTimeTypeHandler());
            TypeHandlers.Add(new GuidTypeHandler());
            
        }

        
        
        
        //DateTime,
        //Guid


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

                foreach (var field in partition.Fields)
                {
                    var name = field.Name; 
                    //TODO: cleanup name

                    var typeHandler = field.DataType;



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
