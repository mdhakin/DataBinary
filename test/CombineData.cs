using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using System.Linq;
using System.Diagnostics;

namespace test
{
    public partial class CombineData : Form
    {
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
        public CombineData()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Enum to capture the state of selection. This 
        /// Because this program is intended to to combine two data files
        /// Only two files can be compared at a time.
        /// </summary>
        enum theState
        {
            none,
            one,
            two
        }

        // the List object raws contains a list of files that can be compared. This array holds the indices of the two raws
        // elements that will be compared.
        int[] selectedItems = new int[2];

        // Two raws indices need to be selected in order to run the analysis. As each of the two raws objects are
        // selected, the state is updated to reflect at what stage of slection the program is currently in.
        theState sttate;

        // This array of strings holds a list of all the datafiles that will be compared.
        string[] dataFiles;

        // This is a List of RawFiles that could be compared.
        List<DataBinary.RawFile> raws;
        private void CombineData_Load(object sender, EventArgs e)
        {
            clearSelectedItems();
            sttate = theState.none;
            UpdateView();
            comboBox1.SelectedIndex = 5;
            string[] dirs = Directory.GetDirectories(Application.StartupPath + "\\output");
            dataFiles = new string[dirs.Length];
            raws = new List<DataBinary.RawFile>();
            for (int i = 0; i < dataFiles.Length; i++)
            {
                string[] parts = dirs[i].Split('\\');
                dataFiles[i] = dirs[i] + "\\" + parts[parts.Length - 1] + ".dat";
                DataBinary.RawFile tempRaw = new DataBinary.RawFile(dataFiles[i]);
                raws.Add(tempRaw);
                listBox1.Items.Add(raws[i].sizeInBytes().ToString() + " Bytes\t" + raws[i].RecordCount.ToString() + " Records"); ;
            }

        }
        private void clearSelectedItems()
        {
            selectedItems[0] = -1;
            selectedItems[1] = -1;
        }

        /// <summary>
        /// This updates the text on the selection button and possibly any other element that 
        /// shows elements associated with the current state.
        /// </summary>
        private void UpdateView()
        {
            switch (sttate)
            {
                case theState.none:
                    // If the state is none, clear the options.
                    clearSelectedItems();
                    button1.Text = "Select First Input";
                    break;
                case theState.one:
                    button1.Text = "Select Second Input";
                    break;
                case theState.two:
                    button1.Text = "Run Combine";
                    break;
                default:
                    break;
            }
        }

        private void setitemsForanalysis()
        {
            if (listBox1.SelectedIndex > -1)
            {
                if (sttate == theState.none)
                {
                    selectedItems[0] = listBox1.SelectedIndex;
                    sttate = theState.one;
                    UpdateView();
                }
                else if (sttate == theState.one && selectedItems[0] != listBox1.SelectedIndex)
                {
                    selectedItems[1] = listBox1.SelectedIndex;
                    sttate = theState.two;
                    UpdateView();
                }
                else if (sttate == theState.two)
                {
                    if (selectedItems[0] != -1 && selectedItems[1] != -1)
                    {
                        // runCombine(raws[selectedItems[0]],raws[selectedItems[1]]);
                        RunCombine2(raws[selectedItems[0]], raws[selectedItems[1]]);
                        sttate = theState.none;
                        UpdateView();
                    }else
                    {
                        sttate = theState.none;
                        UpdateView();
                    }
                    
                }
                else
                {
                    sttate = theState.none;
                    UpdateView();
                }
            }
        }


