using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.IO;

namespace test
{
    public partial class Graph_on_off : Form
    {
        List<string> gaps2;
        //List<bool> timeline;//= new List<bool>();
        //List<UInt32> timestamps;// = new List<UInt32>();
        DataBinary.RawFile raw;
        string outname;
        string[] gaps;
        public Graph_on_off()
        {
            InitializeComponent();
        }

        private void Graph_on_off_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 4;
            OF.InitialDirectory = Application.StartupPath;
            label2.Text = "";
            decimal timee = Convert.ToDecimal(comboBox1.Text);
            decimal minutess = timee / 60;
            decimal hours = minutess / 60;

            decimal total = Math.Round(hours, 1);
            label2.Text = total.ToString() + " Hours";
            // raw = new DataBinary.RawFile("");
        }

        void chartload(int ilen)
        {

            this.ch2.Series.Clear();
            this.ch2.Titles.Clear();
            Series series = ch2.Series.Add("LP Air");
            series.ChartType = SeriesChartType.Line;
            series.Color = Color.Lime;
            series.BorderWidth = 3;


            // The main back color
            ch2.ChartAreas[0].BackColor = Color.Black;

            // So I can adjust the margins
            ch2.ChartAreas[0].InnerPlotPosition.Auto = false;
            ch2.ChartAreas[0].InnerPlotPosition.X = 1;
            ch2.ChartAreas[0].InnerPlotPosition.Y = 0;
            ch2.ChartAreas[0].InnerPlotPosition.Width = 99;

            // How for away are the plot ticks on the x axis
            ch2.ChartAreas[0].Axes[0].Interval = 15;

            ch2.ChartAreas[0].Axes[1].LineColor = Color.White;
            ch2.ChartAreas[0].Axes[2].LineColor = Color.White;
            ch2.ChartAreas[0].Axes[3].LineColor = Color.White;
            ch2.ChartAreas[0].Axes[0].MajorGrid.LineColor = Color.Red;
            ch2.ChartAreas[0].Axes[1].MajorGrid.LineColor = Color.Red;
            ch2.ChartAreas[0].Axes[0].MajorTickMark.LineColor = Color.Red;
            ch2.ChartAreas[0].Axes[1].MajorTickMark.LineColor = Color.Red;
            ch2.ChartAreas[0].Axes[0].LabelStyle.ForeColor = Color.Lime;
            ch2.ChartAreas[0].Axes[1].LabelStyle.ForeColor = Color.Lime;

            // Backcolor of the chart area
            ch2.BackColor = Color.Black;


            for (int i = 0; i < ilen; i++)
            {
                //series.Points.AddXY(xax[i], yax[i]);
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            OF.ShowDialog();

            if (!(string.IsNullOrEmpty( OF.FileName)))
            {
                if (File.Exists(OF.FileName))
                {
                    loaddats(OF.FileName);

                }
            }

        }


        private void loaddats(string filesname)
        {
            raw = new DataBinary.RawFile(filesname);
            string[] fn = OF.FileName.Split('\\');
            outname = "VehicleID- " + fn[fn.Length - 1].Substring(0, 4) + ".csv";
            UInt32 timeCutoff = Convert.ToUInt32(comboBox1.Text);
            gaps = new string[raw.RecordCount];
            gaps2 = new List<string>();

            MCRRS_LIST.mcrrs mccris = new MCRRS_LIST.mcrrs();
            string usn = mccris.getUSNfromID(fn[fn.Length - 1].Substring(0, 4));
            string headder1 = "USN Number: " + usn + ", Data File Start Date:" + raw.FileStartDate.ToString() + ", Data File End Date: "+ raw.FileEnddate.ToString() +",,,,,";
            string headder2 = "***************************************************************************************************************";
            string infoheader = "Time Cutoff=" + timeCutoff.ToString() + " In seconds, Downtime Anaysis File Creation Date: " + DateTime.Now.ToString() + ",,,,,,";
            string headder = "SECONDS, START OF IDLE, UNIX TS,END OF IDLE,UNIX TS,MINUTES,HOURS,DAYS";
            string titleHeader = "MCRRS Downtime Report.";

            gaps2.Add(titleHeader);
            gaps2.Add(headder1);
            gaps2.Add(infoheader);
            gaps2.Add(headder2);
            gaps2.Add(headder);
            for (int i = 0; i < raw.RecordCount-1; i++)
            {
                UInt32 t1 = raw.Msgtime[i];
                UInt32 t2 = raw.Msgtime[i + 1];

                UInt32 diff = t2 - t1;

                if (diff == 0)
                {
                    gaps[i] = "-";
                }else if(diff > timeCutoff)
                {
                    double mins = diff / 60;
                    double hours = mins / 60;
                    double days = hours / 24;

                    string begindate = DataBinary.UT.UnixTimeStampToDateTime(Convert.ToDouble(t1)).ToString();
                    string enddate = DataBinary.UT.UnixTimeStampToDateTime(Convert.ToDouble(t2)).ToString();

                    if (diff >= timeCutoff && t2 > t1)
                    {
                         
                         gaps2.Add(diff.ToString() + "," + begindate + "," + t1.ToString() + "," + enddate + "," + t2.ToString() + "," + ((int)(mins)).ToString() + "," + Math.Round(hours,1).ToString() + "," + ((decimal)Math.Round(days,2)).ToString());
                    }
                    
                }else
                {
                    gaps[i] = "-";
                }
                

            }
            

            if (File.Exists(outname))
            {
                File.Delete(outname);
            }

            listBox1.DataSource = gaps2;
            button2.Enabled = true;
            

        }

        private void button2_Click(object sender, EventArgs e)
        {
            SF.FileName = outname;
            SF.ShowDialog();
            
            File.WriteAllLines(SF.FileName, gaps2);
            DownTimeForm frm = new DownTimeForm(SF.FileName);
            frm.Show();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            decimal timee = Convert.ToDecimal(comboBox1.Text);
            decimal minutess = timee / 60;
            decimal hours = minutess / 60;

            decimal total = Math.Round(hours, 1);
            label2.Text = total.ToString() + " Hours";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OF.Filter = "Text Files|*.csv|All Files|*.*";
            OF.ShowDialog();

            

            if (OF.FileName.Contains("VehicleID"))
            {

                DownTimeForm frm = new DownTimeForm(OF.FileName);
                frm.Show();

            }


            OF.Filter = "datFiles|*.dat|All File|*.*";

        }
    }

    

}
