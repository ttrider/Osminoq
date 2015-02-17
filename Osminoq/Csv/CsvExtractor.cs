using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;
using CsvHelper.Configuration;

namespace TTRider.Osminoq.Csv
{
    public class CsvExtractor : Extractor
    {
        private CsvReader reader;

        public CsvExtractor(Stream stream, IExtractorSettings settings)
        {
            if (stream == null) throw new ArgumentNullException("stream");
            if (settings == null) throw new ArgumentNullException("settings");

           // CsvConfiguration

            //reader = new CsvReader();

        }

        public override IDataItem ExtractDataItem()
        {
            throw new NotImplementedException();
        }
    }
}



/*using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;
using CsvHelper.Configuration;

namespace etlcsv
{

    public class ItemBase
    {
        public void Read(ICsvReaderRow row)
        {
            dynamic item = this;

            item.Name = row.GetField<string>(0);
            item.PositionTitle = row.GetField<string>(1);
            item.Department = row.GetField<string>(2);
            item.EmployeeAnnualSalary = double.Parse(row.GetField<string>(3).Substring(1));
        }

        public virtual void Read(string[] row)
        {
            
        }


        public static string ParseString(string value)
        {
            return value;
        }
        public static double ParseMoney(string value)
        {
            return double.Parse(value.Substring(1));
        }

    }



    class Item : ItemBase
    {
        public string Name;
        public string PositionTitle;
        public string Department;
        public double EmployeeAnnualSalary;

        public override void Read(string[] row)
        {
            this.Name = ParseString(row[0]);
            this.PositionTitle = ParseString(row[1]);
            this.Department = ParseString(row[2]);
            this.EmployeeAnnualSalary = ParseMoney(row[3]);
        }

    }


    class Program
    {
        static void Main(string[] args)
        {

            var ps = typeof (ItemBase).GetMethod("ParseString", BindingFlags.Public | BindingFlags.Static);
            var pm = typeof (ItemBase).GetMethod("ParseMoney", BindingFlags.Public | BindingFlags.Static);

            var ab = AppDomain.CurrentDomain.DefineDynamicAssembly(new AssemblyName("etl"), AssemblyBuilderAccess.Run);
            var mb = ab.DefineDynamicModule("EtlModule");
            var tb = mb.DefineType("ChicagoEmployee", TypeAttributes.Class | TypeAttributes.Public, typeof(ItemBase));
            var nfl0 = tb.DefineField("Name", typeof(string), FieldAttributes.Public);
            var nfl1 = tb.DefineField("PositionTitle", typeof(string), FieldAttributes.Public);
            var nfl2 = tb.DefineField("Department", typeof(string), FieldAttributes.Public);
            var nfl3 = tb.DefineField("EmployeeAnnualSalary", typeof(double), FieldAttributes.Public);

            var rdm = tb.DefineMethod("Read", MethodAttributes.Public|MethodAttributes.Virtual, null, new Type[] {typeof (string[])});
            ILGenerator ILout = rdm.GetILGenerator();

            ILout.Emit(OpCodes.Ldarg_0);
            ILout.Emit(OpCodes.Ldarg_1);
            ILout.Emit(OpCodes.Ldc_I4_0);
            ILout.Emit(OpCodes.Ldelem_Ref);
            ILout.Emit(OpCodes.Call, ps);
            ILout.Emit(OpCodes.Stfld, nfl0);

            ILout.Emit(OpCodes.Ldarg_0);
            ILout.Emit(OpCodes.Ldarg_1);
            ILout.Emit(OpCodes.Ldc_I4_1);
            ILout.Emit(OpCodes.Ldelem_Ref);
            ILout.Emit(OpCodes.Call, ps);
            ILout.Emit(OpCodes.Stfld, nfl1);

            ILout.Emit(OpCodes.Ldarg_0);
            ILout.Emit(OpCodes.Ldarg_1);
            ILout.Emit(OpCodes.Ldc_I4_2);
            ILout.Emit(OpCodes.Ldelem_Ref);
            ILout.Emit(OpCodes.Call, ps);
            ILout.Emit(OpCodes.Stfld, nfl2);
            

            ILout.Emit(OpCodes.Ldarg_0);
            ILout.Emit(OpCodes.Ldarg_1);
            ILout.Emit(OpCodes.Ldc_I4_S,3);
            ILout.Emit(OpCodes.Ldelem_Ref);
            ILout.Emit(OpCodes.Call, pm);
            ILout.Emit(OpCodes.Stfld, nfl3);

            ILout.Emit(OpCodes.Ret);
            //var fb = tb.DefineInitializedData("Mapp", new byte[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 }, System.Reflection.FieldAttributes.Public);

            //var prop = tb.DefineProperty("Name", PropertyAttributes.None, typeof (string), new Type[0]);
            //prop.GetSetMethod()


            var t = tb.CreateType();


            //var path = @"C:\src\Firefly\Models\Healthcare\ACHealthcare\Data\PhoneType.csv";
            var path =
                @"\\vmware-host\Shared Folders\Downloads\Current_Employee_Names__Salaries__and_Position_Titles.csv";

            var f = new CsvFactory();
            var c = new CsvConfiguration();

            c.AutoMap<Item>();
            c.Delimiter = ",";
            c.HasHeaderRecord = true;
            c.IgnoreBlankLines = true;
            c.DetectColumnCountChanges = true;

            

            using (var fl = File.OpenText(path))
            {
                var reader = f.CreateReader(fl, c);

                var rr = new CsvReader();

                while (reader.Read())
                {
                    var item = Activator.CreateInstance(typeof(Item)) as Item;
                    item.Read(reader.CurrentRecord);


                    var item2 = Activator.CreateInstance(t) as ItemBase;
                    //item2.Read(reader);

                    item2.Read(reader.CurrentRecord);
                }
            }
        }
    }
}
*/