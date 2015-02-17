using System.Text.RegularExpressions;

namespace TTRider.Osminoq
{
    public interface IDataStreamFieldInfo : IDataStreamObjectInfo
    {
        string Id { get; }

        int Index { get; }

        string Name { get; }

        Regex Template { get; }
        
        Regex Validation { get; }

        DataType? DataType { get; }

        bool IsArray { get; }

    }
}