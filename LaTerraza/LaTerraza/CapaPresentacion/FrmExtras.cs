using CapaNegocio;
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
    public partial class FrmExtras : Form
    {
        N_Extras extras = new N_Extras();   
        public FrmExtras()
        {
            InitializeComponent();
        }


        public void MostrarDatos(string Buscar)
        {
            dgvExtras.DataSource = extras.ListandoExtras(Buscar);
            dgvExtras.ClearSelection();
        }

        private void FrmExtras_Load(object sender, EventArgs e)
        {
            MostrarDatos("");
            dgvExtras.Columns[0].Visible = false;
            dgvExtras.Columns[1].HeaderText ="Nombre Del Extra";
            dgvExtras.Columns[1].Width = 290;
            dgvExtras.Columns[2].HeaderText = "Precio Del Extra";
            dgvExtras.Columns[2].Width = 90;
            dgvExtras.Columns[3].Width = 300;
            dgvExtras.Columns[3].HeaderText = "Descripcion";
            dgvExtras.Columns["Descripcion1"].DefaultCellStyle.WrapMode = DataGridViewTriState.True;

        }

        private void btnAgregarUsuario_Click(object sender, EventArgs e)
        {
            FrmAgregarExtras frmAgregarExtras = new FrmAgregarExtras();
            frmAgregarExtras.ShowDialog();  
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            MostrarDatos(txtBuscar.Text);
        }

        private void dgvExtras_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dgvExtras.ClearSelection();
        }

        private void dgvExtras_BindingContextChanged(object sender, EventArgs e)
        {
            dgvExtras.ClearSelection();
        }
    }
}
