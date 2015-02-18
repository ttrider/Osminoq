using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTRider.Osminoq.Settings
{
    public class ExtractorPartition : IExtractorPartition
    {
        public ExtractorPartition()
        {
            this.Fields = new List<IDataStreamFieldInfo>();
        }


        public IList<IDataStreamFieldInfo> Fields { get; private set; }
        public string Id { get; set; }
        public int? Index { get; set; }
    }
}
