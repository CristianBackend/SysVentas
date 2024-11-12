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
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void VerificarUsuarios()
        {
            N_Usuarios n_Usuarios = new N_Usuarios();
            try
            {
              bool loginExitoso = n_Usuarios.VerificarUsuarios(txtUsuario.Text, txtClave.Text);

                if (loginExitoso)
                {
                    
                    FrmPrincipal frm = new FrmPrincipal();               
                    frm.Show();
                    frm.label1.Text = txtUsuario.Text.Split(' ')[0];
                    string usuario= txtUsuario.Text;    
                    FrmExitoso.ConfirmacionForm("BIENVENID@ "+ usuario.ToUpper());
                    this.Hide();
                }
                else
                {
                    FrmError frmError = new FrmError("CREDENCIALES INCORRECTAS😢");
                    frmError.ShowDialog();
                    limpiarcajas();
                }
            }
            catch (Exception ex)
            {
                FrmError frmError = new FrmError("Error: " + ex.Message);
                frmError.ShowDialog();
            }
        }

        private void limpiarcajas()
        {
            txtUsuario.Clear();
            txtClave.Clear();
        }
        private void btnCrearCuenta_Click(object sender, EventArgs e)
        {
            frmCrearUsuario frm = new frmCrearUsuario();
            frm.ShowDialog();                                                   
        }

        private void btnIngresar_Click_1(object sender, EventArgs e)
        {
            VerificarUsuarios();
        }

        private void btnCancelar_Click_1(object sender, EventArgs e)
        {
            limpiarcajas();  
        }

       
        private void frmLogin_Load(object sender, EventArgs e)
        {
            
        }
    }
}
