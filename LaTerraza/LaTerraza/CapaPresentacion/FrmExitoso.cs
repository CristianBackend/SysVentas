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
    public partial class FrmExitoso : Form
    {

        public FrmExitoso(string mensaje)
        {
            InitializeComponent();
          txtMensaje.Text = mensaje;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            this.Opacity += .22;
            if (this.Opacity == 1)
            {
                Timer.Stop();
            }
        }

        private void FrmExitoso_Load(object sender, EventArgs e)
        {
            Timer.Start();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public static void ConfirmacionForm(string mensaje)
        {
            FrmExitoso frmExitoso = new FrmExitoso( mensaje);
            frmExitoso.ShowDialog();

        }
    }
}
