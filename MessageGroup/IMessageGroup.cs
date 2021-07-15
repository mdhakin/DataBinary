namespace MessageGroup
{
    public interface IMessageGroup
    {
        long timestamp { get; set; }
        int MessageCount { get; set; }
        message[] Messageset { get; }
    }
}