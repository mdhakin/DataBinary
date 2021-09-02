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

        string[] allreadings;
        string filePath = @"C:\output\5590\5590.dat";
        private void Form1_Load(object sender, EventArgs e)
        {

           



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
            
        }
    }

    

}
