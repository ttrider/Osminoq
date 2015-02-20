using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text.RegularExpressions;
using TTRider.Osminoq.TypeHandlers;

namespace TTRider.Osminoq
{
    internal class TestTabularTextDataItem : DataItem, ITabularTextInitalizableDataItem
    {
        protected static Regex[] __patterns__;

        protected static KeyValuePair<string, PropertyInfo>[] __properties__;

        private string _v1;
        private double? _v2;
        private string _v3;

        static TestTabularTextDataItem()
        {
            __patterns__ = new Regex[3];
            __patterns__[0] = new Regex("pattern1", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            __patterns__[1] = new Regex("pattern3", RegexOptions.Compiled | RegexOptions.IgnoreCase);


            var tp = typeof (TestTabularTextDataItem);
            __properties__  = new KeyValuePair<string, PropertyInfo>[3];
            __properties__[0] = new KeyValuePair<string, PropertyInfo>("v1", tp.GetProperty("v1"));
            __properties__[1] = new KeyValuePair<string, PropertyInfo>("v2", tp.GetProperty("v2"));
            __properties__[2] = new KeyValuePair<string, PropertyInfo>("v3", tp.GetProperty("v3"));
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


        void ITabularTextInitalizableDataItem.Initialize(string[] values)
        {
            _v1 = StringTypeHandler.Convert(DataItemUtilities.ProcessPattern(values[0], __patterns__[0]));
            _v2 = DoubleTypeHandler.Convert(values[1]);
            _v3 = StringTypeHandler.Convert(DataItemUtilities.ProcessPattern(values[2], __patterns__[1]));
        }
    }
}