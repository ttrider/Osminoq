using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TTRider.Osminoq
{
    public class RecordsetDefiniton
    {


    }


    public class TabularTextRecordsetDefiniton : RecordsetDefiniton
    {
        private Type typeItem;

        public TabularTextRecordsetDefiniton(IExtractorPartition partition, int fieldCount)
        {
            var typeBuilder = DataItemFactory.GetTypeBuilder(partition.Id, typeof (DataItem));

            var patternsDef = typeBuilder.DefineField("__patterns__", typeof(Regex[]), FieldAttributes.Static);


            var cctor = typeBuilder.DefineTypeInitializer();
            var initCctor = cctor.GetILGenerator();

            initCctor.Emit(OpCodes.Ldc_I4_S, partition.Fields.Count);
            initCctor.Emit(OpCodes.Newarr, typeof(Regex));
            initCctor.Emit(OpCodes.Stloc_0);

            
            var initializeMethod = typeBuilder.DefineMethod("Initialize", MethodAttributes.Family | MethodAttributes.Virtual, null, new Type[] { typeof(string[]) });
            var init = initializeMethod.GetILGenerator();

            foreach (var field in partition.Fields)
            {
                // source can be either @name or just a number
                // in case of @name - we can't proceed - recordset is without names
                if (string.IsNullOrWhiteSpace(field.Source)) throw new Exception();
                if (field.Source.Trim().StartsWith("@")) throw new Exception();
                var index = int.Parse(field.Source);
                if (index >= fieldCount) throw new Exception();


                if (!string.IsNullOrWhiteSpace(field.Template))
                {
                    initCctor.Emit(OpCodes.Ldloc_0);
                    initCctor.Emit(OpCodes.Ldc_I4_S, index);
                    initCctor.Emit(OpCodes.Ldstr, field.Template);
                    initCctor.Emit(OpCodes.Ldc_I4_S, 9);
                    initCctor.Emit(OpCodes.Newobj, DataItemFactory.GetRegexCtor());
                    initCctor.Emit(OpCodes.Stelem_Ref);
                }


                var name = DataItemFactory.CleanupPropertyName(field.Name);
                var dataHandler = DataItemFactory.GetTypeHandler(field.DataType);

                var fieldDef = typeBuilder.DefineField("_" + name, dataHandler.ReturnType, FieldAttributes.Public);


                init.Emit(OpCodes.Ldarg_0);
                init.Emit(OpCodes.Ldarg_1);
                init.Emit(OpCodes.Ldc_I4_S, index);
                init.Emit(OpCodes.Ldelem_Ref);
                init.Emit(OpCodes.Call, dataHandler);
                init.Emit(OpCodes.Stfld, fieldDef);

            }


            init.Emit(OpCodes.Ret);

            initCctor.Emit(OpCodes.Ldloc_0);
            initCctor.Emit(OpCodes.Stsfld, patternsDef);
  
            initCctor.Emit(OpCodes.Ret);
            this.typeItem = typeBuilder.CreateType();
        }

        public TabularTextRecordsetDefiniton(IExtractorPartition partition, IEnumerable<string> fieldNames)
        {
            var fields = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
            foreach (var fieldName in fieldNames)
            {
                fields[fieldName] = fields.Count;
            }


            var typeBuilder = DataItemFactory.GetTypeBuilder(partition.Id, typeof(DataItem));

            var patternsDef = typeBuilder.DefineField("__patterns__", typeof(Regex[]), FieldAttributes.Static|FieldAttributes.Private);


            var cctor = typeBuilder.DefineTypeInitializer();
            var initCctor = cctor.GetILGenerator();

            initCctor.Emit(OpCodes.Ldc_I4_S, partition.Fields.Count);
            initCctor.Emit(OpCodes.Newarr, typeof(Regex));
            initCctor.Emit(OpCodes.Stsfld, patternsDef);



            var initializeMethod = typeBuilder.DefineMethod("Initialize", MethodAttributes.Family | MethodAttributes.Virtual, null, new Type[] { typeof(string[]) });
            var init = initializeMethod.GetILGenerator();

            var templateIndex = 0;

            foreach (var field in partition.Fields)
            {
                // source can be either @name or just a number
                // in case of @name - we can't proceed - recordset is without names
                if (string.IsNullOrWhiteSpace(field.Source)) throw new Exception();
                var source = field.Source.Trim();
                var index = source.StartsWith("@") ? fields[source.Substring(1)] : int.Parse(source);
                if (index >= fields.Count) throw new Exception();

                var name = DataItemFactory.CleanupPropertyName(field.Name);
                var dataHandler = DataItemFactory.GetTypeHandler(field.DataType);

                var fieldDef = typeBuilder.DefineField("_" + name, dataHandler.ReturnType, FieldAttributes.Private);

                var propDef = typeBuilder.DefineProperty(name, PropertyAttributes.None, dataHandler.ReturnType, new Type[0]);
                var propMethod = typeBuilder.DefineMethod("get_" + name, MethodAttributes.Public, dataHandler.ReturnType, new Type[0]);
                
                
                var propMethodInit = propMethod.GetILGenerator();
                propMethodInit.DeclareLocal(dataHandler.ReturnType);
                propMethodInit.Emit(OpCodes.Ldarg_0);
                propMethodInit.Emit(OpCodes.Ldfld, fieldDef);
                propMethodInit.Emit(OpCodes.Stloc_0);
                propMethodInit.Emit(OpCodes.Ldloc_0);
                propMethodInit.Emit(OpCodes.Ret);
                propDef.SetGetMethod(propMethod);


                init.Emit(OpCodes.Ldarg_0);
                init.Emit(OpCodes.Ldarg_1);
                init.Emit(OpCodes.Ldc_I4_S, index);
                init.Emit(OpCodes.Ldelem_Ref);

                if (!string.IsNullOrWhiteSpace(field.Template))
                {
                    initCctor.Emit(OpCodes.Ldsfld, patternsDef);
                    initCctor.Emit(OpCodes.Ldc_I4_S, templateIndex);
                    initCctor.Emit(OpCodes.Ldstr, field.Template);
                    initCctor.Emit(OpCodes.Ldc_I4_S, 9);
                    initCctor.Emit(OpCodes.Newobj, DataItemFactory.GetRegexCtor());
                    initCctor.Emit(OpCodes.Stelem_Ref);


                    init.Emit(OpCodes.Ldsfld, patternsDef);
                    init.Emit(OpCodes.Ldc_I4_S, templateIndex);
                    init.Emit(OpCodes.Ldelem_Ref);
                    init.Emit(OpCodes.Call, DataItemFactory.ProcessPatternMethod);

                    templateIndex++;
                }

                init.Emit(OpCodes.Call, dataHandler);
                init.Emit(OpCodes.Stfld, fieldDef);

            }


            init.Emit(OpCodes.Ret);



            initCctor.Emit(OpCodes.Ret);
            this.typeItem = typeBuilder.CreateType();
        }

        internal IDataItem CreateDataItem(string[] buffer)
        {
            var item = (IDataItemInternal)Activator.CreateInstance(this.typeItem);
            item.Initialize(buffer);
            return (IDataItem)item;
        }

        

       
    }

}
