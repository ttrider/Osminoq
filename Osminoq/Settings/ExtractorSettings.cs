using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTRider.Osminoq.Settings
{
    public class ExtractorSettings : IExtractorSettings
    {
        public ExtractorSettings()
        {
            this.Encoding = Encoding.UTF8;
            this.BufferSize = 1024*1024;
            this.DataErrorStrategy = DataErrorStrategy.IgnoreDataErrors;
            this.Delimeter = ",";
            this.HasHeaderRecord = true;

            this.Partitions = new List<IExtractorPartition>();
        }


        public Encoding Encoding { get; set; }
        public int BufferSize { get; set; }
        public string Delimeter { get; set; }
        public bool HasHeaderRecord { get; set; }
        public DataErrorStrategy DataErrorStrategy { get; private set; }


        public IList<IExtractorPartition> Partitions { get; private set; } 
    }
}
