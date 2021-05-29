namespace DataLoggerParams
{
    public interface ISixteenBitSignal
    {
        string LSB { get; set; }
        string MSB { get; set; }
        string msgID { get; set; }
        string Name { get; set; }
        int outofRangehi { get; set; }
        int outOfRangelo { get; set; }
        int Scale { get; set; }
    }
}