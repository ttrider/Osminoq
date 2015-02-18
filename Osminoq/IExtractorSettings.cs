using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTRider.Osminoq
{
    public interface IExtractorSettings
    {
        Encoding Encoding { get; set; }
        int BufferSize { get; set; }
        string Delimeter { get; set; }
        bool HasHeaderRecord { get; set; }

        DataErrorStrategy DataErrorStrategy { get; }
    }
}
