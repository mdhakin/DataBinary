using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Output_Data_File
{
    class Program
    {
        static void Main(string[] args)
        {
            string sFilename;
            if (args.Length > 0)
            {
                sFilename = args[0];
            }else
            {
                sFilename = @"C:\output\5497\5497.dat";
            }
            Console.WriteLine(sFilename);
            menu(args);
             Console.ReadLine();

        }

        private static void menu(string[] args)
        {
            string q = "";

            while (q != "q")
            {
                Console.WriteLine("Select an Option.");
                Console.WriteLine("1. output Signals.\n2. Output Info\n3. q to quit.");
                q = Console.ReadLine();
                Console.WriteLine(q + args[0]);
            }
        }
    }

    public class OuputFile
    {
        public string FileName { get => this.mFile_Name; }
        private string mFile_Name;
        public OuputFile(string sFileName)
        {
            mFile_Name = sFileName;
        }
    }

}