        private void writeOutputFile(msg[] iinput, string fileName, UInt32 count)
        {
            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }
            using (BinaryWriter writer = new BinaryWriter(File.Open(fileName, FileMode.Create)))
            {
                for (UInt32 i = 0; i < count; i++)
                {
                    writer.Write(iinput[i].ts);
                    writer.Write(iinput[i].msgID);
                    writer.Write(iinput[i].f0);
                    writer.Write(iinput[i].f1);
                    writer.Write(iinput[i].f2);
                    writer.Write(iinput[i].f3);
                    writer.Write(iinput[i].f4);
                    writer.Write(iinput[i].f5);
                    writer.Write(iinput[i].f6);
                    writer.Write(iinput[i].f7);
                }
                //writer.Write(1.250F);
                //writer.Write(@"c:\Temp");
                //writer.Write(10);
                //writer.Write(true);
            }
        }

        private void RunCombine2(DataBinary.RawFile raw1, DataBinary.RawFile raw2)
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
            List<string> gaps3 = loaddats(primary, Convert.ToUInt32(comboBox1.Text));

            UInt32 combinedTotal = (UInt32)(primary.RecordCount + Secondary.RecordCount);
            UInt32 ct = 0;

            msg[] t = new msg[combinedTotal];
            var watch = new Stopwatch();
            watch.Start();

            string[] parts = gaps3[5].Split(',');
            // 2 4

            UInt32 currentPrimaryRecord = 0;
            //UInt32 currentSecondaryRecord = 0;
            UInt32 currentOutputRecord = 0;

            for (int i = 0; i < gaps3.Count - 5; i++)
            {
                UInt32 startTS = Convert.ToUInt32(parts[2]);
                UInt32 endTS = Convert.ToUInt32(parts[4]);

                // fill the output array until the current gap starts

               
                while (currentPrimaryRecord < startTS && currentOutputRecord < primary.RecordCount)
                {

                    for (int g = 0; g < 11; g++)
                    {
                        t[currentOutputRecord].ts = primary.Msgtime[currentPrimaryRecord];
                        t[currentOutputRecord].msgID = primary.Msgid[currentPrimaryRecord];
                        t[currentOutputRecord].f0 = primary.F0[currentPrimaryRecord];
                        t[currentOutputRecord].f1 = primary.F1[currentPrimaryRecord];
                        t[currentOutputRecord].f2 = primary.F2[currentPrimaryRecord];
                        t[currentOutputRecord].f3 = primary.F3[currentPrimaryRecord];
                        t[currentOutputRecord].f4 = primary.F4[currentPrimaryRecord];
                        t[currentOutputRecord].f5 = primary.F5[currentPrimaryRecord];
                        t[currentOutputRecord].f6 = primary.F6[currentPrimaryRecord];
                        t[currentOutputRecord].f7 = primary.F7[currentPrimaryRecord];
                        currentOutputRecord++;
                        currentPrimaryRecord++;
                    }
                    
                }



                if (currentOutputRecord < combinedTotal)
                {


                    for (int f = 0; f < Secondary.RecordCount; f++)
                    {
                        if (Secondary.Msgtime[f] > startTS && Secondary.Msgtime[f] < endTS || currentPrimaryRecord >= primary.RecordCount)
                        {
                            t[currentOutputRecord].ts = Secondary.Msgtime[f];
                            t[currentOutputRecord].msgID = Secondary.Msgid[f];
                            t[currentOutputRecord].f0 = Secondary.F0[f];
                            t[currentOutputRecord].f1 = Secondary.F1[f];
                            t[currentOutputRecord].f2 = Secondary.F2[f];
                            t[currentOutputRecord].f3 = Secondary.F3[f];
                            t[currentOutputRecord].f4 = Secondary.F4[f];
                            t[currentOutputRecord].f5 = Secondary.F5[f];
                            t[currentOutputRecord].f6 = Secondary.F6[f];
                            t[currentOutputRecord].f7 = Secondary.F7[f];

                            currentOutputRecord++;
                        }

                    }

                }
                //listBox6.Items.Add(tempct.ToString());


            }


            writeOutputFile(t, @"C:\output\5871\5871.dat", currentOutputRecord);




