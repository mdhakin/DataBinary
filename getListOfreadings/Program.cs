using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace getListOfreadings
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] output = go(args);
            File.WriteAllLines("readings.txt", output);
        }
        static string[] go(string[] args)
        {
            
            if (args.Length > 2)
            {
                int[] indx = new int[args.Length - 2];

                for (int i = 0; i < indx.Length; i++)
                {
                    indx[i] = Convert.ToInt32(args[i + 2]);
                }
                Compare_dataset.Compare_dataset data = new Compare_dataset.Compare_dataset(args[0], indx);
                List<CAN_Signals.Signal> idslist = data.getIDs();
                string[] ids = new string[idslist.Count];

                for (int i = 0; i < ids.Length; i++)
                {
                    ids[i] = idslist[i].Name;
                }
                return ids;
            }else
            {
                string[] nullstr = new string[1];
                nullstr[0] = "Error";
                return nullstr;
            }

            
            


            
        }
    }
}
