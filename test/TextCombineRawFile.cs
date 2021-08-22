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
    public partial class TextCombineRawFile : Form
    {
        enum SelectState
        {
            none,
            one,
            two,
            loaded
        }
        public TextCombineRawFile()
        {
            InitializeComponent();
        }
        SelectState state;
        public TextCombineRawFile(string in1, string in2)
        {
            this.inputFileOne = in1;
            this.inputFileTwo = in2;
            state = SelectState.none;
            InitializeComponent();

        }
        private void updateview()
        {
            if (state == SelectState.none)
            {
                toolStripButton1.Enabled = true;
                toolStripButton2.Enabled = false;
                toolStripButton4.Enabled = false;
                toolStripTextBox1.Text = "";
                toolStripTextBox2.Text = "";
                this.inputFileOne = "";
                this.inputFileTwo = "";
                DG1.DataSource = null;
                dg2.DataSource = null;
                dg3.DataSource = null;

                label2.Text = "";
                label3.Text = "";
                label5.Text = "";


            }
            else if (state == SelectState.one)
            {
                toolStripButton1.Enabled = false;
                toolStripButton2.Enabled = true;
                toolStripButton4.Enabled = false;
            }
            else if(state == SelectState.two)
            {
                toolStripButton1.Enabled = false;
                toolStripButton2.Enabled = false;
                toolStripButton4.Enabled = true;
            }
            else if (state == SelectState.loaded)
            {
                toolStripButton1.Enabled = false;
                toolStripButton2.Enabled = false;
                toolStripButton4.Enabled = false;
            }
        }
        public void con()
        {
            RawFile DataOne = new RawFile(this.inputFileOne);
            RawFile DataTwo = new RawFile(this.inputFileTwo);

            if (DataOne.FileReady && DataTwo.FileReady)
            {
                CombineRawFiles combine = new CombineRawFiles();

                DataTable messages = combine.Combine_returnDataTable(DataOne, DataTwo, 3000);
                DG1.DataSource = messages;

                DataTable inOne = convertRawToDT(DataOne);
                DataTable inTwo = convertRawToDT(DataTwo);

                dg2.DataSource = inOne;
                dg3.DataSource = inTwo;

                label2.Text = messages.Rows.Count.ToString();
                label3.Text = inOne.Rows.Count.ToString();
                label5.Text = inTwo.Rows.Count.ToString();

                label8.Text = "Output";
                label7.Text = DataOne.Filename;
                label9.Text = DataTwo.Filename;

            }
        }

        private DataTable convertRawToDT(RawFile raw)
        {
            

            DataTable ddt = new DataTable();

            ddt.Columns.Add("TimeStamp", typeof(UInt32));
            ddt.Columns.Add("MessageID", typeof(UInt16));
            ddt.Columns.Add("f0", typeof(byte));
            ddt.Columns.Add("f1", typeof(byte));
            ddt.Columns.Add("f2", typeof(byte));
            ddt.Columns.Add("f3", typeof(byte));
            ddt.Columns.Add("f4", typeof(byte));
            ddt.Columns.Add("f5", typeof(byte));
            ddt.Columns.Add("f6", typeof(byte));
            ddt.Columns.Add("f7", typeof(byte));

            for (int i = 0; i < raw.RecordCount; i++)
            {
                DataRow dr = ddt.NewRow();
                dr["TimeStamp"] = raw.Msgtime[i];
                dr["MessageID"] = raw.Msgid[i];
                dr["f0"] = raw.F0[i];
                dr["f1"] = raw.F1[i];
                dr["f2"] = raw.F2[i];
                dr["f3"] = raw.F3[i];
                dr["f4"] = raw.F4[i];
                dr["f5"] = raw.F5[i];
                dr["f6"] = raw.F6[i];
                dr["f7"] = raw.F7[i];

                ddt.Rows.Add(dr);
            }


            return ddt;
        }

        public string inputFileOne { get; set; }
        public string inputFileTwo { get; set; }

        
        private void TextCombineRawFile_Load(object sender, EventArgs e)
        {
            resizeShit();
            updateview();
        }

        private void TextCombineRawFile_DoubleClick(object sender, EventArgs e)
        {
            //con();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            of1.InitialDirectory = Application.StartupPath;
            string sFile = "";
            of1.Filter = "dat files|*.dat";
            of1.ShowDialog();
            sFile = of1.FileName;
            if (File.Exists(sFile))
            {
                toolStripTextBox1.Text = sFile;
                state = SelectState.one;
                this.inputFileOne = sFile;
                updateview();
            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            state = SelectState.none;
            updateview();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            of1.InitialDirectory = Application.StartupPath;
            string sFile = "";
            of1.Filter = "dat files|*.dat";
            of1.ShowDialog();
            sFile = of1.FileName;
            if (File.Exists(sFile))
            {
                toolStripTextBox2.Text = sFile;
                state = SelectState.two;
                this.inputFileTwo = sFile;
                updateview();
            }
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            con();
        }

        private void TextCombineRawFile_Resize(object sender, EventArgs e)
        {

            resizeShit();
        }

        private void resizeShit()
        {
            DG1.Height = ((this.Height - 150) / 3);
            panel1.Height = DG1.Height;
            panel1.Top = DG1.Top;
            
            
            dg2.Height = DG1.Height + 20;
            panel2.Height = dg2.Height;
            dg2.Top = DG1.Top + DG1.Height + 10;
            panel2.Top = dg2.Top;

            dg3.Height = DG1.Height + 20;
            dg3.Top = dg2.Top + dg2.Height + 10;
            panel3.Top = dg3.Top;
            panel3.Height = dg3.Height;

        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
}
