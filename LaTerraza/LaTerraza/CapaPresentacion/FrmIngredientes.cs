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
    public partial class FrmIngredientes : Form
    {
        public FrmIngredientes()
        {
            InitializeComponent();
        }

        private void btnQuitar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public void MostrarDatos(string Buscar)
        {
            N_Ingredientes ingredientes = new N_Ingredientes();
           dgvIngredientes.DataSource = ingredientes.ListandoIngredientes(Buscar);
           dgvIngredientes.ClearSelection();
            dgvIngredientes.Columns[2].Visible= false;
            dgvIngredientes.Columns[1].Width = 250;
            dgvIngredientes.Columns["Descripcion1"].HeaderText = "Ingrediente";
            dgvIngredientes.Columns["Cantidad1"].HeaderText = "Cantidad";
        }

        private void FrmIngredientes_Load(object sender, EventArgs e)
        {
            MostrarDatos("");
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            MostrarDatos(txtBuscar.Text);
        }
    }
}
