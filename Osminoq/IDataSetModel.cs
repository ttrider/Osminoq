namespace TTRider.Osminoq
{
    public interface IDataSetModel : IDataStreamObjectInfo
    {
        string Source { get; }
    }
}