using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ConsoleTestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            string sPath1 = @"C:\Users\mhakin.TRIDOMAIN\OneDrive\code\csharp\DataBinary\test\bin\Debug\output\6459\6459.dat";
            string sPath2 = @"C:\Users\mhakin.TRIDOMAIN\OneDrive\code\csharp\DataBinary\test\bin\Debug\output\6511\6511.dat";
            DataBinary.CombineRawFiles.LinearMsg[] output;
            if (File.Exists(sPath1) && File.Exists(sPath2))
            {
                DataBinary.RawFile raw1 = new DataBinary.RawFile(sPath1);
                DataBinary.RawFile raw2 = new DataBinary.RawFile(sPath2);

                DataBinary.CombineRawFiles combine = new DataBinary.CombineRawFiles();

                output = combine.HashCombine(raw1, raw2);
                Console.WriteLine("Ouputfile memory: " + (output.Length * 154).ToString() + " bytes");
                Console.WriteLine("Inputfile one memory: " + (raw1.RecordCount * 14).ToString() + " bytes for " + raw1.RecordCount.ToString() + " Records");
                Console.WriteLine("Inputfile two memory: " + (raw2.RecordCount * 14).ToString() + " bytes for " + raw2.RecordCount.ToString() + " Records");

                for (UInt32 i = 0; i < output.Length; i++)
                {
                    if (output[i].messageBlock[0].ts > 0)
                    {
                        Console.WriteLine(output[i].messageBlock[0].ts.ToString());
                    }
                    
                }
            }

            

            Console.ReadLine();
        }
    }
}
