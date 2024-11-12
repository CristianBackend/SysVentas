using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CapaNegocio;
using CapaEntidades;


namespace CapaPresentacion
{
    public partial class FrmCategoria : Form
    {



        private string Idcategoria;
        private bool Editar = false;
        E_Categoria Categoria = new E_Categoria();
        N_Categoria Ncategoria = new N_Categoria();

        public FrmCategoria()
        {
            InitializeComponent();
        }


        private void btnQuitar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmCategoria_Load(object sender, EventArgs e)
        {
            MostrarDatos("");

            dgvCategoria.Columns[0].Visible = false;
            dgvCategoria.Columns[1].HeaderText = "Codigo";
            dgvCategoria.Columns[2].HeaderText = "Nombre";
            dgvCategoria.Columns[3].HeaderText = "Descripcion";



        }

        public void MostrarDatos(string Buscar)
        {
            N_Categoria Categoria = new N_Categoria();
            dgvCategoria.DataSource = Categoria.ListandoCategoria(Buscar);
            dgvCategoria.ClearSelection();
        }





        private void dgvCategoria_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dgvCategoria.ClearSelection();
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            AgregarCategoria agregarCategoria = new AgregarCategoria();
            agregarCategoria.ShowDialog();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {

            FrmEditarCategoria frm = new FrmEditarCategoria();
            try
            {
                if (dgvCategoria.SelectedRows.Count > 0)
                {

                    frm.tId.Text = dgvCategoria.CurrentRow.Cells[0].Value.ToString();
                    frm.tNombre.Text = dgvCategoria.CurrentRow.Cells[2].Value.ToString();
                    frm.tDescripcion.Text = dgvCategoria.CurrentRow.Cells[3].Value.ToString();
                    frm.ShowDialog();

                }
                else
                {
                    FrmError frmError = new FrmError("Seleccione La Fila Que Desea Editar.");
                    frmError.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                FrmError frmError = new FrmError("Error: " + ex.Message);
                frmError.ShowDialog();
            }

        }

        private void txtBuscar_TextChanged_1(object sender, EventArgs e)
        {
            MostrarDatos(txtBuscar.Text);
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvCategoria.SelectedRows.Count > 0)
                {
                    // Confirmación antes de eliminar
                    Form message = new FrmError("¿ESTÁS SEGURO DE ELIMINARLA? 😢");
                    DialogResult result = message.ShowDialog();

                    if (result == DialogResult.OK)
                    {
                        // Obtiene el Id de la categoría seleccionada
                        Categoria.Id_Categoria1 = Convert.ToInt32(dgvCategoria.SelectedRows[0].Cells[0].Value);

                        // Llama a la función de eliminación de categoría
                        Ncategoria.EliminarCategoria(Categoria);

                        // Muestra un mensaje de éxito
                        FrmExitoso.ConfirmacionForm("¡ELIMINADO! 😭");
                        MostrarDatos("");
                    }
                    else
                    {
                        // Muestra un mensaje de error si el usuario cancela
                        FrmError frmError = new FrmError("Seleccione La Fila.");
                        frmError.ShowDialog();
                    }
                }
                else
                {
                    // Muestra un mensaje si no hay filas seleccionadas
                    FrmError frmError = new FrmError("Seleccione una fila.");
                    frmError.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                FrmError frmError = new FrmError("Error: " + ex.Message);
                frmError.ShowDialog();

            }

        }

        private void Extras_Click(object sender, EventArgs e)
        {
           FrmExtras frmExtras = new FrmExtras();   
          frmExtras.ShowDialog();
        }
    }
}


    
    

