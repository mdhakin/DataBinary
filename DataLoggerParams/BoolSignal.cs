namespace DataLoggerParams
{
    public class BoolSignal : IBoolSignal
    {
        public string msgID { get; set; }
        public string canFrame { get; set; }
        public int bitNo { get; set; }

        public string Name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="m_Name">Param Name</param>
        /// <param name="m_canFrame">Can Frame</param>
        /// <param name="m_msgID">Message ID</param>
        /// <param name="m_bitNo">Bit No</param>
        public BoolSignal(string m_Name, string m_canFrame, string m_msgID, int m_bitNo)
        {

            Name = m_Name;
            msgID = m_msgID;
            canFrame = m_canFrame;
            bitNo = m_bitNo;
        }
    }
}
