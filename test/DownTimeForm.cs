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
    public partial class DownTimeForm : Form
    {
        public string filename { get => mFileName; }
        private string mFileName;
        DataTable dt;
        public DownTimeForm(string fName)
        {
            InitializeComponent();
            mFileName = fName;
            dt = new DataTable("file");

            dt.Columns.Add("seconds", typeof(string));
            dt.Columns.Add("IdleStart", typeof(string));
            dt.Columns.Add("UnixStart", typeof(string));
            dt.Columns.Add("IdleEnd", typeof(string));
            dt.Columns.Add("UnixEnd", typeof(string));
            dt.Columns.Add("minutes", typeof(string));
            dt.Columns.Add("hours", typeof(string));
            dt.Columns.Add("days", typeof(string));
        }

        private void DownTimeForm_Load(object sender, EventArgs e)
        {
            string[] loadfile = File.ReadAllLines(this.mFileName);

            string[] lab1 = loadfile[1].Split(',');

            label1.Text = lab1[0];

            string[] lab2 = loadfile[2].Split(',');


            label2.Text = lab2[0];
            for (int i = 5; i < loadfile.Length; i++)
            {
                string[] items = loadfile[i].Split(',');
                dt.Rows.Add(items);
            }

            DG.DataSource = dt;
        }
    }
}
