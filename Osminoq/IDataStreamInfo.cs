using System.Collections.Generic;

namespace TTRider.Osminoq
{
    public interface IDataStreamInfo
    {
        string Id { get; }

        string Type { get; }

        IEnumerable<IDataStreamPartitionInfo> Partitions { get; }
    }
}