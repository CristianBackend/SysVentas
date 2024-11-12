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
    public partial class FrmError : Form
    {
        public FrmError(string mensaje)
        {
            InitializeComponent();
             txtMensaje.Text = mensaje;
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void FrmError_Load(object sender, EventArgs e)
        {
            Timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            this.Opacity += .50;
            if (this.Opacity == 1)
            {
                Timer.Stop();
            }
        }
    }
}
