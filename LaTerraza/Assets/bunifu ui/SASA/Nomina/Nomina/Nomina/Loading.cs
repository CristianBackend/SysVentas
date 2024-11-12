using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nomina
{
    public partial class Loading : Form
    {
        public Loading()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            while(this.Opacity > 0)
            {
                this.Opacity-=0.00001;
            }
            this.Hide();
            Form1 form1 = new Form1();
            form1.Show();
            timer1.Stop();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            timer1.Interval = 1500;
            timer1.Start();
        }

        private void Loading_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            timer1.Interval = 2500;
            timer1.Start();
        }
    }
}
