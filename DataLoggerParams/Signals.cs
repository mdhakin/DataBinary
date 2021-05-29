using System;
using System.Collections.Generic;
using System.IO;

namespace DataLoggerParams
{
    public class Signals : ISignals
    {
        public static List<ISixteenBitSignal> sixteenbitsignals;
        public static List<IEightBitSignal> eightBitSignals;
        public static List<IBoolSignal> boolsignals;

        public List<ISixteenBitSignal> SixTeenBitSignals { get => sixteenbitsignals; }
        public List<IEightBitSignal> EaghtBitSignals { get => eightBitSignals; }
        public List<IBoolSignal> BoolSignals { get => boolsignals; }

        public string InIFileNme { get => m_inifilename; }
        private string m_inifilename;

        private static string sixteen = "SixteenBitSignal";
        private static string eaight = "EightBitSignal";
        private static string bool_sig = "BoolSignal";
        public Signals(string iniFileName)
        {
            m_inifilename = iniFileName;
            sixteenbitsignals = new List<ISixteenBitSignal>();
            eightBitSignals = new List<IEightBitSignal>();
            boolsignals = new List<IBoolSignal>();

            loadSixTeenBitList(m_inifilename);
            loadEaightBitSignals(m_inifilename);
            loadBoolLists(m_inifilename);
        }

        private static void loadSixTeenBitList(string sFileNme)
        {
            string[] lines = File.ReadAllLines(sFileNme);

            for (int i = 0; i < lines.Length; i++)
            {
                string[] parts = lines[i].Split(',');

                if (parts[0] == sixteen)
                {
                    ISixteenBitSignal sig = Factory.CreateSixteenBitSignal(parts[2], parts[3], parts[4], parts[5], Convert.ToInt32(parts[6]), Convert.ToInt32(parts[7]), Convert.ToInt32(parts[8]));
                    sixteenbitsignals.Add(sig);
                }
            }
        }

        private static void loadEaightBitSignals(string sFileNme)
        {
            string[] lines = File.ReadAllLines(sFileNme);

            for (int i = 0; i < lines.Length; i++)
            {
                string[] parts = lines[i].Split(',');

                if (parts[0] == eaight)
                {
                    IEightBitSignal sig = Factory.CreateEightBitSignal(parts[2], parts[3], parts[4], Convert.ToInt32(parts[5]), Convert.ToInt32(parts[6]), Convert.ToInt32(parts[7]));
                    eightBitSignals.Add(sig);
                }
            }
        }

        private static void loadBoolLists(string sFileName)
        {
            string[] lines = File.ReadAllLines(sFileName);
            for (int i = 0; i < lines.Length; i++)
            {
                string[] parts = lines[i].Split(',');
                if (parts[0] == bool_sig)
                {
                    IBoolSignal sig = Factory.CreateBoolSignal(parts[2], parts[3], parts[4], Convert.ToInt32(parts[5]));
                    boolsignals.Add(sig);
                }
            }
        }
    }
}
