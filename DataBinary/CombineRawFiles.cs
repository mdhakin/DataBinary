using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using System.Data;

namespace DataBinary
{
    public class CombineRawFiles
    {
        /// <summary>
        /// Struct to contain one full message set;
        /// </summary>
        public struct msg
        {
            public uint ts;
            public ushort msgID;
            public byte f0;
            public byte f1;
            public byte f2;
            public byte f3;
            public byte f4;
            public byte f5;
            public byte f6;
            public byte f7;
        }


        /// <summary>
        /// This function will determine the list of gaps defined by the timeCutoff parameter
        /// </summary>
        /// <param name="raw">The Raw data file Dataset</param>
        /// <param name="timeCutoff">The minimum time to use to determne gaps in the datafile</param>
        /// <returns>Returns a List of type string</returns>
        public List<string> loaddats(ref DataBinary.RawFile raw, UInt32 timeCutoff)
        {
            // split the filename path to get the software ID
            string[] fn = raw.Filename.Split('\\');
            
            // Create the output file name
            string outname = "VehicleID- " + fn[fn.Length - 1].Substring(0, 4) + ".csv";

            // Array to hold the gap data
            string[] gaps = new string[raw.RecordCount];
            List<string> gaps2 = new List<string>();

            MCRRS_LIST.mcrrs mccris = new MCRRS_LIST.mcrrs();
            string usn = mccris.getUSNfromID(fn[fn.Length - 1].Substring(0, 4));
            string headder1 = "USN Number: " + usn + ", Data File Start Date:" + raw.FileStartDate.ToString() + ", Data File End Date: " + raw.FileEnddate.ToString() + ",,,,,";
            string headder2 = "***************************************************************************************************************";
            string infoheader = "Time Cutoff=" + timeCutoff.ToString() + " In seconds, Downtime Anaysis File Creation Date: " + DateTime.Now.ToString() + ",,,,,,";
            string headder = "SECONDS, START OF IDLE, UNIX TS,END OF IDLE,UNIX TS,MINUTES,HOURS,DAYS";
            string titleHeader = "MCRRS Downtime Report.";

            gaps2.Add(titleHeader);
            gaps2.Add(headder1);
            gaps2.Add(infoheader);
            gaps2.Add(headder2);
            gaps2.Add(headder);
            for (int i = 0; i < raw.RecordCount - 1; i++)
            {
                UInt32 t1 = raw.Msgtime[i];
                UInt32 t2 = raw.Msgtime[i + 1];

                UInt32 diff = t2 - t1;

                if (diff == 0)
                {
                    gaps[i] = "-";
                }
                else if (diff > timeCutoff)
                {
                    double mins = diff / 60;
                    double hours = mins / 60;
                    double days = hours / 24;

                    string begindate = DataBinary.UT.UnixTimeStampToDateTime(Convert.ToDouble(t1)).ToString();
                    string enddate = DataBinary.UT.UnixTimeStampToDateTime(Convert.ToDouble(t2)).ToString();

                    if (diff >= timeCutoff && t2 > t1)
                    {

                        gaps2.Add(diff.ToString() + "," + begindate + "," + t1.ToString() + "," + enddate + "," + t2.ToString() + "," + ((int)(mins)).ToString() + "," + Math.Round(hours, 1).ToString() + "," + ((decimal)Math.Round(days, 2)).ToString());
                    }

                }
                else
                {
                    gaps[i] = "-";
                }
            }
            
            return gaps2;

        }

        
        private msg[] removeEmpties(msg[] inputMsg, UInt32 lastRow)
        {
            msg[] t = new msg[lastRow];

            for (int i = 0; i < lastRow; i++)
            {
                t[i].ts = inputMsg[i].ts;
                t[i].msgID = inputMsg[i].msgID;
                t[i].f0 = inputMsg[i].f0;
                t[i].f1 = inputMsg[i].f1;
                t[i].f2 = inputMsg[i].f2;
                t[i].f3 = inputMsg[i].f3;
                t[i].f4 = inputMsg[i].f4;
                t[i].f5 = inputMsg[i].f5;
                t[i].f6 = inputMsg[i].f6;
                t[i].f7 = inputMsg[i].f7;

            }

            return t;
        }

