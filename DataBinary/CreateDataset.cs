using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data;
namespace DataBinary
{
    public class CreateDataset
    {
        UInt32 strarttime;// = 1500000000;
        UInt32 interval;// = 10;
        int reccount;// = 300;
        int whenToGap;// = 7;
        string outputFileName;

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

        public CreateDataset()
        {
            strarttime = 1500000000;
            interval = 10;
            reccount = 300;
            whenToGap = 7;
        }
        public CreateDataset(UInt32 startTime, int RecordCount, int LoopNumToGap)
        {
            interval = 10;
            reccount = RecordCount;
            strarttime = startTime;
            whenToGap = LoopNumToGap;
            
        }

        public void addMessageToFile(msg[] msges, string fName)
        {
            string sFileName = fName;


            using (BinaryWriter writer = new BinaryWriter(File.Open(sFileName, FileMode.Append)))
            {
                for (int i = 0; i < msges.Length; i++)
                {
                    writer.Write(msges[i].ts);
                    writer.Write(msges[i].msgID);
                    writer.Write(msges[i].f0);
                    writer.Write(msges[i].f1);
                    writer.Write(msges[i].f2);
                    writer.Write(msges[i].f3);
                    writer.Write(msges[i].f4);
                    writer.Write(msges[i].f5);
                    writer.Write(msges[i].f6);
                    writer.Write(msges[i].f7);
                }
                writer.Close();
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


        public void CreateDatasetFor(string sFileName, byte Ddata)
        {
            UInt32 currenttime = strarttime;

            msg[] message = new msg[reccount];
            int lpct = 0;
            for (int i = 0; i < reccount; i++)
            {
                
                UInt32 ts = currenttime;
                UInt16 msgid = 200;
                byte f0 = Ddata;
                    

                message[i].ts = ts;
                message[i].msgID = msgid;
                message[i].f0 = f0;
                message[i].f1 = f0;
                message[i].f2 = f0;
                message[i].f3 = f0;
                message[i].f4 = f0;
                message[i].f5 = f0;
                message[i].f6 = f0;
                message[i].f7 = f0;
                lpct++;

                if (lpct == 11)
                {
                    if (i == ((11* whenToGap) -1))
                    {
                        currenttime += (interval*10000);
                    }else
                    {
                        currenttime += interval;
                    }
                    lpct = 0;
                    
                }
            }

            if (File.Exists(sFileName))
            {
                File.Delete(sFileName);
            }

            using (BinaryWriter writer = new BinaryWriter(File.Open(sFileName, FileMode.Append)))
            {
                for (int i = 0; i < message.Length; i++)
                {
                    writer.Write(message[i].ts);
                    writer.Write(message[i].msgID);
                    writer.Write(message[i].f0);
                    writer.Write(message[i].f1);
                    writer.Write(message[i].f2);
                    writer.Write(message[i].f3);
                    writer.Write(message[i].f4);
                    writer.Write(message[i].f5);
                    writer.Write(message[i].f6);
                    writer.Write(message[i].f7);
                }
                writer.Close();
            }

        }
    }
}
