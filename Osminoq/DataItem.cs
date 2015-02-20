using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TTRider.Osminoq
{
    public class DataItem : IDataItem, ITabularTextInitalizableDataItem
    {
        void ITabularTextInitalizableDataItem.Initialize(string[] values)
        {
            this.Initialize(values);
        }

        protected virtual void Initialize(string[] values)
        {
        }





        protected virtual KeyValuePair<string, PropertyInfo>[] GetPropertyMap()
        {
            return new KeyValuePair<string, PropertyInfo>[0];
        }

    }
}

