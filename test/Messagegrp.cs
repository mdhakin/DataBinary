using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MessageGroup;
namespace test
{
    public partial class Messagegrp : Form
    {
        public Messagegrp()
        {
            InitializeComponent();
        }
        
        
        int ListIndx;
        List<MessageGroup.MessageGroup> MessageList = new List<MessageGroup.MessageGroup>();
        UInt32 currentTstamp;
        UInt32 initTimestamp;
        private void Messagegrp_Load(object sender, EventArgs e)
        {
            var rand = new Random();

            currentTstamp = 1527507007;
            initTimestamp = 1527507007;



            for (UInt32 i = 0; i < 1000; i++)
            {
                MessageGroup.MessageGroup temp = new MessageGroup.MessageGroup();
                temp.setTimeStamp(currentTstamp + (UInt32)10);
                currentTstamp = currentTstamp + (UInt32)10;
                
                for (int j = 0; j < temp.getMeassageSixe(); j++)
                {
                    temp.SetMessages((byte)rand.Next(0, 255), (byte)rand.Next(0, 255), (byte)rand.Next(0, 255), (byte)rand.Next(0, 255), (byte)rand.Next(0, 255), (byte)rand.Next(0, 255), (byte)rand.Next(0, 255), (byte)rand.Next(0, 255), (byte)rand.Next(0, 255), j);
                }
                MessageList.Add(temp);




            }

            numericUpDown1.Maximum = MessageList.Count - 1;

            label1.Text = "Number of readings: " + MessageList.Count.ToString();


            double timelapse = (double)(currentTstamp - initTimestamp);

            double minutes = timelapse / 60;

            double hours = minutes / 60;

            double days = (double)(hours / 24);

            this.Text = days.ToString() + " Days";
            
        }


        private void listValues()
        {
            for (int i = 0; i < MessageList.Count; i++)
            {
                string ddate = DataBinary.UT.UnixTimeStampToDateTime((double)MessageList[i].timestamp).ToString();

                for (int cnt = 0; cnt < MessageList[i].getMeassageSixe(); cnt++)
                {
                    listBox3.Items.Add("TS: " + MessageList[i].timestamp + "  HumanReadable: " + ddate + "  " + ",id: " + MessageList[i].MesssageID[cnt].ToString() + ", Frames: " + MessageList[i].frame0[cnt].ToString() + "," + MessageList[i].frame1[cnt].ToString() + "," + MessageList[i].frame2[cnt].ToString() + "," + MessageList[i].frame3[cnt].ToString() + "," + MessageList[i].frame4[cnt].ToString() + "," + MessageList[i].frame5[cnt].ToString() + "," + MessageList[i].frame6[cnt].ToString() + "," + MessageList[i].frame7[cnt].ToString());
                }

            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            ListIndx = (int)numericUpDown1.Value; ;
            listBox1.Items.Clear();
            int[] taken = MessageList[ListIndx].ListTakenMessages();

            for (int i = 0; i < MessageList[0].getMeassageSixe(); i++)
            {
                listBox1.Items.Add("Message filled " + taken[i].ToString());
            }


           
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListIndx = (int)numericUpDown1.Value;
            if (listBox1.SelectedIndex > -1)
            {
                if (listBox1.Text == "Message filled 1")
                {
                    listBox2.Items.Clear();

                    
                    string date = DataBinary.UT.UnixTimeStampToDateTime(Convert.ToDouble(MessageList[ListIndx].timestamp)).ToString();

                    listBox2.Items.Add("TimeStamp: " + MessageList[ListIndx].timestamp.ToString());
                    listBox2.Items.Add("Human Readable Date: " + date.ToString());
                    listBox2.Items.Add("**********************");
                    listBox2.Items.Add("Message ID: " + MessageList[ListIndx].MesssageID[listBox1.SelectedIndex].ToString());
                    listBox2.Items.Add("Frame 0: " + MessageList[ListIndx].frame0[listBox1.SelectedIndex].ToString());
                    listBox2.Items.Add("Frame 1: " + MessageList[ListIndx].frame1[listBox1.SelectedIndex].ToString());
                    listBox2.Items.Add("Frame 2: " + MessageList[ListIndx].frame2[listBox1.SelectedIndex].ToString());
                    listBox2.Items.Add("Frame 3: " + MessageList[ListIndx].frame3[listBox1.SelectedIndex].ToString());
                    listBox2.Items.Add("Frame 4: " + MessageList[ListIndx].frame4[listBox1.SelectedIndex].ToString());
                    listBox2.Items.Add("Frame 5: " + MessageList[ListIndx].frame5[listBox1.SelectedIndex].ToString());
                    listBox2.Items.Add("Frame 6: " + MessageList[ListIndx].frame6[listBox1.SelectedIndex].ToString());
                    listBox2.Items.Add("Frame 7: " + MessageList[ListIndx].frame7[listBox1.SelectedIndex].ToString());

                }else
                {
                    listBox2.Items.Clear();
                }
            }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            ListIndx = (int)numericUpDown1.Value;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            listValues();
        }
    }
}
