using System.Text.RegularExpressions;

namespace TTRider.Osminoq
{
    public interface IDataItemProperty : IDataStreamObjectInfo
    {
        string Name { get; }
        string Source { get; }

        string Template { get; }

        string DataType { get; }

        bool IsArray { get; }
    }
}