namespace DataBinary
{
    public interface IRawFile
    {
        byte[] F0 { get; }
        byte[] F1 { get; }
        byte[] F2 { get; }
        byte[] F3 { get; }
        byte[] F4 { get; }
        byte[] F5 { get; }
        byte[] F6 { get; }
        byte[] F7 { get; }
        string FileEnddate { get; }
        string FileError { get; }
        string Filename { get; }
        bool FileReady { get; }
        string FileStartDate { get; }
        ushort[] Msgid { get; }
        uint[] Msgtime { get; }
        long RecordCount { get; }
        int TotalHours { get; }

        string Error();
        long sizeInBytes();

        long checkData();
    }
}