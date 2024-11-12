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
    public partial class FrmEditarCategoria : Form
    {
        E_Categoria Categoria = new E_Categoria();
        N_Categoria Ncategoria = new N_Categoria();
        public FrmEditarCategoria()
        {
            InitializeComponent();
        }

        private void limpiarcajas()
        {
            tNombre.Clear();
            tDescripcion.Clear();
        }
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            limpiarcajas();
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(tNombre.Text) || string.IsNullOrWhiteSpace(tId.Text) || string.IsNullOrWhiteSpace(tDescripcion.Text))
                {
                    FrmError frmError = new FrmError("Por favor llene los campos.");
                    frmError.ShowDialog();
                    return; 
                }

       
                Categoria.Id_Categoria1 = Convert.ToInt32(tId.Text);
                Categoria.Nombre_Categoria1 = tNombre.Text;
                Categoria.Descripcion_Categoria1 = tDescripcion.Text;

                Ncategoria.EditarCategoria(Categoria);

                FrmExitoso.ConfirmacionForm("SE EDITÓ CORRECTAMENTE. 😊👌");
                FrmCategoria frm = Application.OpenForms.OfType<FrmCategoria>().FirstOrDefault();
                if (frm != null)
                {
                    frm.MostrarDatos(""); // Recarga los datos
                    frm.BringToFront();    // Trae el formulario al frente (opcional)
                }
                this.Close();
            }
            catch (Exception ex)
            {
                FrmError frmError = new FrmError("Error: " + ex.Message);
                frmError.ShowDialog();
            }
        }

        
    }
}
