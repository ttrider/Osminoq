using System.Collections.Generic;

namespace TTRider.Osminoq
{
    public interface IDataStreamObjectInfo
    {
        IList<IDataItemProperty> Fields { get; }
    }
}