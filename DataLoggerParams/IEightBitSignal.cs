namespace DataLoggerParams
{
    public interface IEightBitSignal
    {
        string CanFrame { get; }
        string msgID { get; set; }
        string Name { get; }
        int outofRangehi { get; set; }
        int outOfRangelo { get; set; }
        int scale { get; set; }
    }
}