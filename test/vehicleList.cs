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

namespace test
{
    public partial class vehicleList : Form
    {
        public vehicleList()
        {
            InitializeComponent();
        }

        private void vehicleList_Load(object sender, EventArgs e)
        {
            string[] vList = File.ReadAllLines("vehicles.ini");

            listBox1.Items.Add("ID \t\t USN");
            for (int i = 0; i < vList.Length; i++)
            {
                string[] parts = vList[i].Split('|');


                listBox1.Items.Add(parts[0] + "\t\t" + parts[1]);
            }

        }
    }
}
