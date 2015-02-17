using System.Collections.Generic;

namespace TTRider.Osminoq
{
    public interface IDataStreamObjectInfo
    {
        IEnumerable<IDataStreamFieldInfo> Fields { get; }
    }
}