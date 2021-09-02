using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace getGaps
{
    class Program
    {
        static int Main(string[] args)
        {
            UInt32[] begins;
            UInt32[] ends;
            if (args.Length < 1)
            {
                return -1;
            }

            if (!File.Exists(args[0]))
            {
                return -1;
            }

            DataBinary.RawFile raw = new DataBinary.RawFile(args[0]);
            int[] ids = new int[1];

            List<string> gaps = DataBinary.CombineRawFiles.loaddatsStatic(ref raw, 50000);

            begins = new UInt32[gaps.Count - 5];
            ends = new UInt32[gaps.Count - 5];

            UInt32 begintime = raw.Msgtime[0];
            UInt32 endtime = raw.Msgtime[raw.RecordCount - 1];
            int ct = 0;
            for (int i = 5; i < gaps.Count; i++)
            {
                string[] parts = gaps[i].Split(',');

                string begin = parts[4];
                string endit = parts[2];

                begins[ct] = Convert.ToUInt32(begin);
                ends[ct] = Convert.ToUInt32(endit);
                ct++;
                
            }

            UInt32[] newBigin = new UInt32[begins.Length + 1];
            UInt32[] newEnd = new UInt32[begins.Length + 1];

            newBigin[0] = begintime;
            newEnd[newEnd.Length -1] = endtime;

            ct = 1;
            int ct2 = 0;
            for (int i = 0; i < begins.Length; i++)
            {
                newBigin[ct] = begins[i];
                newEnd[ct2] = ends[i];


                ct++;
                ct2++;
            }

            //newBigin[newBigin.Length - 1] = begins[begins.Length - 1];

            for (int i = 0; i < newBigin.Length; i++)
            {
                DateTime datebegin = DataBinary.UT.UnixTimeStampToDateTime(newBigin[i]);
                DateTime dateend = DataBinary.UT.UnixTimeStampToDateTime(newEnd[i]);
                int len = (int)(newEnd[i] - newBigin[i]);
                Console.WriteLine(len.ToString() + "  Begin: " + datebegin.ToString() + " End: " + dateend.ToString());
            }
            

            Console.ReadLine();
            return 0;
        }
    }
}
