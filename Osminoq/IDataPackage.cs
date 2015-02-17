using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTRider.Osminoq
{
    public interface IDataPackage
    {
        string Id { get; }
    }

    public interface IDataStream
    {
        string Id { get; }

        string Type { get; }

        IEnumerable<IDataStreamPartition> Partitions { get; } 
    }



    public interface IDataStreamObject
    {
        IEnumerable<IDataStreamField> Fields { get; } 
    }


    public interface IDataStreamPartition : IDataStreamObject
    {
        string Id { get; }

        int Index { get; }
    }


    public interface IDataStreamField
    {
        string Id { get; }

        int Index { get; }

        string Name { get; }

        DataType    DataType { get; }

        bool IsArray { get; }


        IEnumerable<IDataStreamField> Fields { get; } 
    }


    public enum DataType
    {
        String,
        Number,
        Boolean,
        DateTime,
    }
}
