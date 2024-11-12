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
    public partial class FrmAgregarExtras : Form
    {
        E_Extras extras = new E_Extras();   
        N_Extras N_extras = new N_Extras();
        public FrmAgregarExtras()
        {
            InitializeComponent();
        }


         private void LimpiarCajas()
        {
            tNombre.Clear();
            tDescripcion.Clear();
            tPrecio.Clear();    
        }
        private void btnIngresar_Click(object sender, EventArgs e)
        {
            try
            {
                // Primero verificamos si hay campos vacíos
                if (string.IsNullOrWhiteSpace(tNombre.Text) || string.IsNullOrWhiteSpace(tDescripcion.Text) || string.IsNullOrWhiteSpace(tPrecio.Text))
                {
                    // Mostrar mensaje de error si falta algún campo
                    FrmError frmError = new FrmError("Por favor llene los campos.");
                    frmError.ShowDialog();
                }
                else
                {
                    // Verificar si el precio es un número positivo
                    if (!decimal.TryParse(tPrecio.Text, out decimal precio) || precio <= 0)
                    {
                        FrmError frmError = new FrmError("Por favor ingrese un precio válido y mayor a cero.");
                        frmError.ShowDialog();
                        return; // Sale del método si el precio no es válido
                    }

                    // Si todos los campos están llenos y el precio es válido, procedemos a guardar
                    extras.Extra1 = tNombre.Text;
                    extras.Descripcion1 = tDescripcion.Text;
                    extras.PrecioDelExtra1 = precio; // Asignamos el precio como decimal
                    N_extras.InsertarExtra(extras);

                    FrmExitoso.ConfirmacionForm("SE GUARDÓ CORRECTAMENTE. 😊👌");

                    // Limpiar campos después de guardar
                    LimpiarCajas();

                    // Recargar el formulario de categorías si está abierto
                    FrmExtras frm = Application.OpenForms.OfType<FrmExtras>().FirstOrDefault();
                    if (frm != null)
                    {
                        frm.MostrarDatos(""); // Recarga los datos
                        frm.BringToFront();    // Trae el formulario al frente (opcional)
                    }

                    // Cerrar el formulario actual
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                FrmError frmError = new FrmError("Error: " + ex.Message);
                frmError.ShowDialog();
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            LimpiarCajas();
        }
    }
}
