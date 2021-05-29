namespace DataLoggerParams
{
    public interface IBoolSignal
    {
        int bitNo { get; set; }
        string canFrame { get; set; }
        string msgID { get; set; }
        string Name { get; set; }
    }
}