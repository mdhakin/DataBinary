namespace DataLoggerParams
{


    public class EightBitSignal : IEightBitSignal
    {
        public string CanFrame { get => m_canframe; }
        private string m_canframe;
        public string msgID { get; set; }
        public int scale { get; set; }
        public int outOfRangelo { get; set; }
        public int outofRangehi { get; set; }
        public string Name { get => this.m_name; }

        private string m_name;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="m_sName">Parameter Name</param>
        /// <param name="m_sCanFrame">Can Frame</param>
        /// <param name="m_msgID">Message ID</param>
        /// <param name="m_scale">Max Scale</param>
        /// <param name="mOutlo">Low</param>
        /// <param name="mOutHi">High</param>
        public EightBitSignal(string m_sName, string m_sCanFrame, string m_msgID, int m_scale, int mOutlo, int mOutHi)
        {
            m_name = m_sName;
            m_canframe = m_sCanFrame;
            msgID = m_msgID;
            scale = m_scale;
            this.outOfRangelo = mOutlo;
            this.outofRangehi = mOutHi;
        }

    }
}
