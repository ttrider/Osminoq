using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
}
