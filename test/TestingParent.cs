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
    public partial class TestingParent : Form
    {
        private int childFormNumber = 0;

        public TestingParent()
        {
            InitializeComponent();
        }

        private void ShowNewForm(object sender, EventArgs e)
        {
            
            loadEvaluationForm();
        }

        private void OpenFile(object sender, EventArgs e)
        {
            //OpenFileDialog openFileDialog = new OpenFileDialog();
            //openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            //openFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            //if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            //{
            //    string FileName = openFileDialog.FileName;
            //}
            LoadRawDataTestForm();
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            saveFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = saveFileDialog.FileName;
            }
        }

        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CutToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void ToolBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStrip.Visible = toolBarToolStripMenuItem.Checked;
        }

        private void StatusBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            statusStrip.Visible = statusBarToolStripMenuItem.Checked;
        }

        private void CascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void TileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void TileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void ArrangeIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }

        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
        }

        private void rawDataFileTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadRawDataTestForm();
        }

        private void loadGapVisualize()
        {
            //GapVisualize
            GapVisualize childForm = new GapVisualize();
            childForm.MdiParent = this;
            //childForm.Text = "Window " + childFormNumber++;
            childForm.Show();
        }

        private void LoadRawDataTestForm()
        {
            RawData childForm = new RawData();
            childForm.MdiParent = this;
            //childForm.Text = "Window " + childFormNumber++;
            childForm.Show();
        }

        private void LoadSignalsForm()
        {
            SignalsFrm childForm = new SignalsFrm();
            childForm.MdiParent = this;
            //childForm.Text = "Window " + childFormNumber++;
            childForm.Show();
        }

        private void saveToolStripButton_Click(object sender, EventArgs e)
        {
            LoadSignalsForm();
        }

        private void TestingParent_Load(object sender, EventArgs e)
        {
            OF.InitialDirectory = Application.StartupPath;
        }

        private void messageGroupTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SignalsForm childForm = new SignalsForm();
            childForm.MdiParent = this;
            
            childForm.Show();
        }

        private void vehicleListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //vehicleList
            vehicleList childForm = new vehicleList();
            childForm.MdiParent = this;
            
            childForm.Show();
        }

        private void makeDataFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            loadGapVisualize();
        }

        private void printToolStripButton_Click(object sender, EventArgs e)
        {
            
           
            Graph_on_off childForm = new Graph_on_off();
            childForm.MdiParent = this;
            
            childForm.Show();
        }

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void printPreviewToolStripButton_Click(object sender, EventArgs e)
        {
            ////MakeDataFile
            //MakeDataFile childForm = new MakeDataFile();
            //childForm.MdiParent = this;
            ////childForm.Text = "Window " + childFormNumber++;
            //childForm.Show();
            OpenAddMsgesToFile();
        }

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void helpToolStripButton_Click(object sender, EventArgs e)
        {
            OF.Filter = "Text Files|*.csv|All Files|*.*";
            OF.ShowDialog();



            if (OF.FileName.Contains("VehicleID") && File.Exists(OF.FileName))
            {

                DownTimeForm frm = new DownTimeForm(OF.FileName);
                frm.MdiParent = this;
                frm.Show();

            }else
            {
                MessageBox.Show("Invalad File Name!");
            }
        }

        private void addMessagesToFileToolStripMenuItem_Click(object sender, EventArgs e)
        {

            OpenAddMsgesToFile();

        }

        private void OpenAddMsgesToFile()
        {
            AddMessageToFile childForm = new AddMessageToFile();
            childForm.MdiParent = this;
            childForm.Text = "Add Messages to File";
            childForm.Show();
        }

        private void testCombineRawFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            loadEvaluationForm();
        }

        private void loadEvaluationForm()
        {
            // TextCombineRawFile
            TextCombineRawFile childForm = new TextCombineRawFile(Application.StartupPath + @"\output\6459\6459.dat", Application.StartupPath + @"\output\6511\6511.dat");
            childForm.MdiParent = this;
            childForm.Text = "Test Combine Raw File";
            childForm.Show();
        }
    }
}
