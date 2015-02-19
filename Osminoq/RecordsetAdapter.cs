using System;
using System.IO;

namespace TTRider.Osminoq
{
    public class RecordsetAdapter
    {
        private readonly Type dataItemType;

        protected RecordsetAdapter(Type dataItemType)
        {
            if (dataItemType == null) throw new ArgumentNullException("dataItemType");

            if (dataItemType.GetInterface("TTRider.Osminoq.IDataItem") == null)
            {
                throw new InvalidDataException("Provided type doesn't implement IDataItem interface");
            }
            
            this.dataItemType = dataItemType;
        }

        protected T CreateDataItem<T>()
        {
            return (T)Activator.CreateInstance(this.dataItemType);
        }

    }
}