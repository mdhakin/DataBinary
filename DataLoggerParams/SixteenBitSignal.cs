namespace DataLoggerParams
{
    public class SixteenBitSignal : ISixteenBitSignal
    {
        public string LSB { get; set; }
        public string MSB { get; set; }
        public string msgID { get; set; }
        public int Scale { get; set; }
        public int outOfRangelo { get; set; }
        public int outofRangehi { get; set; }
        public string Name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="m_Name">Param Name</param>
        /// <param name="m_LSB">Least Significat Byte</param>
        /// <param name="m_MSB">Most Significat Byte</param>
        /// <param name="m_msgID">Message ID</param>
        /// <param name="m_scale">Max Scale</param>
        /// <param name="mOutlo">Low</param>
        /// <param name="mOutHi">High</param>
        public SixteenBitSignal(string m_Name, string m_LSB, string m_MSB, string m_msgID, int m_scale, int mOutlo, int mOutHi)
        {
            Name = m_Name;
            LSB = m_LSB;
            MSB = m_MSB;
            msgID = m_msgID;
            Scale = m_scale;
            this.outOfRangelo = mOutlo;
            this.outofRangehi = mOutHi;

        }
    }
}
