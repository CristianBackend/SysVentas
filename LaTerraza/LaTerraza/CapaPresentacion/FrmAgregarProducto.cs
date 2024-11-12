using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CapaNegocio;
using CapaEntidades;

namespace CapaPresentacion
{
    public partial class FrmAgregarProducto : Form
    {
        private byte[] imagenProducto;
        N_Productos N_Productos = new N_Productos();
        private int idProducto;

        public FrmAgregarProducto(int idProducto)
        {
            InitializeComponent();
            this.idProducto = idProducto;
        }


        public FrmAgregarProducto()
        {
            InitializeComponent();
           
        }

        public FrmAgregarProducto(E_Productos producto)
        {
    
        }

        

        private void CargarDatosProducto()
        {
            
        }

        public void ListarCombobox()
        {
            N_Categoria n_Categoria = new N_Categoria();
            cmbCategoria.DataSource = n_Categoria.ListandoCategoria("");
            cmbCategoria.ValueMember = "Id_Categoria1";
            cmbCategoria.DisplayMember = "Nombre_Categoria1";
        }

        private void FrmAgregarProducto_Load(object sender, EventArgs e)
        {
            ListarCombobox();
            var producto = N_Productos.ObtenerProductoPorId(idProducto);

            if (producto != null)
            {
                tId.Text = Convert.ToString(producto.Id_Producto1);
                tNombre.Text = producto.Nombre_Producto1;
                tDescripcion.Text = producto.Descripcion_Producto1;
                tPrecioVenta.Text = producto.Precio_Venta_Producto1.ToString();

                // Convertir la imagen desde la base de datos a formato PictureBox
                if (producto.Imagen != null)
                {
                    using (var ms = new MemoryStream(producto.Imagen))
                    {
                        pictureBox1.Image = Image.FromStream(ms);
                    }
                    // Almacena la imagen original en imagenProducto
                    imagenProducto = producto.Imagen;
                }
            }
        }

        private void iconButton10_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Limpiar()
        {
            tNombre.Text = "";
            tDescripcion.Text = "";
            tPrecioVenta.Text = "";
           cmbCategoria.SelectedIndex = -1;

        }
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Limpiar();
        }
        private Image ConvertirImagen(byte[] imagenBytes)
        {
            using (MemoryStream ms = new MemoryStream(imagenBytes))
            {
                Image image = Image.FromStream(ms);

                // Ajustar el tamaño de la imagen si es necesario
                int width = 290; // Ancho deseado
                int height = 250; // Alto deseado
                return new Bitmap(image, new Size(width, height)); // Redimensiona la imagen
            }
        }
        private void btnCargar_Click(object sender, EventArgs e)
        {
            try { 
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Archivos de Imagen|*.jpg;*.png"; // Filtro para JPG y PNG
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    imagenProducto = File.ReadAllBytes(ofd.FileName);
                    // Muestra la imagen seleccionada en el PictureBox
                    FrmExitoso.ConfirmacionForm("Imagen cargada correctamente. 😊");
                    pictureBox1.Image = ConvertirImagen(imagenProducto);
                }
            }
            }
            catch ( Exception ex)
            {
                FrmError frmError = new FrmError("Error Al Cargar Imagen"+ ex);
                frmError.ShowDialog();
            }
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tNombre.Text) || string.IsNullOrWhiteSpace(tDescripcion.Text) || string.IsNullOrWhiteSpace(tPrecioVenta.Text))
            {
                FrmError frmError = new FrmError("Inserte Los Datos" );
                frmError.ShowDialog();
            }
            else
            {
                try
                {
                    E_Productos nuevoProducto = new E_Productos
                    {
                        Id_Producto1 = Convert.ToInt32(tId.Text),
                        Nombre_Producto1 = tNombre.Text,
                        Descripcion_Producto1 = tDescripcion.Text,
                        Precio_Venta_Producto1 = Convert.ToDecimal(tPrecioVenta.Text),
                        Imagen = imagenProducto ?? N_Productos.ObtenerProductoPorId(idProducto).Imagen, // Usar la imagen cargada o la original
                        P_Id_Categoria1 = Convert.ToInt32(cmbCategoria.SelectedValue)
                    };

                    N_Productos.EditarProducto(nuevoProducto);
                    FrmExitoso.ConfirmacionForm("SE EDITÓ CORRECTAMENTE. 😊👌");

                    // Refrescar DataGridView en FrmInventario
                    FrmInventario frm = Application.OpenForms.OfType<FrmInventario>().FirstOrDefault();
                    if (frm != null)
                    {
                        frm.comportamientodeldgv(); // Recarga los datos
                        frm.BringToFront(); // Trae el formulario al frente
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
        }
}