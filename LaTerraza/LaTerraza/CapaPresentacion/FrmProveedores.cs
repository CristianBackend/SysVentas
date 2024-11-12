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
    public partial class FrmProveedores : Form
    {
        N_Proveedor proveedor = new N_Proveedor();
        E_Proveedor e_Proveedor=new E_Proveedor();
        public FrmProveedores()
        {
            InitializeComponent();
        }

        private void ContarProveedores(){

            int cantidadProveedores = proveedor.ContarProveedores();
            labelContador.Text=cantidadProveedores.ToString();
        }
        private void FrmProveedores_Load(object sender, EventArgs e)
        {
            MostrarDatos("");
            dgvProveedores.Columns[0].Visible = false;
            dgvProveedores.Columns[1].HeaderText = "Codigo";
            dgvProveedores.Columns[2].HeaderText = "Nombre";
            dgvProveedores.Columns[3].HeaderText = "Correo";
            dgvProveedores.Columns[4].HeaderText = "Telefono";
            dgvProveedores.Columns[5].HeaderText = "Direccion";



            dgvProveedores.ClearSelection();
        }
        public void MostrarDatos(string Buscar)
        {
            N_Proveedor pro = new N_Proveedor();
           dgvProveedores.DataSource = pro.ListandoProveedores(Buscar);
           dgvProveedores.Refresh();
            ContarProveedores();

        }

        private void btnQuitar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            FrmAgregarProveedor frmAgregarProveedor = new FrmAgregarProveedor();
            frmAgregarProveedor.ShowDialog();
        }

        

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            FrmAgregarProveedor frm = new FrmAgregarProveedor();
            frm.ShowDialog();   
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            FrmEditarProveedor frmEditarProveedor = new FrmEditarProveedor();
            try{ 

             if(dgvProveedores.SelectedRows.Count == 0 || dgvProveedores.CurrentRow.Index == -1)
                {
                    FrmError frmError = new FrmError("Seleccione La Fila");
                    frmError.ShowDialog();
                }
              else{

                   frmEditarProveedor.tId.Text=dgvProveedores.CurrentRow.Cells[0].Value.ToString();
                   frmEditarProveedor.tNombre.Text = dgvProveedores.CurrentRow.Cells[2].Value.ToString();
                   frmEditarProveedor.tCorreo.Text = dgvProveedores.CurrentRow.Cells[3].Value.ToString();
                   frmEditarProveedor.tTel.Text = dgvProveedores.CurrentRow.Cells[4].Value.ToString();
                   frmEditarProveedor.tDireccion.Text = dgvProveedores.CurrentRow.Cells[5].Value.ToString();

                    frmEditarProveedor.ShowDialog();

              }
           
            }
            catch(Exception ex)
            {
                FrmError frmError = new FrmError("Error " + ex);
                frmError.ShowDialog();
            }
        }

        private void dgvProveedores_BindingContextChanged(object sender, EventArgs e)
        {
            dgvProveedores.ClearSelection();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvProveedores.SelectedRows.Count == 0 || dgvProveedores.CurrentRow.Index == -1)
                {
                    FrmError frmErrorr = new FrmError("Seleccione La Fila");
                    frmErrorr.ShowDialog();
                    return;
                }
                DialogResult resultado = new DialogResult();
                FrmError frmError = new FrmError("¿Seguro que quieres eliminar este Proveedor? 😢");
                resultado = frmError.ShowDialog();

                if (resultado == DialogResult.OK)
                {
                    e_Proveedor.Id_Proveedor1 = Convert.ToInt32(dgvProveedores.SelectedRows[0].Cells[0].Value);
                    proveedor.EliminarProveedor(e_Proveedor.Id_Proveedor1);
                    FrmExitoso.ConfirmacionForm("Se eliminó correctamente. 😒");
                    MostrarDatos("");
                    dgvProveedores.ClearSelection();
                }
            }
            catch 
            {
                FrmError frmErrorr = new FrmError("Aun Hay Productos De Este Proveedor");
                frmErrorr.ShowDialog();
            }
        }

        private void txtBuscar_TextChanged_1(object sender, EventArgs e)
        {
            MostrarDatos(txtBuscar.Text);
        }
    }
}
