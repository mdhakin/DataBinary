
namespace MCRRS_Simulator
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.console = new System.Windows.Forms.PictureBox();
            this.neutral_on = new System.Windows.Forms.PictureBox();
            this.rev_on = new System.Windows.Forms.PictureBox();
            this.fwd_on = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblOpenLoop = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.console)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.neutral_on)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rev_on)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fwd_on)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(29, 28);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "label1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(509, 47);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(92, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "HP water Pressue";
            // 
            // console
            // 
            this.console.Image = global::MCRRS_Simulator.Properties.Resources.BCKGRD_INDICATORS;
            this.console.Location = new System.Drawing.Point(313, 432);
            this.console.Name = "console";
            this.console.Size = new System.Drawing.Size(297, 45);
            this.console.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.console.TabIndex = 6;
            this.console.TabStop = false;
            // 
            // neutral_on
            // 
            this.neutral_on.Image = global::MCRRS_Simulator.Properties.Resources.NEUTRAL_INDICATOR;
            this.neutral_on.Location = new System.Drawing.Point(428, 457);
            this.neutral_on.Name = "neutral_on";
            this.neutral_on.Size = new System.Drawing.Size(49, 14);
            this.neutral_on.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.neutral_on.TabIndex = 5;
            this.neutral_on.TabStop = false;
            // 
            // rev_on
            // 
            this.rev_on.Image = global::MCRRS_Simulator.Properties.Resources.REV_INDICATOR;
            this.rev_on.Location = new System.Drawing.Point(486, 457);
            this.rev_on.Name = "rev_on";
            this.rev_on.Size = new System.Drawing.Size(49, 14);
            this.rev_on.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.rev_on.TabIndex = 4;
            this.rev_on.TabStop = false;
            // 
            // fwd_on
            // 
            this.fwd_on.Image = global::MCRRS_Simulator.Properties.Resources.FWD_INDICATOR;
            this.fwd_on.Location = new System.Drawing.Point(374, 457);
            this.fwd_on.Margin = new System.Windows.Forms.Padding(2);
            this.fwd_on.Name = "fwd_on";
            this.fwd_on.Size = new System.Drawing.Size(49, 14);
            this.fwd_on.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.fwd_on.TabIndex = 1;
            this.fwd_on.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::MCRRS_Simulator.Properties.Resources.MCRRS_MAIN;
            this.pictureBox1.Location = new System.Drawing.Point(215, 211);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(320, 129);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // lblOpenLoop
            // 
            this.lblOpenLoop.AutoSize = true;
            this.lblOpenLoop.Location = new System.Drawing.Point(499, 82);
            this.lblOpenLoop.Name = "lblOpenLoop";
            this.lblOpenLoop.Size = new System.Drawing.Size(107, 13);
            this.lblOpenLoop.TabIndex = 7;
            this.lblOpenLoop.Text = "Open Loop Pressure:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(810, 516);
            this.Controls.Add(this.lblOpenLoop);
            this.Controls.Add(this.fwd_on);
            this.Controls.Add(this.neutral_on);
            this.Controls.Add(this.rev_on);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.console);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.console)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.neutral_on)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rev_on)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fwd_on)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox fwd_on;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox rev_on;
        private System.Windows.Forms.PictureBox neutral_on;
        private System.Windows.Forms.PictureBox console;
        private System.Windows.Forms.Label lblOpenLoop;
    }
}

