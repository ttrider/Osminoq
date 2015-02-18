using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TTRider.Osminoq
{
    public class DataItemProperty : IDataStreamFieldInfo
    {
        public DataItemProperty()
        {
            this.Fields = new List<IDataStreamFieldInfo>();
        }

        public IList<IDataStreamFieldInfo> Fields { get; private set; }
        public string Name { get; set; }
        public Regex Template { get; set; }
        public Regex Validation { get; set; }
        public string DataType { get; set; }
        public bool IsArray { get; set; }
        public string Input { get; set; }
    }
}
