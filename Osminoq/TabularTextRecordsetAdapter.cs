using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using System.Text.RegularExpressions;

namespace TTRider.Osminoq
{
    public class TabularTextRecordsetAdapter : RecordsetAdapter
    {
        public TabularTextRecordsetAdapter(IDataSetModel partition, int fieldCount)
            :base(GenerateDataItemType(partition, new TabularSourceResolver(fieldCount)))
        {
        }

        public TabularTextRecordsetAdapter(IDataSetModel partition, IEnumerable<string> fieldNames)
            :base(GenerateDataItemType(partition, new TabularSourceResolver(fieldNames)))
        {
        }

        private static Type GenerateDataItemType(IDataSetModel partition, TabularSourceResolver sourceResolver)
        {
            var typeBuilder = DataItemFactory.GetTypeBuilder(partition.Source, typeof(DataItem));
            typeBuilder.AddInterfaceImplementation(typeof(ITabularTextInitalizableDataItem));


            var patternsDef = typeBuilder.DefineField("__patterns__", typeof(Regex[]),
                FieldAttributes.Static | FieldAttributes.Private);

            var cctor = typeBuilder.DefineTypeInitializer();
            var initCctor = cctor.GetILGenerator();

            initCctor.Emit(OpCodes.Ldc_I4_S, partition.Properties.Count);
            initCctor.Emit(OpCodes.Newarr, typeof(Regex));
            initCctor.Emit(OpCodes.Stsfld, patternsDef);



            var initializeMethod = typeBuilder.DefineMethod(
                "TTRider.Osminoq.ITabularTextInitalizableDataItem.Initialize", 
                    MethodAttributes.Private |
                    MethodAttributes.Virtual | 
                    MethodAttributes.Final |
                    MethodAttributes.HideBySig|
                    MethodAttributes.NewSlot,
                null,
                new [] { typeof(string[]) });
            typeBuilder.DefineMethodOverride(initializeMethod, typeof(TTRider.Osminoq.ITabularTextInitalizableDataItem).GetMethod("Initialize"));

            var init = initializeMethod.GetILGenerator();

            var templateIndex = 0;

            foreach (var field in partition.Properties)
            {
                var index = sourceResolver.ResolveSource(field.Source);

                var name = DataItemFactory.CleanupPropertyName(field.Name);
                var dataHandler = DataItemFactory.GetTypeHandler(field.DataType);

                var fieldDef = typeBuilder.DefineField("_" + name, dataHandler.ReturnType, FieldAttributes.Private);

                var propDef = typeBuilder.DefineProperty(name, PropertyAttributes.None, dataHandler.ReturnType, new Type[0]);
                var propMethod = typeBuilder.DefineMethod("get_" + name, MethodAttributes.Public, dataHandler.ReturnType,
                    new Type[0]);


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
            return typeBuilder.CreateType();
        }

        public IDataItem CreateDataItem(string[] buffer)
        {
            var item = this.CreateDataItem<ITabularTextInitalizableDataItem>();
            item.Initialize(buffer);
            return (IDataItem)item;
        }
    }
}