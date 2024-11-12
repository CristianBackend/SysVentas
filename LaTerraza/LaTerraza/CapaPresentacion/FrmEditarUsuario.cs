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
    public partial class FrmEditarUsuario : Form
    {

        E_Usuarios e_usuarios = new E_Usuarios();
        N_Usuarios n_usuarios = new N_Usuarios();
        public FrmEditarUsuario()
        {
            InitializeComponent();
        }
        private void LimpiarCajas()
        {
            tNombre.Clear();
            cmbRol.SelectedIndex = -1;
            txtClave.Clear();
        }
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            LimpiarCajas();
        }
        private void btnEditar_Click(object sender, EventArgs e)
        {
            try
            {
                if (tNombre.Text == "" || cmbRol.Text == "" || txtClave.Text == "")
                {
                    FrmError frmError = new FrmError("POR FAVOR LLENA LOS CAMPOS😢");
                    frmError.ShowDialog();
                }
                else
                {
                    e_usuarios.Id_Usuario1 = Convert.ToInt32(txtId.Text);
                    e_usuarios.Usuario1 = tNombre.Text;
                    e_usuarios.Rol1 = cmbRol.Text;
                    e_usuarios.Clave1= txtClave.Text;

                    n_usuarios.EditarUsuario(e_usuarios);
                    FrmExitoso.ConfirmacionForm("SE EDITÓ CORRECTAMENTE. 😊👌");
                    FrmUsuarios frm = Application.OpenForms.OfType<FrmUsuarios>().FirstOrDefault();
                    if (frm != null)
                    {
                        frm.comportamientodgv(""); // Recarga los datos
                        frm.BringToFront();    // Trae el formulario al frente (opcional)
                    }
                    this.Close();

                }
            }
            catch (Exception ex)
            {
                FrmError frmError = new FrmError("Error: " + ex.Message);
                frmError.ShowDialog();
            }
        }
    }
}
