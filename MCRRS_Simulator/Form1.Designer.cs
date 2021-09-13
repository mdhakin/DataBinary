
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblOpenLoop = new System.Windows.Forms.Label();
            this.lblEngingRpm = new System.Windows.Forms.Label();
            this.lblVacRPM = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtCurrentIndex = new System.Windows.Forms.TextBox();
            this.lblChangeIndex = new System.Windows.Forms.Label();
            this.btnChangeIndex = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.CleaningPlatformOn = new System.Windows.Forms.PictureBox();
            this.pDeck_Lock = new System.Windows.Forms.PictureBox();
            this.pDeck_Float = new System.Windows.Forms.PictureBox();
            this.lblVac_console_on = new System.Windows.Forms.PictureBox();
            this.fwd_on = new System.Windows.Forms.PictureBox();
            this.neutral_on = new System.Windows.Forms.PictureBox();
            this.rev_on = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.console = new System.Windows.Forms.PictureBox();
            this.listBox1 = new System.Windows.Forms.ListBox();
            ((System.ComponentModel.ISupportInitialize)(this.CleaningPlatformOn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pDeck_Lock)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pDeck_Float)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblVac_console_on)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fwd_on)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.neutral_on)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rev_on)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.console)).BeginInit();
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
            this.label1.Location = new System.Drawing.Point(482, 8);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "TimeStamp";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(482, 25);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(92, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "HP water Pressue";
            // 
            // lblOpenLoop
            // 
            this.lblOpenLoop.AutoSize = true;
            this.lblOpenLoop.Location = new System.Drawing.Point(482, 39);
            this.lblOpenLoop.Name = "lblOpenLoop";
            this.lblOpenLoop.Size = new System.Drawing.Size(107, 13);
            this.lblOpenLoop.TabIndex = 7;
            this.lblOpenLoop.Text = "Open Loop Pressure:";
            // 
            // lblEngingRpm
            // 
            this.lblEngingRpm.AutoSize = true;
            this.lblEngingRpm.Location = new System.Drawing.Point(482, 53);
            this.lblEngingRpm.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblEngingRpm.Name = "lblEngingRpm";
            this.lblEngingRpm.Size = new System.Drawing.Size(68, 13);
            this.lblEngingRpm.TabIndex = 8;
            this.lblEngingRpm.Text = "Engine Rpm:";
            // 
            // lblVacRPM
            // 
            this.lblVacRPM.AutoSize = true;
            this.lblVacRPM.Location = new System.Drawing.Point(482, 70);
            this.lblVacRPM.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblVacRPM.Name = "lblVacRPM";
            this.lblVacRPM.Size = new System.Drawing.Size(35, 13);
            this.lblVacRPM.TabIndex = 10;
            this.lblVacRPM.Text = "label3";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "100",
            "200",
            "300",
            "400",
            "500",
            "600",
            "700",
            "800",
            "900",
            "1000"});
            this.comboBox1.Location = new System.Drawing.Point(498, 253);
            this.comboBox1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(66, 21);
            this.comboBox1.TabIndex = 11;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(496, 236);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "Time Interval";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(567, 258);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(20, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "ms";
            // 
            // txtCurrentIndex
            // 
            this.txtCurrentIndex.Location = new System.Drawing.Point(498, 306);
            this.txtCurrentIndex.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtCurrentIndex.Name = "txtCurrentIndex";
            this.txtCurrentIndex.Size = new System.Drawing.Size(66, 20);
            this.txtCurrentIndex.TabIndex = 16;
            this.txtCurrentIndex.Text = "0";
            // 
            // lblChangeIndex
            // 
            this.lblChangeIndex.AutoSize = true;
            this.lblChangeIndex.Location = new System.Drawing.Point(498, 287);
            this.lblChangeIndex.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblChangeIndex.Name = "lblChangeIndex";
            this.lblChangeIndex.Size = new System.Drawing.Size(33, 13);
            this.lblChangeIndex.TabIndex = 17;
            this.lblChangeIndex.Text = "Index";
            // 
            // btnChangeIndex
            // 
            this.btnChangeIndex.Location = new System.Drawing.Point(567, 305);
            this.btnChangeIndex.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnChangeIndex.Name = "btnChangeIndex";
            this.btnChangeIndex.Size = new System.Drawing.Size(50, 19);
            this.btnChangeIndex.TabIndex = 18;
            this.btnChangeIndex.Text = "Update";
            this.btnChangeIndex.UseVisualStyleBackColor = true;
            this.btnChangeIndex.Click += new System.EventHandler(this.btnChangeIndex_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(814, 70);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 20;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // CleaningPlatformOn
            // 
            this.CleaningPlatformOn.BackColor = System.Drawing.Color.Transparent;
            this.CleaningPlatformOn.Image = global::MCRRS_Simulator.Properties.Resources.GREEN_DECK;
            this.CleaningPlatformOn.Location = new System.Drawing.Point(138, 77);
            this.CleaningPlatformOn.Name = "CleaningPlatformOn";
            this.CleaningPlatformOn.Size = new System.Drawing.Size(54, 129);
            this.CleaningPlatformOn.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.CleaningPlatformOn.TabIndex = 19;
            this.CleaningPlatformOn.TabStop = false;
            this.CleaningPlatformOn.Visible = false;
            // 
            // pDeck_Lock
            // 
            this.pDeck_Lock.Image = global::MCRRS_Simulator.Properties.Resources.DECK_LOCK_INDICATOR;
            this.pDeck_Lock.Location = new System.Drawing.Point(281, 252);
            this.pDeck_Lock.Margin = new System.Windows.Forms.Padding(2);
            this.pDeck_Lock.Name = "pDeck_Lock";
            this.pDeck_Lock.Size = new System.Drawing.Size(65, 14);
            this.pDeck_Lock.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pDeck_Lock.TabIndex = 15;
            this.pDeck_Lock.TabStop = false;
            // 
            // pDeck_Float
            // 
            this.pDeck_Float.Image = global::MCRRS_Simulator.Properties.Resources.DECK_FLOAT_INDICATOR;
            this.pDeck_Float.Location = new System.Drawing.Point(277, 271);
            this.pDeck_Float.Margin = new System.Windows.Forms.Padding(2);
            this.pDeck_Float.Name = "pDeck_Float";
            this.pDeck_Float.Size = new System.Drawing.Size(65, 14);
            this.pDeck_Float.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pDeck_Float.TabIndex = 14;
            this.pDeck_Float.TabStop = false;
            // 
            // lblVac_console_on
            // 
            this.lblVac_console_on.Image = global::MCRRS_Simulator.Properties.Resources.VACUUM_INDICATOR;
            this.lblVac_console_on.Location = new System.Drawing.Point(151, 256);
            this.lblVac_console_on.Margin = new System.Windows.Forms.Padding(2);
            this.lblVac_console_on.Name = "lblVac_console_on";
            this.lblVac_console_on.Size = new System.Drawing.Size(49, 14);
            this.lblVac_console_on.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.lblVac_console_on.TabIndex = 9;
            this.lblVac_console_on.TabStop = false;
            // 
            // fwd_on
            // 
            this.fwd_on.Image = global::MCRRS_Simulator.Properties.Resources.FWD_INDICATOR;
            this.fwd_on.Location = new System.Drawing.Point(151, 271);
            this.fwd_on.Margin = new System.Windows.Forms.Padding(2);
            this.fwd_on.Name = "fwd_on";
            this.fwd_on.Size = new System.Drawing.Size(49, 14);
            this.fwd_on.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.fwd_on.TabIndex = 1;
            this.fwd_on.TabStop = false;
            // 
            // neutral_on
            // 
            this.neutral_on.Image = global::MCRRS_Simulator.Properties.Resources.NEUTRAL_INDICATOR;
            this.neutral_on.Location = new System.Drawing.Point(190, 271);
            this.neutral_on.Name = "neutral_on";
            this.neutral_on.Size = new System.Drawing.Size(49, 14);
            this.neutral_on.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.neutral_on.TabIndex = 5;
            this.neutral_on.TabStop = false;
            // 
            // rev_on
            // 
            this.rev_on.Image = global::MCRRS_Simulator.Properties.Resources.REV_INDICATOR;
            this.rev_on.Location = new System.Drawing.Point(233, 271);
            this.rev_on.Name = "rev_on";
            this.rev_on.Size = new System.Drawing.Size(49, 14);
            this.rev_on.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.rev_on.TabIndex = 4;
            this.rev_on.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::MCRRS_Simulator.Properties.Resources.MCRRS_MAIN;
            this.pictureBox1.Location = new System.Drawing.Point(138, 77);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(320, 129);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // console
            // 
            this.console.Image = global::MCRRS_Simulator.Properties.Resources.BCKGRD_INDICATORS;
            this.console.Location = new System.Drawing.Point(107, 252);
            this.console.Name = "console";
            this.console.Size = new System.Drawing.Size(297, 45);
            this.console.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.console.TabIndex = 6;
            this.console.TabStop = false;
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(688, 184);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(239, 290);
            this.listBox1.TabIndex = 21;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(939, 487);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.CleaningPlatformOn);
            this.Controls.Add(this.btnChangeIndex);
            this.Controls.Add(this.lblChangeIndex);
            this.Controls.Add(this.txtCurrentIndex);
            this.Controls.Add(this.pDeck_Lock);
            this.Controls.Add(this.pDeck_Float);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.lblVacRPM);
            this.Controls.Add(this.lblVac_console_on);
            this.Controls.Add(this.lblEngingRpm);
            this.Controls.Add(this.lblOpenLoop);
            this.Controls.Add(this.fwd_on);
            this.Controls.Add(this.neutral_on);
            this.Controls.Add(this.rev_on);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.console);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.CleaningPlatformOn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pDeck_Lock)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pDeck_Float)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblVac_console_on)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fwd_on)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.neutral_on)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rev_on)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.console)).EndInit();
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
        private System.Windows.Forms.Label lblEngingRpm;
        private System.Windows.Forms.PictureBox lblVac_console_on;
        private System.Windows.Forms.Label lblVacRPM;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.PictureBox pDeck_Float;
        private System.Windows.Forms.PictureBox pDeck_Lock;
        private System.Windows.Forms.TextBox txtCurrentIndex;
        private System.Windows.Forms.Label lblChangeIndex;
        private System.Windows.Forms.Button btnChangeIndex;
        private System.Windows.Forms.PictureBox CleaningPlatformOn;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ListBox listBox1;
    }
}

