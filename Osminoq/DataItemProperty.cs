using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TTRider.Osminoq
{
    public class DataItemProperty : IDataItemProperty
    {
        public DataItemProperty()
        {
            this.Fields = new List<IDataItemProperty>();
        }

        public IList<IDataItemProperty> Fields { get; private set; }
        public string Name { get; set; }
        public string Template { get; set; }
        public string DataType { get; set; }
        public bool IsArray { get; set; }
        public string Source { get; set; }
    }
}
