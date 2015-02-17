using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTRider.Osminoq
{
    public abstract class Extractor : IExtractor
    {

        ~Extractor()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            
        }

        public abstract IDataItem ExtractDataItem();
    }
}
