using CapaEntidades;
using CapaNegocio;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace CapaPresentacion
{
    public partial class FrmGuardarProductos : Form
    {
        private byte[] imagenProducto; // Variable para almacenar la imagen cargada
        N_Productos productos = new N_Productos();
        E_Productos proc = new E_Productos();

        public FrmGuardarProductos()
        {
            InitializeComponent();
        }

        private void FrmGuardarProductos_Load(object sender, EventArgs e)
        {
            ListarCombobox();
        }

        private void iconButton10_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void ListarCombobox()
        {
            N_Proveedor prov = new N_Proveedor();
            N_Categoria n_Categoria = new N_Categoria();
            cmbCategoria.DataSource = n_Categoria.ListandoCategoria("");
            cmbCategoria.ValueMember = "Id_Categoria1";
            cmbCategoria.DisplayMember = "Nombre_Categoria1";
        }

        // Método que se llama al hacer clic en el botón específico para cargar la imagen
        private void button1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Archivos de Imagen|*.jpg;*.png"; // Filtro para JPG y PNG
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    imagenProducto = File.ReadAllBytes(ofd.FileName);
                    // Almacenar la imagen en la variable
                    FrmExitoso.ConfirmacionForm("Imagen cargada correctamente. 😊");
                    pictureBox1.Image = ConvertirImagen(imagenProducto);


                }
            }
        }
       private Image ConvertirImagen(byte[] imagenBytes)
{
    try
    {
        using (MemoryStream ms = new MemoryStream(imagenBytes))
        {
            Image image = Image.FromStream(ms);

            // Ajustar el tamaño de la imagen si es necesario
            int width = 300; // Ancho deseado
            int height = 270; // Alto deseado
            return new Bitmap(image, new Size(width, height)); // Redimensiona la imagen
        }
    }
    catch (ArgumentException)
    {
      MessageBox.Show("La imagen no es válida.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
       return null;
    }
}

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            // Verificar que los campos estén completos, incluyendo la imagen
            if (string.IsNullOrWhiteSpace(tNombre.Text) || string.IsNullOrWhiteSpace(tDescripcion.Text) || imagenProducto == null || string.IsNullOrWhiteSpace(ttPrecioventa.Text))
            {
                FrmError frmError = new FrmError("Llene Los Campos");
                frmError.ShowDialog();
            }
            else
            {
                try
                {
                    E_Productos nuevoProducto = new E_Productos
                    {
                        Nombre_Producto1 = tNombre.Text,
                        Descripcion_Producto1 = tDescripcion.Text,
                        Precio_Venta_Producto1 = Convert.ToDecimal(ttPrecioventa.Text),
                        Imagen = imagenProducto, // Asigna la imagen cargada
                        P_Id_Categoria1 = Convert.ToInt32(cmbCategoria.SelectedValue)
                    };

                    productos.AgregarProducto(nuevoProducto);
                    FrmExitoso.ConfirmacionForm("SE GUARDO CORRECTAMENTE. 😊👌");

                    FrmInventario frm = Application.OpenForms.OfType<FrmInventario>().FirstOrDefault();
                    if (frm != null)
                    {
                        frm.comportamientodeldgv(); // Recarga los datos
                        frm.BringToFront(); // Trae el formulario al frent
                        frm.dgvProductos.Refresh(); // Forzar un refresh del DataGridView
                    }
                    this.Close();

                    imagenProducto = null; // Restablecer la imagen cargada después de guardar
                }
                catch (Exception ex)
                {
                    FrmError frmError = new FrmError("Error: " + ex.Message);
                    frmError.ShowDialog();
                }
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void Limpiar()
        {
            tNombre.Text = "";
            tDescripcion.Text = "";
            ttPrecioventa.Text = "";
            cmbCategoria.SelectedIndex = -1;

        }
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Limpiar();
       
        }
    }
}

