using DataBinary;
using MessageGroup;

namespace test
{
    public static class Factory
    {
        public static IRawFile CreateRawFile(string sPath)
        {
            return new RawFile(sPath);
        }

        public static IMessageGroup CreateMessageGroup(long ts, int mCount)
        {
            return new MessageGroup.MessageGroup(ts, mCount);
        }
    }
}
