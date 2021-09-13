using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataBinary;
namespace test
{
    public partial class GapVisualize : Form
    {
        public GapVisualize()
        {
            InitializeComponent();
        }
        const string f1 = @"C:\Users\mhakin.TRIDOMAIN\Desktop\1.dat";
        const string f2 = @"C:\Users\mhakin.TRIDOMAIN\Desktop\2.dat";
        const string f3 = @"C:\Users\mhakin.TRIDOMAIN\Desktop\3.dat";
        const string f4 = @"C:\Users\mhakin.TRIDOMAIN\Desktop\4.dat";
        const string f5 = @"C:\Users\mhakin.TRIDOMAIN\Desktop\5.dat";
        const string f6 = @"C:\Users\mhakin.TRIDOMAIN\Desktop\6.dat";
        string dumpstring = "";
        public UInt32[] begins { get; set; }
        public UInt32[] ends { get; set; }
        public UInt32 FileStart { get; set; }
        public UInt32 FileEnd { get; set; }
        public int currentwidth { get; set; }
        private void GapVisualize_Load(object sender, EventArgs e)
        {
            //this.currentwidth = 0;
            textBox1.Text = f5;
            textBox2.Text = f6;
            
        }

        private void loadit(string file1, string file2)
        {
            panel1.Controls.Clear();
            panel2.Controls.Clear();
            
            UInt32 size = loadFile(file1, 300);

            this.currentwidth = (int)((this.FileStart - 1500000000) / 100);
            dumpstring = size.ToString();
            this.Text = dumpstring;


            Color cl = Color.Lime;
            Color cl2 = Color.Red;

            
            float twidth2 = (float)(this.begins[0] - this.FileStart) / 100;

            loadPB2(panel1, twidth2, cl);

            twidth2 = ((float)(this.ends[0] - this.begins[0]) / 100);
            loadPB2(panel1, twidth2, cl2);


            twidth2 = ((float)(this.begins[1] - this.ends[0]) / 100);
            loadPB2(panel1, twidth2, cl);


            
            twidth2 = ((float)(this.ends[1] - this.begins[1]) / 100);
            loadPB2(panel1, twidth2, cl2);


            
            twidth2 = ((float)(this.FileEnd - this.ends[1]) / 100);
            loadPB2(panel1, twidth2, cl);


            
            size = loadFile(file2, 300);
            this.currentwidth = (int)((this.FileStart - 1500000000) / 100);

            dumpstring = size.ToString();
            this.Text = dumpstring;




            
            twidth2 = ((float)(this.begins[0] - this.FileStart) / 100);
            loadPB2(panel2, twidth2, cl);


            
            twidth2 = ((float)(this.ends[0] - this.begins[0]) / 100);
            loadPB2(panel2, twidth2, cl2);

            
            twidth2 = ((float)(this.begins[1] - this.ends[0]) / 100);
            loadPB2(panel2, twidth2, cl);


            
            twidth2 = ((float)(this.ends[1] - this.begins[1]) / 100);
            loadPB2(panel2, twidth2, cl2);


            
            twidth2 = ((float)(this.FileEnd - this.ends[1]) / 100);
            loadPB2(panel2, twidth2, cl);
        }







