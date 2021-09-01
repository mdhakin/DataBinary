using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Compare_dataset;
using System.IO;

namespace outputCSV
{
    class Program
    {
        static int Main(string[] args)
        {
            
            if (args.Length > 2)
            {
                int[] indx = new int[args.Length - 2];

                for (int i = 0; i < indx.Length; i++)
                {
                    indx[i] = Convert.ToInt32(args[i+2]);
                }

                OutCSV csv = new OutCSV(args[0], indx);
                string[] retval = csv.getCSV(indx);
                File.WriteAllLines(args[1], retval);
                
                return 27;
            }
            else
            {
                
                return -1;
            }
            
        }
    }

    public class OutCSV
    {
        public string FileName { get => m_filename; }
        private string m_filename;
        public OutCSV(string sFileName, int[] args)
        {
            m_filename = sFileName;
        }

        public string[] getCSV(int[] indexes)
        {
            
            
            Compare_dataset.Compare_dataset data = new Compare_dataset.Compare_dataset(this.m_filename,indexes);

            string[] outstr = data.getResult();

            return outstr;
        }

    }
}
