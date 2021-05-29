using System.Collections.Generic;

namespace DataLoggerParams
{
    public interface ISignals
    {
        List<IBoolSignal> BoolSignals { get; }
        List<IEightBitSignal> EaghtBitSignals { get; }
        string InIFileNme { get; }
        List<ISixteenBitSignal> SixTeenBitSignals { get; }
    }
}