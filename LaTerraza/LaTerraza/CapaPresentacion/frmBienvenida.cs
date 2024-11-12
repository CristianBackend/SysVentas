using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CapaPresentacion
{
    public partial class frmBienvenida : Form
    {
        public frmBienvenida()
        {
            InitializeComponent();
        }

        private void frmBienvenida_Load(object sender, EventArgs e)
        {
            Timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            this.Opacity += .06;
            if (this.Opacity == 1)
            {
                Timer.Stop();
                frmLogin frm = new frmLogin(); 
                frm.Show();
                this.Hide();
            }
            
        }
    }
}
