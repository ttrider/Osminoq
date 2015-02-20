using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTRider.Osminoq.Settings
{
    public class DataPackageModel : IDataPackageModel
    {
        public DataPackageModel()
        {
            this.Encoding = Encoding.UTF8;
            this.BufferSize = 1024*1024;
            this.DataErrorStrategy = DataErrorStrategy.IgnoreDataErrors;
            this.Delimeter = ",";
            this.HasHeaderRecord = true;

            this.DataSetModels = new List<IDataSetModel>();
        }


        public Encoding Encoding { get; set; }
        public int BufferSize { get; set; }
        public string Delimeter { get; set; }
        public bool HasHeaderRecord { get; set; }
        public DataErrorStrategy DataErrorStrategy { get; private set; }


        public IList<IDataSetModel> DataSetModels { get; private set; } 
    }
}
