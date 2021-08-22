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
    public partial class AddMessageToFile : Form
    {
        enum AppState
        {
            loaded,
            unloaded,
            newFile,
            adding
        }
        public AddMessageToFile()
        {
            InitializeComponent();
        }
        AppState state;
        DataTable mainTable;
        private void AddMessageToFile_Load(object sender, EventArgs e)
        {
            state = AppState.unloaded;
            OF.InitialDirectory = Application.StartupPath;
            OF.Filter = "dat Files|*.dat|All Files|*.*";
            mainLoop();
        }


        private void mainLoop()
        {
            if (state == AppState.newFile)
            {
                state = AppState.adding;
                comboBox1.Items.Clear();
                comboBox1.Items.Add("150");
                dg.DataSource = null;
                dg.DataSource = mainTable;

                
            }else if (state == AppState.loaded)
            {
                state = AppState.adding;
                comboBox1.Items.Clear();

                for (int i = 0; i < mainTable.Rows.Count; i++)
                {
                    comboBox1.Items.Add(mainTable.Rows[i].Field<UInt32>(0));
                    dg.DataSource = null;
                    dg.DataSource = mainTable;

                }

            }
           
        }

        

        private void connect(string sPath)
        {
            if (File.Exists(sPath))
            {
                FileConnected?.Invoke(this, EventArgs.Empty);
                
                
                loadFile(sPath,ref mainTable);
                state = AppState.loaded;
                dg.DataSource = mainTable;
                this.Text = sPath;



            }
        }

        private DataTable setupDataTable()
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

            return ddt;
        }


        private DataTable loadDT(UInt32[] ts, UInt16[] id, byte[] F0, byte[] F1, byte[] F2, byte[] F3, byte[] F4, byte[] F5, byte[] F6, byte[] F7)
        {
            DataTable dt = new DataTable("file");

            dt.Columns.Add("TimeStamp", typeof(UInt32));
            dt.Columns.Add("MessageID", typeof(UInt16));
            dt.Columns.Add("f0", typeof(byte));
            dt.Columns.Add("f1", typeof(byte));
            dt.Columns.Add("f2", typeof(byte));
            dt.Columns.Add("f3", typeof(byte));
            dt.Columns.Add("f4", typeof(byte));
            dt.Columns.Add("f5", typeof(byte));
            dt.Columns.Add("f6", typeof(byte));
            dt.Columns.Add("f7", typeof(byte));


            for (int i = 0; i < ts.Length; i++)
            {
                DataRow dr = dt.NewRow();
                dr["TimeStamp"] = ts[i];
                dr["MessageID"] = id[i];
                dr["f0"] = F0[i];
                dr["f1"] = F1[i];
                dr["f2"] = F2[i];
                dr["f3"] = F3[i];
                dr["f4"] = F4[i];
                dr["f5"] = F5[i];
                dr["f6"] = F6[i];
                dr["f7"] = F7[i];

                dt.Rows.Add(dr);


            }


            return dt;
        }

        /// <summary>
        /// This is an internal function used to load the inital file into the class.
        /// </summary>
        /// <returns>If this returns anything but 0 there was an error.</returns>
        private int loadFile(string sFileName, ref DataTable ddt)
        {
            comboBox1.Items.Clear();
            FileStream fs = new FileStream(sFileName, FileMode.Open, FileAccess.Read);
            BinaryReader reader = new BinaryReader(fs);
            UInt32 m_Count = 0;
            bool fileready = false;
            string m_error = "";
            string m_fileStartDate = "";
            string m_fileEndDate = "";
            UInt32 m_totalHours = 0;
            // Calculate how many records there are
            long loops = fs.Length / 14;

            // Initialize the arrays according to the number of records
            UInt32[] m_msgtime = new UInt32[loops];
            UInt16[] m_msgid = new UInt16[loops];
            byte[] m_f0 = new byte[loops];
            byte[] m_f1 = new byte[loops];
            byte[] m_f2 = new byte[loops];
            byte[] m_f3 = new byte[loops];
            byte[] m_f4 = new byte[loops];
            byte[] m_f5 = new byte[loops];
            byte[] m_f6 = new byte[loops];
            byte[] m_f7 = new byte[loops];

            // hold the current timestamp to determine when a timestamp has changed
            UInt32 currTimeStamp = 0;

            // Increment every time a timestamp change occurs
            // at the end use to determine loggong frequency.
            long timestampCount = 0;

            if (loops > 0)
            {
                // Master loop through all records in the file.
                for (int i = 0; i < loops; i++)
                {
                    m_msgtime[i] = reader.ReadUInt32();
                    if (currTimeStamp != m_msgtime[i])
                    {
                        comboBox1.Items.Add(currTimeStamp.ToString());
                        if (currTimeStamp == 0)
                        {
                            DateTime dt = new DateTime();
                            currTimeStamp = m_msgtime[i];
                            m_fileStartDate = currTimeStamp.ToString();
                            timestampCount++;
                        }
                        else
                        {
                            timestampCount++;
                            if (m_msgtime[i] - currTimeStamp < 30)
                            {
                                m_totalHours = Convert.ToUInt32(m_totalHours + (m_msgtime[i] - currTimeStamp));
                                currTimeStamp = m_msgtime[i];
                            }
                            else
                            {
                                // To Do Calculate total hours
                                currTimeStamp = m_msgtime[i];
                            }
                        }
                    }
                    if (m_msgtime[i] < 1500000000)
                    {
                        m_Count = 0;
                        fileready = false;
                        m_error = "Data not formatted Correctly";
                        return -1;
                    }
                    m_msgid[i] = reader.ReadUInt16();
                    m_f0[i] = reader.ReadByte();
                    m_f1[i] = reader.ReadByte();
                    m_f2[i] = reader.ReadByte();
                    m_f3[i] = reader.ReadByte();
                    m_f4[i] = reader.ReadByte();
                    m_f5[i] = reader.ReadByte();
                    m_f6[i] = reader.ReadByte();
                    m_f7[i] = reader.ReadByte();
                }

                // set the total number of records in this file
                m_Count = (UInt32)loops;

                // Set the start and end Dates
                m_fileStartDate = DataBinary.UT.UnixTimeStampToDateTime(Convert.ToDouble(m_msgtime[0])).ToString();
                m_fileEndDate = DataBinary.UT.UnixTimeStampToDateTime(Convert.ToDouble(m_msgtime[m_Count - 1])).ToString();

                ddt = loadDT(m_msgtime, m_msgid, m_f0, m_f1, m_f2, m_f3, m_f4, m_f5, m_f6, m_f7);
            }
            else
            {
                //m_error = "Error loading data";
                //fileready = false;
                return -1;
            }

            // Close the reader
            reader.Close();
            // Close the file
            fs.Close();

            // Indicate that the file is loaded and ready for access
            fileready = true;
            return 0;
        }


        public event EventHandler FileConnected;

        private void button1_Click(object sender, EventArgs e)
        {
            OF.ShowDialog();
            if (File.Exists(OF.FileName))
            {
                connect(OF.FileName);
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            mainTable = setupDataTable();
            state = AppState.newFile;
            
            mainLoop();
        }

        private void button3_Click(object sender, EventArgs e)
        {

            UInt16[] ids = new UInt16[11];
            ids[0] = 100;
            ids[1] = 200;
            ids[2] = 201;
            ids[3] = 202;
            ids[4] = 203;
            ids[5] = 204;
            ids[6] = 205;
            ids[7] = 206;
            ids[8] = 207;
            ids[9] = 208;
            ids[10] = 500;

            var rand = new Random();
            if (state == AppState.adding)
            {
                if (!comboBox1.Items.Contains(txtTimeStamp) && Convert.ToUInt32(comboBox1.Items[comboBox1.Items.Count - 1].ToString()) < Convert.ToUInt32(txtTimeStamp.Text) && Convert.ToByte(txtdata.Text) < 255)
                {
                    for (int i = 0; i < 11; i++)
                    {


                        DataRow dr = mainTable.NewRow();
                        dr["TimeStamp"] = Convert.ToUInt32(txtTimeStamp.Text);
                        dr["MessageID"] = ids[i];
                        dr["f0"] = Convert.ToByte(txtdata.Text);
                        dr["f1"] = Convert.ToByte(txtdata.Text);
                        dr["f2"] = Convert.ToByte(txtdata.Text);
                        dr["f3"] = Convert.ToByte(txtdata.Text);
                        dr["f4"] = Convert.ToByte(txtdata.Text);
                        dr["f5"] = Convert.ToByte(txtdata.Text);
                        dr["f6"] = Convert.ToByte(txtdata.Text);
                        dr["f7"] = Convert.ToByte(txtdata.Text);

                        mainTable.Rows.Add(dr);
                    }

                    comboBox1.Items.Add(txtTimeStamp.Text);
                    txtTimeStamp.Text = (Convert.ToUInt32(txtTimeStamp.Text) + 10).ToString();

                }
            }

            mainLoop();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SF1.ShowDialog();
            if (!string.IsNullOrEmpty(SF1.FileName))
            {
                if (File.Exists(SF1.FileName))
                {
                    File.Delete(SF1.FileName);
                }
               //DataBinary.CreateDataset.WriteDatatableToFile(ref mainTable, SF1.FileName);

                DataBinary.CombineRawFiles.WriteDatatableToFile(ref mainTable, SF1.FileName);
            }

            
            
        }

        private void testfile()
        {
            //DataBinary.RawFile raw = new DataBinary.RawFile((Environment.SpecialFolder.UserProfile) + @"\Desktop\0000.dat");
            //MessageBox.Show(raw.sizeInBytes().ToString());
        }

        private void AddMessageToFile_DoubleClick(object sender, EventArgs e)
        {
            testfile();
        }
    }

    
}