        private void loadit2(string file1, string file2)
        {
            panel1.Controls.Clear();
            panel2.Controls.Clear();

            UInt32 size = loadFile(file1, 300);

            this.currentwidth = (int)((this.FileStart - 1500000000) / 100);
            dumpstring = size.ToString();
            this.Text = dumpstring;


            Color cl = Color.Lime;
            Color cl2 = Color.Red;


            float twidth2 = (float)(this.begins[0] - this.FileStart) / 100;

            loadPB2(panel1, twidth2, cl);
            int ct = 0;
            
            for (int i = 0; i < this.begins.Length - 1; i++)
            {

                twidth2 = ((float)(this.ends[i] - this.begins[i]) / 100);
                loadPB2(panel1, twidth2, cl2);

                twidth2 = ((float)(this.begins[i + 1] - this.ends[i]) / 100);
                loadPB2(panel1, twidth2, cl);
                ct = i;
            }

            ct++;

            twidth2 = ((float)(this.ends[ct] - this.begins[ct]) / 100);
            loadPB2(panel1, twidth2, cl2);



            twidth2 = ((float)(this.FileEnd - this.ends[ct]) / 100);
            loadPB2(panel1, twidth2, cl);







            size = loadFile(file2, 300);
            this.currentwidth = (int)((this.FileStart - 1500000000) / 100);

            dumpstring = size.ToString();
            this.Text = dumpstring;


            twidth2 = (float)(this.begins[0] - this.FileStart) / 100;

            loadPB2(panel2, twidth2, cl);
            ct = 0;

            for (int i = 0; i < this.begins.Length - 1; i++)
            {

                twidth2 = ((float)(this.ends[i] - this.begins[i]) / 100);
                loadPB2(panel2, twidth2, cl2);

                twidth2 = ((float)(this.begins[i + 1] - this.ends[i]) / 100);
                loadPB2(panel2, twidth2, cl);
                ct = i;
            }

            ct++;

            twidth2 = ((float)(this.ends[ct] - this.begins[ct]) / 100);
            loadPB2(panel2, twidth2, cl2);



            twidth2 = ((float)(this.FileEnd - this.ends[ct]) / 100);
            loadPB2(panel2, twidth2, cl);


        }







        private UInt32 loadFile(string sPath, UInt32 interval)
        {

            DataBinary.RawFile raw = new DataBinary.RawFile(sPath);
            CombineRawFiles combine = new CombineRawFiles();

            List<string> gaps1 = combine.loaddats(ref raw, interval);

            string[] Gaps1Array = new string[gaps1.Count - 5];

            for (int i = 0; i < Gaps1Array.Length; i++)
            {
                Gaps1Array[i] = gaps1[i + 5];
            }

            // List of gaps in the primary file
            UInt32[] gapstarts = new UInt32[gaps1.Count - 5];
            UInt32[] gapends = new UInt32[gaps1.Count - 5];


            // fill gap info
            for (int i = 5; i < gaps1.Count; i++)
            {
                string[] parts = gaps1[i].Split(',');

                gapstarts[i - 5] = Convert.ToUInt32(parts[2]);
                gapends[i - 5] = Convert.ToUInt32(parts[4]);
            }

            this.begins = gapstarts;
            this.ends = gapends;

            UInt32 fileStart = raw.Msgtime[0];
            this.FileStart = fileStart;
            long rawlen = raw.RecordCount - 1;

            UInt32 fileEnd = raw.Msgtime[rawlen];
            this.FileEnd = fileEnd;

            UInt32 offset = 1500000000;
            

            return (fileEnd - offset);
        }

        private void loadPB(Panel pan, float count, Color cl)
        {
            float thewidth = ((float)panel1.Width * count);

            PictureBox pb = new PictureBox();
            pb.BackColor = cl;
            pb.Height = pan.Height;
            if (thewidth < 1)
            {
                thewidth = 1;
            }
            pb.Width = (int)thewidth;

            pb.Left = this.currentwidth;
            pan.Controls.Add(pb);
            this.currentwidth = this.currentwidth + pb.Width;
        }

        private void loadPB2(Panel pan, float count, Color cl)
        {
            PictureBox pb = new PictureBox();
            pb.BackColor = cl;
            pb.Height = pan.Height;
            int ww = (int)count;
            if (ww < 1)
            {
                ww = 1;
            }
            pb.Width = ww;
            pb.Left = this.currentwidth;
            pan.Controls.Add(pb);
            this.currentwidth = pb.Left + pb.Width;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!checkBox1.Checked)
            {
                loadit(textBox1.Text, textBox2.Text);
            }else
            {
                loadit2(textBox1.Text, textBox2.Text);
            }
            
        }
    }
}
