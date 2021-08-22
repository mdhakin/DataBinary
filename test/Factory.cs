using DataBinary;


namespace test
{
    public static class Factory
    {
        public static IRawFile CreateRawFile(string sPath)
        {
            return new RawFile(sPath);
        }

       
    }
}
