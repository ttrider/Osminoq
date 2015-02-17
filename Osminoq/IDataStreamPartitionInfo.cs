namespace TTRider.Osminoq
{
    public interface IDataStreamPartitionInfo : IDataStreamObjectInfo
    {
        string Id { get; }

        int Index { get; }
    }
}