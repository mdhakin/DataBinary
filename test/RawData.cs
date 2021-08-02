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
using DataBinary;
namespace test
{
    public partial class RawData : Form
    {
        public string CurrentFile { get => currentfile; }
        private string currentfile = "";
        private IRawFile raw;
        Settings set;
        public RawData()
        {
            InitializeComponent();
        }

        private void RawData_Load(object sender, EventArgs e)
        {
            set = new Settings();

            of.InitialDirectory = set.lastRawfilePath;
        }

        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            of.ShowDialog();

            if (!string.IsNullOrEmpty(of.FileName))
            {
                if (File.Exists(of.FileName))
                {
                    this.currentfile = of.FileName;
                    raw = Factory.CreateRawFile(this.currentfile);

                    if (raw.FileReady)
                    {
                        btnOpenFile.Enabled = false;
                        listBox1.Items.Clear();
                        listBox1.Items.Add("File: " + raw.Filename);
                        listBox1.Items.Add("Start Date: " + raw.FileStartDate);
                        listBox1.Items.Add("End Date:" + raw.FileEnddate);
                        listBox1.Items.Add("Number of records: " + raw.RecordCount.ToString());
                        listBox1.Items.Add("Size: " + raw.sizeInBytes().ToString() + " bytes");
                        if (raw.checkData() == 0)
                        {
                            listBox1.Items.Add("Data is consistant.");
                        }else
                        {
                            listBox1.Items.Add("Data appears to contain errors");
                        }
                        listBox1.Items.Add(    ((raw.TotalHours / 60) / 60).ToString() + " Hours."          );
                        string[] parts = raw.Filename.Split('\\');
                        string file = parts[parts.Length - 1];
                        listBox1.Items.Add("FileName: " + file);
                        string softwareID = file.Substring(0, 4);
                        listBox1.Items.Add("SoftwareID: " + softwareID);

                        MCRRS_LIST.mcrrs mc = new MCRRS_LIST.mcrrs();
                        listBox1.Items.Add("USN Number: " + mc.getUSNfromID(softwareID));

                        // backgroundWorker1.RunWorkerAsync()
                        // Start BackgroundWorker
                        //backgroundWorker1.RunWorkerAsync(2000);
                        loadreadings();
                    }
                }
            }
        }

        private void loadreadings()
        {
            // 

            listBox2.Items.Clear();

            string[] radings = new string[raw.RecordCount];

            long tempcount = raw.RecordCount;

            if (tempcount > 199)
            {
                tempcount = 100;
            }

            for (int i = 0; i < tempcount; i++)
            {
                radings[i] = (i + 1).ToString() + "\t" + UT.UnixTimeStampToDateTime( Convert.ToDouble(raw.Msgtime[i])) + "\t" + raw.Msgid[i] + "\t" + raw.F0[i] + "\t" + raw.F1[i] + "\t" + raw.F2[i] + "\t" + raw.F3[i] + "\t" + raw.F4[i] + "\t" + raw.F5[i] + "\t" + raw.F6[i] + "\t" + raw.F7[i];
            
            }
            listBox2.DataSource = radings;
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            //loadreadings();
        }

        private void RawData_ForeColorChanged(object sender, EventArgs e)
        {

        }

        private void RawData_FormClosing(object sender, FormClosingEventArgs e)
        {
            set.saveRawFileLastPath(of.FileName);
        }
    }
}
