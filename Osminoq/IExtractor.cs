﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TTRider.Osminoq
{
    public interface IExtractor : IDisposable
    {
        //Task<IDataItem> ExtractDataItemAsync();
        //Task<IDataItem> ExtractDataItemAsync(CancellationToken cancellationToken);

        IDataItem ExtractDataItem();


    }
}
