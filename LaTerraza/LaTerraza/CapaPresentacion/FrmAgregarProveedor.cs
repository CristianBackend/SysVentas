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
using System.Text.RegularExpressions;
namespace CapaPresentacion
{
    public partial class FrmAgregarProveedor : Form
    {

        N_Proveedor N_Proveedor=new N_Proveedor();
        E_Proveedor e_Proveedor = new E_Proveedor();
        public FrmAgregarProveedor()
        {
            InitializeComponent();
        }

        

        private void Limpiar()
        {
            tCorreo.Text = "";
            tNombre.Text = "";
            tTel.Text = "";
            tDireccion.Text = "";
        }
        // Funcion pa q permita solo números y guiones
        private bool EsTelefonoValido(string telefono)
        {
           
            Regex regex = new Regex(@"^[0-9\-]+$");
            return regex.IsMatch(telefono);
        }

        
        private void btnCancelar_Click_1(object sender, EventArgs e)
        {
            Limpiar();
            
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            try { 
            if (tNombre.Text == "" || tCorreo.Text == "" || !EsTelefonoValido(tTel.Text) || tDireccion.Text == "" || !tCorreo.Text.Contains("@"))
            {
                FrmError frmError = new FrmError("Verifique Nuevamente las Credenciales");
                frmError.ShowDialog();

            }
            else
            {
                e_Proveedor.Correo1 = tCorreo.Text;
                e_Proveedor.Nombre1 = tNombre.Text;
                e_Proveedor.Telefono1 = tTel.Text;
                e_Proveedor.Direccion1 = tDireccion.Text;
                N_Proveedor.InsertarProveedor(e_Proveedor);
                FrmExitoso.ConfirmacionForm("SE GUARDO CORRECTAMENTE. 😊👌");
                FrmProveedores frm = Application.OpenForms.OfType<FrmProveedores>().FirstOrDefault();
                if (frm != null)
                {
                    frm.MostrarDatos(""); // Recarga los datos
                    frm.BringToFront();    // Trae el formulario al frente (opcional)
                }
                this.Close();
                Limpiar();

            }
            }
            catch (Exception ex)
            {
                FrmError frmError = new FrmError("Error:" + ex);
                frmError.ShowDialog();
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }
    }
}