            //// fill the array until the first break in data
            //for (int i = 0; i < primary.RecordCount; i++)
            //{
            //    t[i].ts = primary.Msgtime[i];
            //    t[i].msgID = primary.Msgid[i];

            //    t[i].f0 = primary.F0[i];
            //    t[i].f1 = primary.F1[i];
            //    t[i].f2 = primary.F2[i];
            //    t[i].f3 = primary.F3[i];
            //    t[i].f4 = primary.F4[i];
            //    t[i].f5 = primary.F5[i];
            //    t[i].f6 = primary.F6[i];
            //    t[i].f7 = primary.F7[i];
            //    ct++;
            //}




            watch.Stop();

            MessageBox.Show(watch.ElapsedMilliseconds.ToString() + " ms, records written: " + currentOutputRecord.ToString());

            // msg[] combined = new msg[combinedTotal];




        }




        private void runCombine(DataBinary.RawFile raw1, DataBinary.RawFile raw2)
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


            List<string> gaps3 = loaddats(primary, Convert.ToUInt32(comboBox1.Text));
            //MessageBox.Show("The First Item is " + primary.FileStartDate.ToString() + " The Second is " + Secondary.FileStartDate.ToString());

            List<MessageGroup.MessageGroup> grp = new List<MessageGroup.MessageGroup>();

            for (int i = 0; i < primary.RecordCount - 11; i++)
            {
                MessageGroup.MessageGroup temp = new MessageGroup.MessageGroup();

                for (int j = 0; j < 11; j++)
                {
                    temp.setTimeStamp(primary.Msgtime[i]);
                    temp.MesssageID[j] = primary.Msgid[i + j];
                    temp.frame0[j] = primary.F0[i + j];
                    temp.frame1[j] = primary.F1[i + j];
                    temp.frame2[j] = primary.F2[i + j];
                    temp.frame3[j] = primary.F3[i + j];
                    temp.frame4[j] = primary.F4[i + j];
                    temp.frame5[j] = primary.F5[i + j];
                    temp.frame6[j] = primary.F6[i + j];
                    temp.frame7[j] = primary.F7[i + j];
                    //i++;
                }
                grp.Add(temp);
                i += 10;

            }


            int insertcount = 0;
           
            for (int i = 5; i < gaps3.Count; i++)
            {
                string lineone = "";
                string linetwo = "";
                string[] parts = gaps3[i].Split(',');

                

                UInt32 nextgapstart = Convert.ToUInt32(parts[2]);
                UInt32 nextgapend = Convert.ToUInt32(parts[4]);

                listBox3.Items.Add(nextgapstart.ToString());
                listBox4.Items.Add(nextgapend.ToString());

                for (int j = 0; j < Secondary.RecordCount; j++)
                {
                    if (Secondary.Msgtime[j] > nextgapstart && Secondary.Msgtime[j] < nextgapend)
                    {
                        listBox5.Items.Add(Secondary.Msgtime[j].ToString());
                        MessageGroup.MessageGroup temp3 = new MessageGroup.MessageGroup();
                        temp3.setTimeStamp(Secondary.Msgtime[j]);
                        for (int k = 0; k < 11; k++)
                        {
                            temp3.MesssageID[k] = Secondary.Msgid[j + k];
                            temp3.frame0[k] = Secondary.F0[j + k];
                            temp3.frame1[k] = Secondary.F1[j + k];
                            temp3.frame2[k] = Secondary.F2[j + k];
                            temp3.frame3[k] = Secondary.F3[j + k];
                            temp3.frame4[k] = Secondary.F4[j + k];
                            temp3.frame5[k] = Secondary.F5[j + k];
                            temp3.frame6[k] = Secondary.F6[j + k];
                            temp3.frame7[k] = Secondary.F7[j + k];

                        }
                        grp.Insert(0, temp3);
                        insertcount++;
                        j += 10;
                    }
                }

               
            }
            grp = grp.OrderBy(x => x.timestamp).ToList();
            this.Text = insertcount.ToString();
            //int dd = grp[1].MesssageID[0];

            string[] timestamps = new string[grp.Count];

            //string[] output = new string[grp.Count];

            for (int i = 0; i < grp.Count; i++)
            {
                timestamps[i] = grp[i].timestamp.ToString();
            }

            //this.Text = this.Text + " ts: " + timestamps.Length.ToString();

            if (File.Exists(@"C:\output\test.dat"))
            {
                File.Delete(@"C:\output\test.dat");
            }

            //File.WriteAllLines(@"C:\Users\mhakin.TRIDOMAIN\Desktop\out.txt", timestamps);


            makenewrawfile(grp, @"C:\output\test.dat");
        }

        private void makenewrawfile(List<MessageGroup.MessageGroup> grp, string path)
        {
            using (BinaryWriter writer = new BinaryWriter(File.Open(path, FileMode.Append)))
            {
                for (int i = 0; i < grp.Count - 11; i++)
                {
                    for (int j = 0; j < 11; j++)
                    {
                        UInt32 ts = grp[i].timestamp;
                        UInt16 msgid = (UInt16)grp[i].MesssageID[j];
                        byte f0 = (byte)grp[i].frame0[j];
                        byte f1 = (byte)grp[i].frame1[j];
                        byte f2 = (byte)grp[i].frame2[j];
                        byte f3 = (byte)grp[i].frame3[j];
                        byte f4 = (byte)grp[i].frame4[j];
                        byte f5 = (byte)grp[i].frame5[j];
                        byte f6 = (byte)grp[i].frame6[j];
                        byte f7 = (byte)grp[i].frame7[j];

                        writer.Write(ts);
                        writer.Write(msgid);
                        writer.Write(f0);
                        writer.Write(f1);
                        writer.Write(f2);
                        writer.Write(f3);
                        writer.Write(f4);
                        writer.Write(f5);
                        writer.Write(f6);
                        writer.Write(f7);
                        //writer.Write(grp[i].timestamp);
                        //writer.Write(grp[i + j].MesssageID[j]);
                        //writer.Write(grp[i + j].frame0[j]);
                        //writer.Write(grp[i + j].frame1[j]);
                        //writer.Write(grp[i + j].frame2[j]);
                        //writer.Write(grp[i + j].frame3[j]);
                        //writer.Write(grp[i + j].frame4[j]);
                        //writer.Write(grp[i + j].frame5[j]);
                        //writer.Write(grp[i + j].frame6[j]);

                    }

                }


                writer.Close();
            }
        }

        private List<string> loaddats(DataBinary.RawFile raw, UInt32 timeCutoff)
        {
            //DataBinary.RawFile raw = new DataBinary.RawFile(filesname);
            string[] fn = raw.Filename.Split('\\');
            string outname = "VehicleID- " + fn[fn.Length - 1].Substring(0, 4) + ".csv";

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
            if (File.Exists(outname))
            {
                File.Delete(outname);
            }
            return gaps2;

        }
        private string converttextToHours(string input)
        {
            decimal timee = Convert.ToDecimal(input);
            decimal minutess = timee / 60;
            decimal hours = minutess / 60;

            decimal total = Math.Round(hours, 1);
            return total.ToString() + " Hours";
        }






        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex > -1)
            {
                UInt32 tcut = Convert.ToUInt32(comboBox1.Text);
                List<string> temp = loaddats(raws[listBox1.SelectedIndex], tcut);
                listBox2.DataSource = temp;
            }
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            label2.Text = converttextToHours(comboBox1.Text);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            setitemsForanalysis();
        }

        private void listBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox3.SelectedIndex > -1)
            {
                listBox4.SelectedIndex = listBox3.SelectedIndex;
                textBox1.Text = listBox3.Text;
                textBox2.Text = listBox4.Text;
            }
        }
    }
}
