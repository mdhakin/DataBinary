using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace MCRRS_Simulator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        int currenttimestampIndx;
        string[] allreadings;

        string[] open_loop_psi; //3

        string[] hp_water_psi;

        string[] fwd_on1; // 43
        string fwd_on_str = "Vehicle in forward";

        string[] neutal_on1;
        string neutral_on_str = "Vehicle in neutral";

        string[] rev_on1;
        

        string filePath = @"C:\output\5590\5590.dat";
        //DataBinary.RawFile raw;
        private void Form1_Load(object sender, EventArgs e)
        {

            // raw = new DataBinary.RawFile(filePath);

            setup();
        }

        private void setup()
        {
            fwd_on1 = getReading(43);
            neutal_on1 = getReading(45);
            rev_on1 = getReading(44);
            hp_water_psi = getReading(6);
            open_loop_psi = getReading(3);
            currenttimestampIndx = 10000;
        }

        private string[] getReading(int indx)
        {
            int[] tempindx = new int[1];
            tempindx[0] = indx;
            string[] output;
            Compare_dataset.Compare_dataset data = new Compare_dataset.Compare_dataset(filePath, tempindx);
            output = data.getResult();
            return output;
        }

        private void makeReadingsList()
        {
            int[] ids = new int[95];
            for (int i = 0; i < ids.Length; i++)
            {
                if (i == 13 || i == 15)
                {
                    i = 1;
                }
                else
                {
                    ids[i] = i;
                }
            }
            Compare_dataset.Compare_dataset data = new Compare_dataset.Compare_dataset(filePath, ids);
            allreadings = data.getResult();

        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            
            if (currenttimestampIndx == fwd_on1.Length)
            {
                currenttimestampIndx = 0;
            }
            string[] fwd_Parts_on = fwd_on1[currenttimestampIndx].Split('|');
            if (fwd_Parts_on[1] == "1")
            {
                fwd_on.Visible = true;
            }else
            {
                fwd_on.Visible = false;
            }

            string[] n_on = neutal_on1[currenttimestampIndx].Split('|');
            if (n_on[1] == "1")
            {
                neutral_on.Visible = true;
            }
            else
            {

                neutral_on.Visible = false;
            }

            string[] rev_Parts_on = rev_on1[currenttimestampIndx].Split('|');
            if (rev_Parts_on[1] == "1")
            {
                rev_on.Visible = true;
            }
            else
            {

                rev_on.Visible = false;
            }


            string[] hp_water_parts = open_loop_psi[currenttimestampIndx].Split('|');
            lblOpenLoop.Text = "Open Loop Pressure: " + hp_water_parts[1];
            label1.Text = DataBinary.UT.UnixTimeStampToDateTime(Convert.ToDouble(hp_water_parts[0])).ToString();


            string[] hp_water_psi_parts = hp_water_psi[currenttimestampIndx].Split('|');
            label2.Text = "HP water psi: " + hp_water_psi_parts[1];
            currenttimestampIndx++;

        }
    }

    

}
