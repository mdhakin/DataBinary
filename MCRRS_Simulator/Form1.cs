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

        bool Inforward;
        UInt32 ct = 782;
        string[] results;
        //string[] hpwaterpressure;
        //string[] hpwaterhydpressure;
        Compare_dataset.Compare_dataset data;
        private void Form1_Load(object sender, EventArgs e)
        {
            
            int[] test = new int[94];

            for (int i = 0; i < test.Length; i++)
            {
                test[i] = i;
            }
            //int[] hpwaterpsi = new int[1];
            //int[] hpwaterhydpsi = new int[1];

            test[0] = 43;
            //hpwaterpsi[0] = 6;
            //hpwaterhydpsi[0] = 5;
            data = new Compare_dataset.Compare_dataset(@"C:\output\5590\5590.dat", test);
            results = data.getResult();
            Inforward = false; // 42

            //Compare_dataset.Compare_dataset hpwater1 = new Compare_dataset.Compare_dataset(@"C:\output\5590\5590.dat", hpwaterpsi);

            //hpwaterpressure = hpwater1.getResult();
            File.WriteAllLines(@"all.csv", results);
            

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //string[] parts = results[ct].Split('|');
            //label1.Text = DataBinary.UT.UnixTimeStampToDateTime(Convert.ToDouble(parts[0])).ToString();
            //string[] hpwaterparts = hpwaterpressure[ct].Split('|');
            //if(parts[1] == "1")
            //{
            //    pictureBox2.Visible = true;
            //}else
            //{
            //    pictureBox2.Visible = false;
            //}
            //label2.Text = "HP water Pressue " + hpwaterparts[1] + " psi";
            ct++;
        }
    }

    

}
