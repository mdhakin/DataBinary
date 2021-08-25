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
            //Console.WriteLine(args[0]);
            int loops = Convert.ToInt32(args[3]);
            string sPath1 = args[0];
            string sPath2 = @"C:\output\5590\5590.dat";
          
            if (File.Exists(sPath1) && File.Exists(sPath2))
            {
               

                int[] ids = new int[2];
                ids[0] = Convert.ToInt32(args[1]);
                ids[1] = Convert.ToInt32(args[2]);

                Console.WriteLine("ID 1 " + ids[0].ToString());
                Console.WriteLine("ID 2 " + ids[1].ToString());

                Compare_dataset.Compare_dataset data = new Compare_dataset.Compare_dataset(sPath1, ids);

                

                List<CAN_Signals.Signal> sigs = data.getIDs();
                for (int i = 0; i < sigs.Count; i++)
                {
                    Console.WriteLine(i.ToString() + " " + sigs[i].Name);
                }

                string[] result = data.getResult();
                for (int i = 0; i < loops; i++)
                {
                    Console.WriteLine(result[i]);
                }
                
            }

            Console.ReadLine();
        }
    }
}
