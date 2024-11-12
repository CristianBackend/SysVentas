using CapaEntidades;
using CapaNegocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CapaPresentacion
{
    public partial class AgregarCategoria : Form
    {
        E_Categoria Categoria = new E_Categoria();
        N_Categoria Ncategoria = new N_Categoria();
        public AgregarCategoria()
        {
            InitializeComponent();
        }


        private void LimpiarCajas()
        {
            tNombre.Clear();
            tDescripcion.Clear();
        }
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            LimpiarCajas();
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            try
            {
                // Primero verificamos si hay campos vacíos
                if (string.IsNullOrWhiteSpace(tNombre.Text) || string.IsNullOrWhiteSpace(tDescripcion.Text))
                {
                    // Mostrar mensaje de error si falta algún campo
                    FrmError frmError = new FrmError("Por favor llene los campos.");
                    frmError.ShowDialog();
                }
                else
                {
                    // Si todos los campos están llenos, procedemos a guardar
                    Categoria.Nombre_Categoria1 = tNombre.Text;
                    Categoria.Descripcion_Categoria1 = tDescripcion.Text;
                    Ncategoria.InsertarCategoria(Categoria);

                    FrmExitoso.ConfirmacionForm("SE GUARDÓ CORRECTAMENTE. 😊👌");

                    // Limpiar campos después de guardar
                    LimpiarCajas();

                    // Recargar el formulario de categorías si está abierto
                    FrmCategoria frm = Application.OpenForms.OfType<FrmCategoria>().FirstOrDefault();
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

        private void tDescripcion_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void tNombre_TextChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void iconButton1_Click(object sender, EventArgs e)
        {

        }

        private void iconButton6_Click(object sender, EventArgs e)
        {

        }

        private void iconButton3_Click(object sender, EventArgs e)
        {

        }
    }
}
