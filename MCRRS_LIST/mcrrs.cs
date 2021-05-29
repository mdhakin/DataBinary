using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MCRRS_LIST
{
    public class mcrrs
    {
        public string[] Mchines { get => machines; }
        string[] machines;
        public mcrrs()
        {
           machines = File.ReadAllLines("vehicles.ini");
        }
        
        public string getUSNfromID(string id)
        {

            for (int i = 0; i < machines.Length; i++)
            {
                string[] parts = machines[i].Split('|');
                if (parts[0] == id)
                {
                    return parts[1];
                }
            }

            return "";
        }
    }
}
