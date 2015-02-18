namespace TTRider.Osminoq
{
    public interface IExtractorPartition : IDataStreamObjectInfo
    {
        string Id { get; }

        int? Index { get; }
    }
}