using CapaEntidades;
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
    public partial class FrmCuentasPagar : Form
    {
        N_Compras N_Compras = new N_Compras();

        public FrmCuentasPagar()
        {
            InitializeComponent();
            this.VisibleChanged += dgvCuentasPagar_VisibleChanged;
        }

        public void buscar(string buscar)
        {
            dgvCuentasPagar.DataSource = N_Compras.BuscarCompra(buscar);
        }

        private void CargarDatos()
        {
            DataTable dt = N_Compras.MostrarCuentasPorPagar();
            dgvCuentasPagar.DataSource = dt;
            // Limpiar selección después de cargar los datos
            dgvCuentasPagar.ClearSelection();
        }

        private void ContarCuentasPagar()
        {
            int cantidadProveedores = N_Compras.ContarCuentasPagar();
            labelContador.Text = cantidadProveedores.ToString();
        }

        private void FrmCuentasPagar_Load(object sender, EventArgs e)
        {
            CargarDatos();
            dgvCuentasPagar.Columns[0].HeaderText = "NO COMPRA";
            dgvCuentasPagar.Columns[1].Visible = false;
            dgvCuentasPagar.Columns[2].HeaderText = "Proveedor";
            dgvCuentasPagar.Columns[6].Visible = false;
            dgvCuentasPagar.Columns[7].HeaderText = "Productos";
            dgvCuentasPagar.Columns[7].Width = 250;
            dgvCuentasPagar.Columns["Nombre_Proveedor"].Width = 250;
            dgvCuentasPagar.Columns[9].HeaderText = "Precio";
            dgvCuentasPagar.Columns["Id_Compra"].DisplayIndex = 0;
            dgvCuentasPagar.Columns["Descripcion_Producto"].DisplayIndex = 1;
            dgvCuentasPagar.Columns["Nombre_Proveedor"].DisplayIndex = 2;
            dgvCuentasPagar.Columns["Precio_Unitario"].DisplayIndex = 3;
            dgvCuentasPagar.Columns["Cantidad"].DisplayIndex = 4;
            dgvCuentasPagar.Columns["Subtotal"].DisplayIndex = 5;
            dgvCuentasPagar.Columns["Total"].DisplayIndex = 6;
            dgvCuentasPagar.Columns["Estado"].DisplayIndex = 7;
            dgvCuentasPagar.Columns["Fecha"].DisplayIndex = 8;
            ContarCuentasPagar();

            // Limpiar selección al cargar
            dgvCuentasPagar.ClearSelection();
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            buscar(txtBuscar.Text);

            // Supongamos que tienes el DataTable "dataTable" que se usa como fuente de datos
            DataTable dataTable = (DataTable)dgvCuentasPagar.DataSource;

            if (dataTable != null)
            {
                // Crear una vista filtrada que solo incluya "A Credito"
                DataView dv = new DataView(dataTable);
                dv.RowFilter = "Estado <> 'A Contado'";

                // Asignar la vista filtrada al DataGridView
                dgvCuentasPagar.DataSource = dv;
            }

            // Limpiar selección después de filtrar
            dgvCuentasPagar.ClearSelection();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvCuentasPagar.SelectedRows.Count == 0 || dgvCuentasPagar.CurrentRow.Index == -1)
                {
                    FrmError frmErrorr = new FrmError("Seleccione La Fila");
                    frmErrorr.ShowDialog();
                    return;
                }
                DialogResult resultado = new DialogResult();
                FrmError frmError = new FrmError("Confirme Que Esta Cuenta Ha Sido Pagada Totalmente, Se eliminará de CUENTAS POR PAGAR y pasara al flujo de cuentas normales");
                resultado = frmError.ShowDialog();

                if (resultado == DialogResult.OK)
                {
                    int idCompra = Convert.ToInt32(dgvCuentasPagar.CurrentRow.Cells["Id_Compra"].Value);

                    N_Compras.CambiarEstadoAContado(idCompra);

                    FrmExitoso.ConfirmacionForm("ESTA CUENTA HA SIDO PAGADA Y YA NO SERA VISIBLE COMO CUENTAS POR PAGAR.");

                    ContarCuentasPagar();
                    FrmCompras frm = Application.OpenForms.OfType<FrmCompras>().FirstOrDefault();
                    if (frm != null)
                    {
                        frm.CargarDatos(); // Recarga los datos
                        frm.BringToFront();    // Trae el formulario al frente (opcional)
                    }
                    CargarDatos();

                }

            }
            catch (Exception ex)
            {
                FrmError frmError = new FrmError("Error: " + ex.Message);
                frmError.ShowDialog();
            }
        }

        private void dgvCuentasPagar_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            // Limpiar selección al completar la vinculación de datos
            dgvCuentasPagar.ClearSelection();
        }

        private void dgvCuentasPagar_BindingContextChanged(object sender, EventArgs e)
        {
            // Limpiar selección si cambia el contexto de la vinculación
            dgvCuentasPagar.ClearSelection();
        }

        private void dgvCuentasPagar_VisibleChanged(object sender, EventArgs e)
        {
            // Limpiar selección cuando cambia la visibilidad del formulario
            dgvCuentasPagar.ClearSelection();
        }
    }
}