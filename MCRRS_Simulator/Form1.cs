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

        string[] aDeck_float;
        string[] aDeck_Lock;

        string[] aVac_rpm;
        string sVacuumRpm = "Blower RPM: ";

        string[] aEnginerpm; // 0
        string sEngineRpmStr = "Engine Rpm: ";

        string[] open_loop_psi; //3

        string[] hp_water_psi;
        string sHP_Water_PSIStr = "HP water psi: ";
        string[] fwd_on1; // 43
        string fwd_on_str = "Vehicle in forward";

        string[] neutal_on1;
        string neutral_on_str = "Vehicle in neutral";

        string[] rev_on1;


        string filePath = "";
        
        private void Form1_Load(object sender, EventArgs e)
        {
            string[] args = Environment.GetCommandLineArgs();
            
            if(args.Length > 1)
            {
                string nargs = args[0];
                if (File.Exists(args[0]))
                {
                    filePath = args[0];
                }else
                {
                    filePath = "5497.dat";
                }
            }else
            {
                filePath = "5497.dat";
            }
            comboBox1.SelectedIndex = 5;
            this.Text = filePath;
            // raw = new DataBinary.RawFile(filePath);
            position();
            setup();
            
        }

        private void setup()
        {
            fwd_on1 = getReading(43);
            neutal_on1 = getReading(45);
            rev_on1 = getReading(44);
            hp_water_psi = getReading(6);
            open_loop_psi = getReading(3);
            aEnginerpm = getReading(0);
            aVac_rpm = getReading(12);
            aDeck_float = getReading(81);
            aDeck_Lock = getReading(82);
            currenttimestampIndx = 0;
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

        private void position()
        {
            console.Left = 143;
            console.Top = 310;

            fwd_on.Left = 204;
            fwd_on.Top = 335;

            neutral_on.Left = 259;
            neutral_on.Top = fwd_on.Top;

            rev_on.Left = 314;
            rev_on.Top = fwd_on.Top;

            lblVac_console_on.Left = fwd_on.Left;
            lblVac_console_on.Top = 315;

            pDeck_Float.Left = 369;
            pDeck_Float.Top = fwd_on.Top;

            pDeck_Lock.Top = lblVac_console_on.Top;
            pDeck_Lock.Left = pDeck_Float.Left;
        }



        private void timer1_Tick(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex > 0)
            {
                string sInterval = comboBox1.Text;
                int iInterval = Convert.ToInt32(sInterval);
                timer1.Interval = iInterval;
            }
            

            if (currenttimestampIndx == fwd_on1.Length)
            {
                currenttimestampIndx = 0;
            }
            string[] fwd_Parts_on = fwd_on1[currenttimestampIndx].Split('|');
            if (fwd_Parts_on[1] == "1")
            {
                fwd_on.Visible = true;
            } else
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

            string[] deck_float_parts = aDeck_float[currenttimestampIndx].Split('|');
            if (deck_float_parts[1] == "1")
            {
                pDeck_Float.Visible = true;
            }
            else
            {

                pDeck_Float.Visible = false;
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

            string[] Deck_lock_parts = aDeck_Lock[currenttimestampIndx].Split('|');
            if (Deck_lock_parts[1] == "1")
            {
                pDeck_Lock.Visible = true;
            }
            else
            {

                pDeck_Lock.Visible = false;
            }


            string[] hp_water_parts = open_loop_psi[currenttimestampIndx].Split('|');
            lblOpenLoop.Text = "Open Loop Pressure: " + hp_water_parts[1];
            label1.Text = DataBinary.UT.UnixTimeStampToDateTime(Convert.ToDouble(hp_water_parts[0])).ToString();


            string[] hp_water_psi_parts = hp_water_psi[currenttimestampIndx].Split('|');
            label2.Text = sHP_Water_PSIStr + hp_water_psi_parts[1];

            string[] Engine_Rpm_parts = aEnginerpm[currenttimestampIndx].Split('|');
            lblEngingRpm.Text = sEngineRpmStr + Engine_Rpm_parts[1] + " RPM";

            string[] Vac_RPM_parts = aVac_rpm[currenttimestampIndx].Split('|');
            lblVacRPM.Text = sVacuumRpm + Vac_RPM_parts[1] + " RPM";

            lblChangeIndex.Text = "Index " + currenttimestampIndx + " of " + aVac_rpm.Length.ToString(); ;
            currenttimestampIndx++;

        }

        private void btnChangeIndex_Click(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty(txtCurrentIndex.Text) && Convert.ToInt32(txtCurrentIndex.Text) > -1 && Convert.ToInt32(txtCurrentIndex.Text) < aVac_rpm.Length)
            {
                currenttimestampIndx = Convert.ToInt32(txtCurrentIndex.Text);
            }
        }
    }

    

}
