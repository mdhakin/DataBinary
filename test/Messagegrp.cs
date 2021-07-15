using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MessageGroup;
namespace test
{
    public partial class Messagegrp : Form
    {
        public Messagegrp()
        {
            InitializeComponent();
        }

        private void Messagegrp_Load(object sender, EventArgs e)
        {

            MessageGroup.MessageGroup grp = new MessageGroup.MessageGroup(150, 2);

            grp.Messageset[0].frames[0] = (byte)254;

            
        }
    }
}
