using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace test
{
    public partial class MakeDataFile : Form
    {
        public MakeDataFile()
        {
            InitializeComponent();
        }
        static double iniTimeStamp = 1627453007;
        private DateTime StartTime;
        private DateTime EndTime;
        private double UnixStart;
        private double UnixEnd;
        private double difference;
        private double measurementcount;
        private UInt32 roundedTotal;
        List<MessageGroup.MessageGroup> MessageList;
        private void MakeDataFile_Load(object sender, EventArgs e)
        {
            dateTimePicker1.Value = DataBinary.UT.UnixTimeStampToDateTime(iniTimeStamp);

            dirListBox1.Path = Application.StartupPath;

            dateTimePicker2.Value = DateTime.Now;

            updateStartAndStop();
        }

        private void makedataFile()
        {

            UInt16[] ids = new UInt16[15];

            

            ids[0] = 500;
            ids[1] = 206;
            ids[2] = 207;
            ids[3] = 202;
            ids[4] = 203;
            ids[5] = 208;
            ids[6] = 201;
            ids[7] = 205;
            ids[8] = 204;
            ids[9] = 200;
            ids[10] = 100;
            ids[11] = 310;
            ids[12] = 110;
            ids[13] = 120;
            ids[14] = 130;

            var rand = new Random();
            MessageList = new List<MessageGroup.MessageGroup>();
            UInt32 timestamp = 0;
            UInt16 msgId = 0;
            byte[] frames = new byte[8];

            for (int i = 0; i < 8; i++)
            {
                frames[i] = 0;
            }
            UInt32 totalHours = (UInt32)numericUpDown1.Value;
            UInt32 totalminutes = (totalHours * 60) + 20;
            UInt32 currentTstamp = (UInt32)UnixStart;
            UInt32 ee = currentTstamp;
            for (UInt32 i = currentTstamp; i < ee + (60 * totalminutes); i+=10)
            {
                
                MessageGroup.MessageGroup temp = new MessageGroup.MessageGroup();
                temp.setTimeStamp(i);


                int trytojump = rand.Next(0, 200);

                if (trytojump == 52)
                {
                    i += 862;
                    ee += 882;
                }



                for (int j = 0; j < 11; j++)
                {
                    temp.SetMessages(ids[j], (byte)rand.Next(0, 255), (byte)rand.Next(0, 255), (byte)rand.Next(0, 255), (byte)rand.Next(0, 255), (byte)rand.Next(0, 255), (byte)rand.Next(0, 255), (byte)rand.Next(0, 255), (byte)rand.Next(0, 255), j);
                }
                MessageList.Add(temp);




            }


        }


        private void writeDatToFile(string path)
        {
            using (BinaryWriter writer = new BinaryWriter(File.Open(path, FileMode.Append)))
            {

                for (int i = 0; i < MessageList.Count; i++)
                {

                    UInt32 ts = (UInt32)MessageList[i].timestamp;

                    for (int j = 0; j < 11; j++)
                    {

                   
                        UInt16 msgid = (UInt16)MessageList[i].MesssageID[j];
                        byte f0 = MessageList[i].frame0[j];
                        byte f1 = MessageList[i].frame1[j];
                        byte f2 = MessageList[i].frame2[j];
                        byte f3 = MessageList[i].frame3[j];
                        byte f4 = MessageList[i].frame4[j];
                        byte f5 = MessageList[i].frame5[j];
                        byte f6 = MessageList[i].frame6[j];
                        byte f7 = MessageList[i].frame7[j];

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
                    }
                }
                writer.Close();
            }

        }


        private void updateStartAndStop()
        {
            StartTime = dateTimePicker1.Value;
            EndTime = dateTimePicker2.Value;
            

            UnixStart = DataBinary.UT.ToUnixTimestamp(dateTimePicker1.Value);
            UnixEnd = DataBinary.UT.ToUnixTimestamp(dateTimePicker2.Value);

            difference = UnixEnd - UnixStart;

            measurementcount = (difference / 10) / 2;
            roundedTotal = (UInt32)(measurementcount);

            

            label1.Text = "Measurement Count: " + roundedTotal.ToString();
            
        }
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            if (dateTimePicker1.Value > dateTimePicker2.Value)
            {
                dateTimePicker2.Value = dateTimePicker1.Value;
            }
            dateTimePicker3.Value = dateTimePicker1.Value;
            updateStartAndStop();
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            //if (dateTimePicker1.Value > dateTimePicker2.Value)
            //{
            //    dateTimePicker2.Value = dateTimePicker1.Value;
            //}
            //dateTimePicker4.Value = dateTimePicker2.Value;
            //updateStartAndStop();

            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (roundedTotal < 6000)
            {

           
            int convert = Convert.ToInt32(UnixStart);
            for (UInt32 i = Convert.ToUInt32(convert); i < (UInt32)UnixEnd; i = i + 10)
            {
                listBox1.Items.Add(i.ToString());
            } 
            
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex > -1)
            {
                int temp = Convert.ToInt32(listBox1.Text);

                this.Text = DataBinary.UT.UnixTimeStampToDateTime(Convert.ToDouble(temp)).ToString();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            makedataFile();
            button3.Enabled = true;
            button2.Enabled = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string[] check = Directory.GetDirectories(Application.StartupPath);
            bool isThere = false;
            for (int i = 0; i < check.Length; i++)
            {
                if (textBox1.Text == check[i] || string.IsNullOrEmpty(textBox1.Text))
                {
                    isThere = true;
                }
            }

            if (!isThere)
            {
                Directory.CreateDirectory(textBox1.Text);
                writeDatToFile(textBox1.Text + "\\5767.dat");
            }

            dirListBox1.Refresh();
        }

        private void dateTimePicker3_ValueChanged(object sender, EventArgs e)
        {
            dateTimePicker1.Value = dateTimePicker3.Value;
            updateStartAndStop();
        }

        private void dateTimePicker4_ValueChanged(object sender, EventArgs e)
        {
            //dateTimePicker2.Value = dateTimePicker4.Value;
        }

        private void dirListBox1_SelectedIndexChanged(object sender, EventArgs e)
        { 
            
            UInt32 time;// = raw.Msgtime[i];
                UInt32 time2; //= raw.Msgtime[i + 1];
            if (File.Exists(dirListBox1.Path + "\\" + dirListBox1.SelectedItem + "\\5767.dat"))
            {
                DataBinary.RawFile raw = new DataBinary.RawFile(dirListBox1.Path + "\\" + dirListBox1.SelectedItem + "\\5767.dat");
                label3.Text = "Start date: " + raw.FileStartDate.ToString();
                label4.Text = "End Date: " + raw.FileEnddate.ToString();
                label5.Text = "Record Count: " + raw.RecordCount.ToString();
                

                int tally = 0;
               
                for (int ip = 0; ip < raw.RecordCount - 1; ip++)
                {
                    int ii = ip;
                    int iii =ip + 1;
                    time = raw.Msgtime[ii];
                    time2 = raw.Msgtime[iii];
                    UInt32 diff = time2 - time;

                    if (diff > 60)
                    {
                        tally++;
                    }

                }
                label6.Text = "Breaks: " + tally.ToString();

            }
            else
            {
                label3.Text = "";
                label4.Text = "";
                label5.Text = "";
                label6.Text = "";
            }
        }
    }
}
