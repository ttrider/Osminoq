using System.Collections.Generic;

namespace TTRider.Osminoq
{
    public interface IDataStreamObjectInfo
    {
        IList<IDataStreamFieldInfo> Fields { get; }
    }
}