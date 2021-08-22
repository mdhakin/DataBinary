using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace DataLoggerParams
{
    public class MessageIDs
    {
        private string mFileName;
        private string signalsFile;
        public MessageIDs()
        {
            mFileName = "messageids.ini";
            signalsFile = "MCRRS_SIGNALS.ini";
        }

        public string[] getIDs()
        {
            if (File.Exists(mFileName))
            {
                string[] idss = File.ReadAllLines(mFileName);
                return idss;
            }else
            {
                return null;
            }
        }

        public List<string> SignalsInId(string idd)
        {
            List<string> signals = new List<string>();

            if (File.Exists(signalsFile))
            {
                string[] lines = File.ReadAllLines(signalsFile);
                for (int i = 0; i < lines.Length; i++)
                {
                    string[] parts = lines[i].Split(',');

                    if (parts[0] == "SixteenBitSignal")
                    {
                        if (parts[5] == idd)
                        {
                            signals.Add(parts[1]);
                        }

                    }else if (parts[0] == "EightBitSignal")
                    {
                        if (parts[4] == idd)
                        {
                            signals.Add(parts[1]);
                        }
                    }
                    else
                    {
                        if (parts[4] == idd)
                        {
                            signals.Add(parts[1]);
                        }
                    }
                }


            }

            return signals;

        }

        // sixteen pos 5
        // eaight 4
        // bool 4


    }
}