        public RawFile Combine_Return_RawFile(DataBinary.RawFile raw1, DataBinary.RawFile raw2, UInt32 gapWidth)
        {
            DataTable outputOne = Combine_returnDataTable(raw1, raw2, gapWidth);

            if (File.Exists("temp.dat"))
            {
                File.Delete("temp.dat");
            }

            WriteDatatableToFile(ref outputOne, "temp.dat");

            RawFile raw = new RawFile("temp.dat");

            if (raw.FileReady)
            {
                return raw;
            }else
            {
                return null;
            }

        }


        public static void WriteDatatableToFile(ref DataTable dt, string sFileName)
        {
            if (File.Exists(sFileName))
            {
                File.Delete(sFileName);
            }

            using (BinaryWriter writer = new BinaryWriter(File.Open(sFileName, FileMode.Create)))
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr = dt.Rows[i];
                    writer.Write((UInt32)dr.Field<UInt32>(0));
                    writer.Write((UInt16)dr.Field<UInt16>(1));
                    writer.Write((byte)dr.Field<byte>(2));
                    writer.Write((byte)dr.Field<byte>(3));
                    writer.Write((byte)dr.Field<byte>(4));
                    writer.Write((byte)dr.Field<byte>(5));
                    writer.Write((byte)dr.Field<byte>(6));
                    writer.Write((byte)dr.Field<byte>(7));
                    writer.Write((byte)dr.Field<byte>(8));
                    writer.Write((byte)dr.Field<byte>(9));
                }
                writer.Close();
            }

        }

        public DataTable Combine_returnDataTable(DataBinary.RawFile raw1, DataBinary.RawFile raw2, UInt32 gapWidth)
        {
            DataTable ddt = new DataTable();

            ddt.Columns.Add("TimeStamp", typeof(UInt32));
            ddt.Columns.Add("MessageID", typeof(UInt16));
            ddt.Columns.Add("f0", typeof(byte));
            ddt.Columns.Add("f1", typeof(byte));
            ddt.Columns.Add("f2", typeof(byte));
            ddt.Columns.Add("f3", typeof(byte));
            ddt.Columns.Add("f4", typeof(byte));
            ddt.Columns.Add("f5", typeof(byte));
            ddt.Columns.Add("f6", typeof(byte));
            ddt.Columns.Add("f7", typeof(byte));

            msg[] msgs = Combine(raw1, raw2, gapWidth);


            for (int i = 0; i < msgs.Length; i++)
            {
                DataRow dr = ddt.NewRow();
                dr["TimeStamp"] = msgs[i].ts;
                dr["MessageID"] = msgs[i].msgID;
                dr["f0"] = msgs[i].f0;
                dr["f1"] = msgs[i].f1;
                dr["f2"] = msgs[i].f2;
                dr["f3"] = msgs[i].f3;
                dr["f4"] = msgs[i].f4;
                dr["f5"] = msgs[i].f5;
                dr["f6"] = msgs[i].f6;
                dr["f7"] = msgs[i].f7;

                ddt.Rows.Add(dr);
            }

            return ddt;
        }


        public msg[] Combine(DataBinary.RawFile raw1, DataBinary.RawFile raw2, UInt32 gapWidth)
        {
            DataBinary.RawFile primary;
            DataBinary.RawFile Secondary;

            // See which rawfile starts earlier
            if (raw1.Msgtime[0] > raw2.Msgtime[0])
            {
                primary = raw2;
                Secondary = raw1;
            }
            else
            {
                primary = raw1;
                Secondary = raw2;
            }

            // loaddats is the fuction that loads the breaks in the data. 
            // comboBox1 holds the min time a gap has to be to count it as a gap
            List<string> gaps3 = loaddats(ref primary, gapWidth);

            // What is the max size the new dataset can be?
            UInt32 combinedTotal = (UInt32)(primary.RecordCount + Secondary.RecordCount);

            // msg to hold the combined dataset
            msg[] t = new msg[combinedTotal];

            // calculate the performance;
            var watch = new Stopwatch();
            watch.Start();

            // List of gaps in the primary file
            UInt32[] gapstarts = new UInt32[gaps3.Count - 5];
            UInt32[] gapends = new UInt32[gaps3.Count - 5];


            // fill gap info
            for (int i = 5; i < gaps3.Count; i++)
            {
                string[] parts = gaps3[i].Split(',');

                gapstarts[i - 5] = Convert.ToUInt32(parts[2]);
                gapends[i - 5] = Convert.ToUInt32(parts[4]);
            }


            // Keep track of the last index used
            UInt32 lastPrimaryIndex = 0;
            UInt32 lastnewIndex = 0;

            // Main algorithm.
            for (int i = 0; i < gapstarts.Length; i++)
            {
                // fill all primary up until the first gap starts
                while (primary.Msgtime[lastPrimaryIndex] < gapstarts[i])
                {
                    t[lastnewIndex].ts = primary.Msgtime[lastPrimaryIndex];
                    t[lastnewIndex].msgID = primary.Msgid[lastPrimaryIndex];
                    t[lastnewIndex].f0 = primary.F0[lastPrimaryIndex];
                    t[lastnewIndex].f1 = primary.F1[lastPrimaryIndex];
                    t[lastnewIndex].f2 = primary.F2[lastPrimaryIndex];
                    t[lastnewIndex].f3 = primary.F3[lastPrimaryIndex];
                    t[lastnewIndex].f4 = primary.F4[lastPrimaryIndex];
                    t[lastnewIndex].f5 = primary.F5[lastPrimaryIndex];
                    t[lastnewIndex].f6 = primary.F6[lastPrimaryIndex];
                    t[lastnewIndex].f7 = primary.F7[lastPrimaryIndex];
                    lastnewIndex++;
                    lastPrimaryIndex++;
                }

                // find items in the secondary that fall in the current gap 
                // add those to the file
                for (int j = 0; j < Secondary.RecordCount; j++)
                {
                    if (Secondary.Msgtime[j] > gapstarts[i] && Secondary.Msgtime[j] < gapends[i])
                    {
                        t[lastnewIndex].ts = Secondary.Msgtime[j];
                        t[lastnewIndex].msgID = Secondary.Msgid[j];
                        t[lastnewIndex].f0 = Secondary.F0[j];
                        t[lastnewIndex].f1 = Secondary.F1[j];
                        t[lastnewIndex].f2 = Secondary.F2[j];
                        t[lastnewIndex].f3 = Secondary.F3[j];
                        t[lastnewIndex].f4 = Secondary.F4[j];
                        t[lastnewIndex].f5 = Secondary.F5[j];
                        t[lastnewIndex].f6 = Secondary.F6[j];
                        t[lastnewIndex].f7 = Secondary.F7[j];
                        lastnewIndex++;

                    }
                }

                // check to see if there are any record left
                if (i == gaps3.Count - 6)
                {

                    while (lastPrimaryIndex < primary.RecordCount)
                    {
                        if (primary.Msgtime[lastPrimaryIndex] > gapends[i])
                        {
                            t[lastnewIndex].ts = primary.Msgtime[lastPrimaryIndex];
                            t[lastnewIndex].msgID = primary.Msgid[lastPrimaryIndex];
                            t[lastnewIndex].f0 = primary.F0[lastPrimaryIndex];
                            t[lastnewIndex].f1 = primary.F1[lastPrimaryIndex];
                            t[lastnewIndex].f2 = primary.F2[lastPrimaryIndex];
                            t[lastnewIndex].f3 = primary.F3[lastPrimaryIndex];
                            t[lastnewIndex].f4 = primary.F4[lastPrimaryIndex];
                            t[lastnewIndex].f5 = primary.F5[lastPrimaryIndex];
                            t[lastnewIndex].f6 = primary.F6[lastPrimaryIndex];
                            t[lastnewIndex].f7 = primary.F7[lastPrimaryIndex];
                            lastnewIndex++;
                        }

                        lastPrimaryIndex++;
                    }

                    // test to see if there are any more in the secondary
                    for (int j = 0; j < Secondary.RecordCount; j++)
                    {
                        if (Secondary.Msgtime[j] > primary.Msgtime[lastPrimaryIndex - 1])
                        {
                            t[lastnewIndex].ts = Secondary.Msgtime[j];
                            t[lastnewIndex].msgID = Secondary.Msgid[j];
                            t[lastnewIndex].f0 = Secondary.F0[j];
                            t[lastnewIndex].f1 = Secondary.F1[j];
                            t[lastnewIndex].f2 = Secondary.F2[j];
                            t[lastnewIndex].f3 = Secondary.F3[j];
                            t[lastnewIndex].f4 = Secondary.F4[j];
                            t[lastnewIndex].f5 = Secondary.F5[j];
                            t[lastnewIndex].f6 = Secondary.F6[j];
                            t[lastnewIndex].f7 = Secondary.F7[j];
                            lastnewIndex++;
                        }
                    }

                }

            }
            watch.Stop();

            msg[] output = removeEmpties(t, lastnewIndex);

            return output;
        }
    }
}
