using System.Text.RegularExpressions;

namespace TTRider.Osminoq
{
    public interface IDataStreamFieldInfo : IDataStreamObjectInfo
    {
        string Name { get; }

        Regex Template { get; }
        
        Regex Validation { get; }

        string DataType { get; }

        bool IsArray { get; }

        string Input { get; }

    }
}