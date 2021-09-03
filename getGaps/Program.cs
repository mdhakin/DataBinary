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
                Console.WriteLine("You need to pass a file name with full path.");
                Console.ReadLine();
                return -1;
            }

            if (!File.Exists(args[0]))
            {
                Console.WriteLine("The File" + args[0] + " does not exist.");
                Console.ReadLine();
                return -1;
            }

            DataBinary.RawFile raw = new DataBinary.RawFile(args[0]);
            UInt32 interval = Convert.ToUInt32(args[1]);
            int[] ids = new int[1];

            List<string> gaps = DataBinary.CombineRawFiles.loaddatsStatic(ref raw, interval);

            string[] gapparts = gaps[5].Split(',');

            begins = new UInt32[gaps.Count - 4];
            ends = new UInt32[gaps.Count - 4];

            begins[0] = raw.Msgtime[0];
            ends[0] = ConvertUnixTimeStringToUInt32(gapparts[2]);

            ends[ends.Length - 1] = raw.Msgtime[raw.RecordCount - 1];

            int ct = 1;
            for (int i = 6; i < gaps.Count - 1; i++)
            {
                string[] firts_parts = gaps[i-1].Split(',');
                string[] gapparts2 = gaps[i].Split(',');
                ends[ct] = ConvertUnixTimeStringToUInt32(gapparts2[2]);
                begins[ct] = ConvertUnixTimeStringToUInt32(firts_parts[4]);
                ct++;
            }

            
            string[] gapparts3 = gaps[gaps.Count - 2].Split(',');
            string[] getparts4 = gaps[gaps.Count - 1].Split(',');
            begins[ct] = ConvertUnixTimeStringToUInt32(gapparts3[4]);
            
            

            ends[ct] = ConvertUnixTimeStringToUInt32(getparts4[2]);
            
            ct++;
            begins[ct] = ConvertUnixTimeStringToUInt32(getparts4[4]);
            string[] outputfile = new string[begins.Length];
            ct++;
            for (int i = 0; i < outputfile.Length; i++)
            {
                outputfile[i] = begins[i].ToString() + "|" + ends[i].ToString() + "|" + (Convert.ToInt32(ends[i]) - Convert.ToInt32(begins[i])).ToString()+"|";
            }

            string softID = args[0].Substring(args[0].Length - 8, 4) + ".txt";
            if (File.Exists(softID))
            {
                File.Delete(softID);
            }

            File.WriteAllLines(softID, outputfile);

            //Console.ReadLine();
            return 0;
        }

        private static UInt32 ConvertUnixTimeStringToUInt32(string input)
        {
            UInt32 output = 0;

            output = Convert.ToUInt32(input);

            return output;
        }
    }
}
