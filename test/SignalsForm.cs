using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataLoggerParams;

namespace test
{
    public partial class SignalsForm : Form
    {
        public SignalsForm()
        {
            InitializeComponent();
        }

        private void SignalsForm_Load(object sender, EventArgs e)
        {
            MessageIDs ids = new MessageIDs();
            lb1.DataSource = ids.getIDs();
        }

        private void lb1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lb1.SelectedIndex != -1 && !string.IsNullOrEmpty(lb1.Text))
            {
                MessageIDs ids = new MessageIDs();
                lb2.DataSource = ids.SignalsInId(lb1.Text);
            }
        }
    }
}
