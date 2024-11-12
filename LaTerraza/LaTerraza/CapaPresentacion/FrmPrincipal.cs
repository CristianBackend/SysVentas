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
    public partial class FrmPrincipal : Form
    {
        public FrmPrincipal()
        {
            InitializeComponent();
        }

     
        private void FrmPrincipal_Load(object sender, EventArgs e)
        {
           
           
            Timer.Start();
        }
        private Form activeForm = null;
        private void openChildForm(Form ChildForm)
        {
            if (activeForm != null)
           activeForm.Close();
            activeForm = ChildForm;
            ChildForm.TopLevel = false;

            ChildForm.Dock = DockStyle.Fill;
            PanelContenedor.Controls.Add(ChildForm);
            PanelContenedor.Tag = ChildForm;
            ChildForm.FormBorderStyle = FormBorderStyle.None;

            ChildForm.BringToFront();
            ChildForm.Show();


        }


        private void Timer_Tick(object sender, EventArgs e)
        {
            this.Opacity += 0.4;
            if (this.Opacity == 1)
            {
                Timer.Stop();
                frmLogin frm = new frmLogin();
                frm.Close();
            }
        }

        private void btnVender_Click(object sender, EventArgs e)
        {
            openChildForm(new FrmVentas());
        }

        private void btnCliente_Click(object sender, EventArgs e)
        {
            openChildForm(new FrmClientes());
        }

        private void btnProveedores_Click_1(object sender, EventArgs e)
        {
            openChildForm(new FrmProveedores());
        }

        private void btnInventario_Click(object sender, EventArgs e)
        {
            openChildForm(new FrmInventario());
        }

        private void btnVentas_Click(object sender, EventArgs e)
        {
            openChildForm(new FrmVentasHechas());
        }

        private void btnCategorias_Click_1(object sender, EventArgs e)
        {
            openChildForm(new FrmCategoria());
        }

        private void btnCompras_Click(object sender, EventArgs e)
        {
            openChildForm(new FrmCompras());
        }

        private void btnUsuarios_Click(object sender, EventArgs e)
        {
            openChildForm(new FrmUsuarios());
        }

        private void btnCuentasCobrar_Click(object sender, EventArgs e)
        {
            openChildForm(new frmCuentasCobrar());
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmLogin frm  = new frmLogin(); 
            frm.Show();
        }
    }
}
