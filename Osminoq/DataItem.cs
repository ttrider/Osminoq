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
        private double? _v2;
        private string _v3;

        static TestTabularTextDataItem()
        {
            __patterns__ = new Regex[3];
            __patterns__[0] = new Regex("pattern1", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            __patterns__[1] = new Regex("pattern3", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        }

        public string v1
        {
            get { return this._v1; }
        }
        public double? v2
        {
            get { return this._v2; }
        }
        public string v3
        {
            get { return this._v3; }
        }


        public object GetValue(string property)
        {
            switch (property)
            {
                case "v1":
                    return this._v1;
                case "v2":
                    return this._v2;
                case "v3":
                    return this._v3;
            }
            return null;
        }

        protected override void Initialize(string[] values)
        {
            _v1 = StringTypeHandler.Convert(DataItemUtilities.ProcessPattern(values[0], __patterns__[0]));
            _v2 = DoubleTypeHandler.Convert(values[1]);
            _v3 = StringTypeHandler.Convert(DataItemUtilities.ProcessPattern(values[2], __patterns__[1]));
        }
    }


}

