using System;
using System.Collections;

namespace MessageGroup
{
    /// <summary>
    /// Is a container for a group of messages comprising a full set of readings
    /// for a moment in time. The timestamp is a long value identifing the common 
    /// Timestamp for all contained messages. 
    /// </summary>
    public class MessageGroup 
    {
        public UInt32 timestamp { get => mTimeStamp;}
        private static int messagecount = 13;
        private UInt32 mTimeStamp = new UInt32();

        public UInt16[] MesssageID = new UInt16[messagecount];
        public byte[] frame0 = new byte[messagecount];
        public byte[] frame1 = new byte[messagecount];
        public byte[] frame2 = new byte[messagecount];
        public byte[] frame3 = new byte[messagecount];
        public byte[] frame4 = new byte[messagecount];
        public byte[] frame5 = new byte[messagecount];
        public byte[] frame6 = new byte[messagecount];
        public byte[] frame7 = new byte[messagecount];


        private int[] messagetaken = new int[messagecount];

        private bool bTimeStampSet = false;

        public int getMeassageSixe()
        {
            return messagecount;
        }
        public MessageGroup()
        {

            for (int i = 0; i < messagecount; i++)
            {
                messagetaken[i] = 0;
            }
            

        }

        public bool setTimeStamp(UInt32 ts)
        {
            if (ts > 0)
            {
                mTimeStamp = ts;
                bTimeStampSet = true;
                return true;
            }else
            {
                return false;
            }
        }

        // 1627507007
        public int SetMessages(UInt16 id, byte f0, byte f1, byte f2, byte f3, byte f4, byte f5, byte f6, byte f7, int index)
        {

            if (messagetaken[index] == 0 && bTimeStampSet == true && (f0 > -1 && f0 < 256) && (f1 > -1 && f1 < 256) && (f2 > -1 && f2 < 256) && (f3 > -1 && f3 < 256) && (f4 > -1 && f4 < 256) && (f5 > -1 && f5 < 256) && (f6 > -1 && f6 < 256) && (f7 > -1 && f7 < 256))
            {
                messagetaken[index] = 1;
                MesssageID[index] = id;
                frame0[index] = f0;
                frame1[index] = f1;
                frame2[index] = f2;
                frame3[index] = f3;
                frame4[index] = f4;
                frame5[index] = f5;
                frame6[index] = f6;
                frame7[index] = f7;

                
                return 0;
            }else
            {
                return -1;
            }
        }

        public void releaseIndex(int index)
        {
            messagetaken[index] = 0;
        }

        public int[] ListTakenMessages()
        {
            int[] taken = new int[messagecount];

            for (int i = 0; i < messagecount; i++)
            {
                taken[i] = messagetaken[i];
            }


            return taken;
        }
    }

    
}
