using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
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
        private static readonly MethodInfo ProcessPatternInfo =
            typeof (TabularTextRecordsetDefiniton).GetMethod("ProcessPattern", BindingFlags.Static | BindingFlags.Public);



        private IExtractorPartition partition;
        private string[] buffer;

        public TabularTextRecordsetDefiniton(IExtractorPartition partition, int fieldCount)
        {

            foreach (var field in partition.Fields)
            {
                var name = DataItemFactory.CleanupPropertyName(field.Name);
                var dataHandler = DataItemFactory.GetTypeHandler(field.DataType);
                
                // init 
                //protected static Regex[] patterns;
                //protected static Regex[] validators;
                var dataTemplate = field.Template;
                var dataValidator = field.Validation;

            }


            
            this.partition = partition;
        }

        public TabularTextRecordsetDefiniton(IExtractorPartition partition, IEnumerable<string> fieldNames)
        {
            this.partition = partition;
            this.buffer = buffer;
        }

        internal IDataItem CreateDataItem(string[] buffer)
        {
            return null;
        }

        // represents string[] format of the record

        public List<TabularTextRecordsetField> Fields { get; set; }


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
    }





    public class TabularTextRecordsetField
    {
        
    }
}
