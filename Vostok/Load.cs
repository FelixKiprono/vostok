using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Vostok
{
    public partial class Load : Form
    {
        public Load()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            progress.Value++;
            if( progress.Value==100)
            {
                timer1.Enabled = false;
                this.Hide();
                new Connect().ShowDialog();
            }
        }
    }
}
