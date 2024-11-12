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
    public partial class frmCrearUsuario : Form
    {
      E_Usuarios usuarios = new E_Usuarios();
      N_Usuarios n_usuarios = new N_Usuarios();
        public frmCrearUsuario()
        {
            InitializeComponent();
        }
       private void Limpiarcajas()
        {
            tNombre.Clear();
            cmbRol.SelectedIndex = -1;
            txtClave.Clear();
        }

        private void btnCancelar_Click_1(object sender, EventArgs e)
        {
            Limpiarcajas();
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            try { 

            if(tNombre.Text == "" || cmbRol.Text=="" || txtClave.Text=="") {
                FrmError frmError = new FrmError("POR FAVOR LLENA LOS CAMPOS😢");
                frmError.ShowDialog();
                }
                else
            {
                    usuarios.Usuario1 = tNombre.Text;
                    usuarios.Rol1 = cmbRol.Text;
                    usuarios.Clave1 = txtClave.Text;
                    n_usuarios.InsertarUsuario(usuarios);
                    FrmExitoso.ConfirmacionForm("SE GUARDO CORRECTAMENTE. 😊👌");
                    FrmUsuarios frm = Application.OpenForms.OfType<FrmUsuarios>().FirstOrDefault();
                    if (frm != null)
                    {
                        frm.comportamientodgv(""); // Recarga los datos
                        frm.BringToFront();    // Trae el formulario al frente (opcional)
                    }
                    this.Close();
                   
                }
            }
            catch(Exception ex) 
            {
                FrmError frmError = new FrmError("Error: " + ex.Message);
                frmError.ShowDialog();
            }
        }

        
    }
}
