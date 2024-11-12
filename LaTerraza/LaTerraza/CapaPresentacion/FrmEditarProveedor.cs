using CapaEntidades;
using CapaNegocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CapaPresentacion
{
    public partial class FrmEditarProveedor : Form
    {
        E_Proveedor e_Proveedor = new E_Proveedor();
        N_Proveedor N_Proveedor = new N_Proveedor();
        public FrmEditarProveedor()
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
      
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Limpiar();
        }
        private bool EsTelefonoValido(string telefono)
        {

            Regex regex = new Regex(@"^[0-9\-]+$");
            return regex.IsMatch(telefono);
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            try
            {
                if (tNombre.Text == "" || tCorreo.Text == "" || !EsTelefonoValido(tTel.Text) || tDireccion.Text == "" || !tCorreo.Text.Contains("@"))
                {
                    FrmError frmError = new FrmError("Verifique Nuevamente las Credenciales");
                    frmError.ShowDialog();

                }
                else
                {
                    e_Proveedor.Id_Proveedor1=Convert.ToInt32(tId.Text);
                    e_Proveedor.Correo1 = tCorreo.Text;
                    e_Proveedor.Nombre1 = tNombre.Text;
                    e_Proveedor.Telefono1 = tTel.Text;
                    e_Proveedor.Direccion1 = tDireccion.Text;
                    N_Proveedor.EditarProveedor(e_Proveedor);
                    FrmExitoso.ConfirmacionForm("SE EDITO CORRECTAMENTE. 😊👌");
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
    }
}
