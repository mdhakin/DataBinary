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
using MCRRS_LIST;
using DataLoggerParams;
namespace test
{
    public partial class SignalsFrm : Form
    {
        public SignalsFrm()
        {
            InitializeComponent();
        }

        ISignals sigs;
        Settings set;
        private void Form1_Load_1(object sender, EventArgs e)
        {
            set = new Settings();
            TreeNode mainNode = new TreeNode();
            mainNode.Name = "mainNode";
            mainNode.Text = "Main";
            this.treeView1.Nodes.Add(mainNode);


            TreeNode SixteenbitNode = new TreeNode();
            SixteenbitNode.Name = "Sixteen";
            SixteenbitNode.Text = "Sixteen bit Parameters";
            mainNode.Nodes.Add(SixteenbitNode);

            TreeNode EaghtBitNode = new TreeNode();
            EaghtBitNode.Name = "Eaght";
            EaghtBitNode.Text = "Eaght bit Parameters";
            mainNode.Nodes.Add(EaghtBitNode);


            TreeNode BoolNodes = new TreeNode();
            BoolNodes.Name = "Bool";
            BoolNodes.Text = "Boolean Parameters";
            mainNode.Nodes.Add(BoolNodes);

            sigs = DataLoggerParams.Factory.CreateSignals("MCRRS_SIGNALS.ini");

            for (int i = 0; i < sigs.SixTeenBitSignals.Count; i++)
            {
                TreeNode temp = new TreeNode();
                temp.Name = sigs.SixTeenBitSignals[i].Name;
                temp.Text = sigs.SixTeenBitSignals[i].Name;
                SixteenbitNode.Nodes.Add(temp);
            }

            for (int i = 0; i < sigs.EaghtBitSignals.Count; i++)
            {
                TreeNode temp = new TreeNode();
                temp.Name = sigs.EaghtBitSignals[i].Name;
                temp.Text = sigs.EaghtBitSignals[i].Name;
                EaghtBitNode.Nodes.Add(temp);
            }

            for (int i = 0; i < sigs.BoolSignals.Count; i++)
            {
                TreeNode temp = new TreeNode();
                temp.Name = sigs.BoolSignals[i].Name;
                temp.Text = sigs.BoolSignals[i].Name;
                BoolNodes.Nodes.Add(temp);
            }
        }

        private void treeView1_MouseDown(object sender, MouseEventArgs e)
        {
           
        }

        private void treeView1_Click(object sender, EventArgs e)
        {
            
        }

        private void treeView1_DoubleClick(object sender, EventArgs e)
        {
            cleartxtboxes();
            TreeNode temp = treeView1.SelectedNode;
            if (temp.Name != "mainNode" && temp.Name != "Sixteen" && temp.Name != "Eaght" && temp.Name != "Bool")
            {
                this.Text = temp.Parent.Name;

                if (temp.Parent.Name == "Sixteen")
                {
                    for (int i = 0; i < sigs.SixTeenBitSignals.Count; i++)
                    {
                        if (temp.Name == sigs.SixTeenBitSignals[i].Name)
                        {
                            panel1.Visible = true;
                            textBox1.Text = sigs.SixTeenBitSignals[i].Name;
                            textBox2.Text = sigs.SixTeenBitSignals[i].msgID;
                            textBox3.Text = sigs.SixTeenBitSignals[i].MSB;
                            textBox4.Text = sigs.SixTeenBitSignals[i].LSB;
                            textBox5.Text = sigs.SixTeenBitSignals[i].outofRangehi.ToString();
                            textBox6.Text = sigs.SixTeenBitSignals[i].outOfRangelo.ToString();
                            textBox7.Text = sigs.SixTeenBitSignals[i].Scale.ToString();
                            
                        }
                    }
                }


                if (temp.Parent.Name == "Eaght")
                {
                    for (int i = 0; i < sigs.EaghtBitSignals.Count; i++)
                    {
                        if (temp.Name == sigs.EaghtBitSignals[i].Name)
                        {
                            panel2.Visible = true;
                            textBox8.Text = sigs.EaghtBitSignals[i].Name;
                            textBox9.Text = sigs.EaghtBitSignals[i].msgID;
                            textBox10.Text = sigs.EaghtBitSignals[i].CanFrame;
                            textBox11.Text = sigs.EaghtBitSignals[i].outofRangehi.ToString();
                            textBox12.Text = sigs.EaghtBitSignals[i].outOfRangelo.ToString();
                            textBox13.Text = sigs.EaghtBitSignals[i].scale.ToString();

                        }
                    }
                }


                if (temp.Parent.Name == "Bool")
                {
                    for (int i = 0; i < sigs.BoolSignals.Count; i++)
                    {
                        if (temp.Name == sigs.BoolSignals[i].Name)
                        {
                            panel3.Visible = true;
                            textBox14.Text = sigs.BoolSignals[i].Name;
                            textBox15.Text = sigs.BoolSignals[i].msgID;
                            textBox16.Text = sigs.BoolSignals[i].canFrame;
                            textBox17.Text = sigs.BoolSignals[i].bitNo.ToString();
                            

                        }
                    }
                }



            } 
        }
        private void cleartxtboxes()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";

            textBox8.Text = "";
            textBox9.Text = "";
            textBox10.Text = "";
            textBox11.Text = "";
            textBox12.Text = "";
            textBox13.Text = "";

            panel1.Visible = false;
            panel2.Visible = false;
            panel3.Visible = false;
        }
        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
