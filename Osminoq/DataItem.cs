using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TTRider.Osminoq.TypeHandlers;

namespace TTRider.Osminoq
{
    public class DataItem : IDataItem, IDataItemInternal
    {



        void IDataItemInternal.Initialize(string[] values)
        {
            this.Initialize(values);
        }


        protected virtual void Initialize(string[] values)
        {
        }
    }

    internal class TestTabularTextDataItem : DataItem
    {
        protected static Regex[] __patterns__;


        private string _v1;
        private int? _v2;
        private string _v3;

        static TestTabularTextDataItem()
        {
            __patterns__ = new Regex[3];
            __patterns__[0] = new Regex("pattern1", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            __patterns__[2] = new Regex("pattern3", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        }




        protected override void Initialize(string[] values)
        {
            _v1 = StringTypeHandler.Convert(DataItemFactory.ProcessPattern(values[0], __patterns__[0]));
            _v2 = IntegerTypeHandler.Convert(DataItemFactory.ProcessPattern(values[1], __patterns__[1]));
            _v3 = StringTypeHandler.Convert(DataItemFactory.ProcessPattern(values[2], __patterns__[2]));
        }
    }


}

