using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CapaEntidades;
using CapaNegocio;

namespace CapaPresentacion
{
    public partial class frmComprarProductoEnInventario : Form
    {
        N_Productos N_Productos = new N_Productos();
        public frmComprarProductoEnInventario()
        {
            InitializeComponent();
        }

        public void comportamientodeldgv()
        {
           
            dgvCompras.DataSource = N_Productos.MostrarProductos();
              dgvCompras.Columns[0].Visible = false;
            dgvCompras.Columns[4].Visible = false;
            dgvCompras.Columns[5].Visible = false;
            dgvCompras.Columns[6].Visible = false;
            dgvCompras.Columns[7].Visible = false;
            dgvCompras.Columns[8].Visible = false;
            dgvCompras.Columns[9].Visible = false;
            dgvCompras.Columns[10].Visible = false;
            dgvCompras.Columns[11].Visible = false;
            
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();   
        }

        private void frmComprarProductoEnInventario_Load(object sender, EventArgs e)
        {
            comportamientodeldgv();
            this.BeginInvoke((MethodInvoker)delegate {
                dgvCompras.ClearSelection();
            });
        }
        public void buscar(string buscar)
        {
            dgvCompras.DataSource = N_Productos.BuscarProducto(buscar);
        }
        private void dgvCompras_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dgvCompras.ClearSelection();
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            buscar(txtBuscar.Text);
        }

        private void dgvCompras_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dgvCompras.CurrentRow.Cells[2].Value.ToString();
            textBox2.Text= dgvCompras.CurrentRow.Cells[3].Value.ToString();
        }

        public void actualizarstock(E_Productos productos, int cantidad)
        {
            cantidad = Convert.ToInt32(tCantidad.Text);
            productos.Nombre_Producto1 = textBox1.Text;
            N_Productos.ActualizarStock(productos, cantidad);
         }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            E_Productos productos = new E_Productos();
            int cantidad;
            if (tCantidad.Text=="")
            {
                FrmError frmError = new FrmError("SEGURO QUE QUIERES ELEIMINAR ESTE REGISTRO?😢");
            }
            else
            {
                cantidad = Convert.ToInt32(tCantidad.Text);
                productos.Nombre_Producto1 = textBox1.Text;
                N_Productos.ActualizarStock(productos, cantidad);
                FrmExitoso.ConfirmacionForm("¡Que Buena Compra! 😊👌");
                tCantidad.Clear();
                textBox1.Clear();
                textBox2.Clear();
                FrmCompras frm = Application.OpenForms.OfType<FrmCompras>().FirstOrDefault();
                if (frm != null)
                {
                    
                    frm.BringToFront();
                }
                this.Close();
            }
        }
    }
}
