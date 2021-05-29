using System;

namespace DataLoggerParams
{
    public static class Factory
    {
        public static ISixteenBitSignal CreateSixteenBitSignal(string m_Name, string m_LSB, string m_MSB, string m_msgID, int m_scale, int mOutlo, int mOutHi)
        {
            return new SixteenBitSignal(m_Name, m_LSB, m_MSB, m_msgID, m_scale, mOutlo, mOutHi);
        }

        public static IEightBitSignal CreateEightBitSignal(string m_sName, string m_sCanFrame, string m_msgID, int m_scale, int mOutlo, int mOutHi)
        {
            return new EightBitSignal(m_sName, m_sCanFrame, m_msgID, m_scale, mOutlo, mOutHi);
        }

        public static IBoolSignal CreateBoolSignal(string m_Name, string m_canFrame, string m_msgID, int m_bitNo)
        {
            return new BoolSignal(m_Name, m_canFrame, m_msgID, m_bitNo);
        }

        public static ISignals CreateSignals(string sfName)
        {
            return new Signals(sfName);
        }

        
    }
}
