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
    public partial class FrmUsuarios : Form
    {
        N_Usuarios N_Usuarios = new N_Usuarios();
        E_Usuarios e_Usuarios = new E_Usuarios();
      
        public FrmUsuarios()
        {
            InitializeComponent();
        }
        private void btnQuitar_Click(object sender, EventArgs e)
        {
          this.Close();
        }
        private void FrmUsuarios_Load(object sender, EventArgs e)
        {
            comportamientodgv("");
            dgvUsuarios.ClearSelection();
        }

        //Configuracion De El Datagridview
        public void comportamientodgv(string Buscar)
        {
            dgvUsuarios.DataSource = N_Usuarios.ListandoUsuarios(Buscar);
            dgvUsuarios.Columns[0].HeaderText = "ID Del Usuario";
            dgvUsuarios.Columns[1].HeaderText = "Nombre";
            dgvUsuarios.Columns[2].HeaderText = "Rol";
            dgvUsuarios.Columns[3].HeaderText = "Clave";
        }

        private void btnAgregarUsuario_Click(object sender, EventArgs e)
        {
            frmCrearUsuario frmUsuarios = new frmCrearUsuario();    
            frmUsuarios.ShowDialog();
        }

        //Esto es para limpiar la seleccion despues del CRUD
        private void dgvUsuarios_BindingContextChanged(object sender, EventArgs e)
        {
            dgvUsuarios.ClearSelection();
           
        }

        private void btnEliminarUsuario_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvUsuarios.SelectedRows.Count == 0 || dgvUsuarios.CurrentRow.Index == -1)
                {
                    FrmError frmErrorr = new FrmError("Seleccione La Fila");
                     frmErrorr.ShowDialog();
                    return;
                }
                DialogResult resultado = new DialogResult();
                FrmError frmError = new FrmError("¿Seguro que quieres eliminar este usuario? 😢");
                resultado = frmError.ShowDialog();

                if (resultado == DialogResult.OK)
                {
                    e_Usuarios.Id_Usuario1 = Convert.ToInt32(dgvUsuarios.SelectedRows[0].Cells[0].Value);
                    N_Usuarios.EliminarUsuario(e_Usuarios.Id_Usuario1);
                    FrmExitoso.ConfirmacionForm("Se eliminó correctamente. 😒");
                    comportamientodgv("");
                    dgvUsuarios.ClearSelection();
                }
            }
            catch (Exception ex)
            {
                FrmError frmError = new FrmError("Error: " + ex.Message);
                frmError.ShowDialog();
            }
        }

        private void btnEditarUsuario_Click(object sender, EventArgs e)
        {
            try
            {

                if (dgvUsuarios.SelectedRows.Count == 0 || dgvUsuarios.CurrentRow.Index == -1)
                {
                    FrmError frmError = new FrmError("Por favor, selecciona una fila. 😊");
                    frmError.ShowDialog();
                    return;
                }
                else {
                    FrmEditarUsuario frmEditarUsuario = new FrmEditarUsuario();
               
                    frmEditarUsuario.txtId.Text = dgvUsuarios.CurrentRow.Cells[0].Value.ToString();
                    frmEditarUsuario.tNombre.Text = dgvUsuarios.CurrentRow.Cells[1].Value.ToString();
                    frmEditarUsuario.cmbRol.Text= dgvUsuarios.CurrentRow.Cells[2].Value.ToString();
                    frmEditarUsuario.txtClave.Text = dgvUsuarios.CurrentRow.Cells[3].Value.ToString();
                    frmEditarUsuario.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                FrmError frmError = new FrmError("Ocurrió un error: " + ex.Message);
                frmError.ShowDialog();
            }
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            comportamientodgv(txtBuscar.Text);
        }
    }
}
