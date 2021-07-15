using System;
using System.Collections;

namespace MessageGroup
{
    /// <summary>
    /// Is a container for a group of messages comprising a full set of readings
    /// for a moment in time. The timestamp is a long value identifing the common 
    /// Timestamp for all contained messages. 
    /// </summary>
    public class MessageGroup : IMessageGroup
    {
        public long timestamp { get; set; }

        private long mTimeStamp;

        

        public message[] Messageset { get => mMessageset; }
        private message[] mMessageset;

        public int MessageCount { get; set; }
        

        public MessageGroup(long ts, int messagecount)
        {
            this.mTimeStamp = ts;
            
            this.MessageCount = messagecount;

            mMessageset = new message[messagecount];
        }
    }

    public class message
    {
        public long TimeStamp { get; set; }
        

        public UInt16 MsgID { get; set; }
        

        public byte[] frames;
        public message()
        {
            //mTimeStamp = ts;
            //frames = new byte[8];
            //mMsgID = id;
            //frames[0] = f0;
            //frames[1] = f1;
            //frames[2] = f2;
            //frames[3] = f3;
            //frames[4] = f4;
            //frames[5] = f5;
            //frames[6] = f6;
            //frames[7] = f7;

        }
    }

    class MessageGroupCompare : IComparer
    {
        public int Compare(object x, object y)
        {
            return (new CaseInsensitiveComparer()).Compare(((MessageGroup)x).timestamp, ((MessageGroup)y).timestamp);
        }

        // Example
        /*
        MessageGroup[] group = {
            new MessageGroup(){ FirstName="Steve", LastName="Jobs"},
            new MessageGroup(){ FirstName="Bill", LastName="Gates"},
            new MessageGroup(){ FirstName="Lary", LastName="Page"}
        };
 
        Array.Sort(people, new PersonComparer());
         
         */
    }
}
