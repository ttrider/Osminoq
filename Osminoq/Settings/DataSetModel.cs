using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTRider.Osminoq.Settings
{
    public class DataSetModel : IDataSetModel
    {
        public DataSetModel()
        {
            this.Properties = new List<IDataItemProperty>();
        }

        public string Source { get; set; }

        public IList<IDataItemProperty> Properties { get; private set; }

    }
}
