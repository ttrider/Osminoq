using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTRider.Osminoq
{
    public class DataItemTypeHandlers : System.Collections.ObjectModel.KeyedCollection<string,IDataItemTypeHandler>
    {
        public DataItemTypeHandlers()
            :base(StringComparer.OrdinalIgnoreCase)
        {
            
        }

        protected override string GetKeyForItem(IDataItemTypeHandler item)
        {
            if (item == null) throw new ArgumentNullException("item");

            return item.Id;
        }
    }
}
