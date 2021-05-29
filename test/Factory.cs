using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
