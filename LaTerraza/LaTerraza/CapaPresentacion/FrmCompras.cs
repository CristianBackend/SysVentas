using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CapaNegocio;
using CapaEntidades;

namespace CapaPresentacion
{
    public partial class FrmCompras : Form
    {
        N_Compras N_Compras =new N_Compras();
        public FrmCompras()
        {
            InitializeComponent();
        }
        private void btnQuitar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void CargarDatos()
        {
          DataTable dt = N_Compras.MostrarCompras();
          dgvVerProductos.DataSource = dt;
            dgvVerProductos.Columns[0].HeaderText = "NO COMPRA";
            dgvVerProductos.Columns[1].Visible = false;
            dgvVerProductos.Columns[2].HeaderText = "Proveedor";
        dgvVerProductos.Columns[6].Visible = false;
        dgvVerProductos.Columns[7].HeaderText = "Productos";
        dgvVerProductos.Columns[7].Width = 250;
        dgvVerProductos.Columns["Nombre_Proveedor"].Width = 250;
        dgvVerProductos.Columns[9].HeaderText = "Precio";
        dgvVerProductos.Columns["Id_Compra"].DisplayIndex = 0;
       dgvVerProductos.Columns["Descripcion_Producto"].DisplayIndex = 1;
       dgvVerProductos.Columns["Nombre_Proveedor"].DisplayIndex = 2;
        dgvVerProductos.Columns["Precio_Unitario"].DisplayIndex = 3;
       dgvVerProductos.Columns["Cantidad"].DisplayIndex = 4;
      dgvVerProductos.Columns["Subtotal"].DisplayIndex = 5;
      dgvVerProductos.Columns["Total"].DisplayIndex = 6;
     dgvVerProductos.Columns["Estado"].DisplayIndex = 7;
     dgvVerProductos.Columns["Fecha"].DisplayIndex = 8;
        }
        private void FrmCompras_Load(object sender, EventArgs e)
        {
            CargarDatos();
        }

        private void btnComprar_Click(object sender, EventArgs e)
        {
            ComprarProductoNuevo frm = new ComprarProductoNuevo();  
            frm.ShowDialog();
        }

        public void buscar(string buscar)
        {
           dgvVerProductos.DataSource = N_Compras.BuscarCompra(buscar);
        }

        private void btnVer_Click(object sender, EventArgs e)
        {
           
           FrmIngredientes frm = new FrmIngredientes();
            frm.ShowDialog();
           
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            buscar(txtBuscar.Text);
        }

        private void btnCuentasPagar_Click(object sender, EventArgs e)
        {
            FrmCuentasPagar frm = new FrmCuentasPagar();    
            frm.ShowDialog();
        }
    }
}
