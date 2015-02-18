using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTRider.Osminoq
{
    public abstract class TextExtractor :  Extractor
    {
        protected TextExtractor(IExtractorSettings settings)
            :base(settings)
        {
        }


        protected abstract string[] ExtractRecord();

        public override IDataItem ExtractDataItem()
        {
            var buffer = this.ExtractRecord();


            // translate it

            return null;
        }

    }
}
