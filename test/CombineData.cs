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
                        RunCombine3(raws[selectedItems[0]], raws[selectedItems[1]]);
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

        private void RunCombine3(DataBinary.RawFile raw1, DataBinary.RawFile raw2)
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

            UInt32[] gapstarts = new UInt32[gaps3.Count - 5];
            UInt32[] gapends = new UInt32[gaps3.Count - 5];

            for (int i = 5; i < gaps3.Count; i++)
            {
                string[] parts = gaps3[i].Split(',');

                gapstarts[i - 5] = Convert.ToUInt32(parts[2]);
                gapends[i - 5] = Convert.ToUInt32(parts[4]);
            }

            UInt32 lastPrimaryIndex = 0;
            UInt32 lastSecondaryIndex = 0;
            UInt32 lastnewIndex = 0;
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
                        //lastSecondaryIndex++;
                    }
                }

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


                }



            }




            //go to the next gap and start again

            writeOutputFile(t, @"C:\output\5871\5871.dat", lastnewIndex);


            watch.Stop();
            MessageBox.Show(watch.ElapsedMilliseconds.ToString() + " ms");
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

        private void button2_Click(object sender, EventArgs e)
        {
            DataBinary.CreateDataset data = new DataBinary.CreateDataset(1500000000,330,7);
            data.CreateDatasetFor(Application.StartupPath + "\\output\\6459\\6459.dat",255);

            DataBinary.CreateDataset data2 = new DataBinary.CreateDataset(1500000061, 330, 7);
            data2.CreateDatasetFor(Application.StartupPath + "\\output\\6511\\6511.dat",155);
        }
    }
}
