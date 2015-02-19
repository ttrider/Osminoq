using System.Text.RegularExpressions;

namespace TTRider.Osminoq
{
    public interface IDataItemProperty : IDataStreamObjectInfo
    {
        string Name { get; }
        string Source { get; }

        Regex Template { get; }
        
        Regex Validation { get; }

        string DataType { get; }

        bool IsArray { get; }
    }
}