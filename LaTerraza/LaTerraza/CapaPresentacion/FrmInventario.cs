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
using System.IO;

namespace CapaPresentacion
{
    public partial class FrmInventario : Form
    {
        private Dictionary<int, Image> imagenCache = new Dictionary<int, Image>();

        N_Productos N_Productos =new N_Productos();
        E_Productos E_Productos =new E_Productos();
        
        public FrmInventario()
        {
            InitializeComponent();
        }

        private void btnQuitar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmInventario_Load(object sender, EventArgs e)
        {
          
            comportamientodeldgv();
        }
        public void LimpiarCache()
        {
            imagenCache.Clear();
        }
        public void comportamientodeldgv()
        {
            imagenCache.Clear(); // Limpia la caché una vez al cargar los datos

            DataTable dt = N_Productos.MostrarProductos();
            dgvProductos.DataSource = dt;
            dgvProductos.CellFormatting += DgvProductos_CellFormatting;

            // Configuración de columnas
            dgvProductos.Columns[1].Visible = false;
            dgvProductos.Columns[2].Visible = false;
            dgvProductos.Columns[3].Visible = false;
            dgvProductos.Columns[7].Visible = false;
            dgvProductos.Columns["Codigo"].Width = 80;
            dgvProductos.Columns["Categoria"].Width = 300;
            dgvProductos.Columns["Precio_Venta"].HeaderText = "Precio De Venta";
            dgvProductos.Columns["Precio_Venta"].Width = 100;
           
            dgvProductos.Columns[0].Width = 300;
            dgvProductos.Columns[10].DisplayIndex = 0;
            dgvProductos.Columns[0].DisplayIndex = dgvProductos.Columns.Count - 1;
            dgvProductos.DefaultCellStyle.WrapMode = DataGridViewTriState.True;

            dgvProductos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dgvProductos.Columns["Nombre"].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgvProductos.Columns["Descripcion"].DefaultCellStyle.WrapMode = DataGridViewTriState.True;


        }

        public  void DgvProductos_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
         {
            if (dgvProductos.Columns[e.ColumnIndex].Name == "Imagen" && e.Value is byte[] imagenBytes)
            {
                int productoId = (int)dgvProductos.Rows[e.RowIndex].Cells["Id_Producto"].Value;

                if (!imagenCache.TryGetValue(productoId, out Image cachedImage))
                {
                    try
                    {
                        cachedImage = ConvertirImagen(imagenBytes);
                        imagenCache[productoId] = cachedImage;
                    }
                    catch (Exception ex)
                    {
                        FrmError frmError = new FrmError("Error: " + ex.Message);
                        frmError.ShowDialog();
                        return;
                    }
                }

                e.Value = cachedImage;
                e.FormattingApplied = true;

                // Ajusta la altura de la fila según el tamaño de la imagen
                DataGridViewRow row = dgvProductos.Rows[e.RowIndex];
                row.Height = cachedImage.Height + 10;
            }
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

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            buscar(txtBuscar.Text);
        }

        public void buscar(string buscar)
        {
            dgvProductos.DataSource = N_Productos.BuscarProducto(buscar);
        }

        

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            FrmGuardarProductos frm = new FrmGuardarProductos();
            frm.ShowDialog();
        }

        private void dgvProductos_BindingContextChanged(object sender, EventArgs e)
        {
           comportamientodeldgv();
           dgvProductos.Refresh();
          dgvProductos.ClearSelection();  
            
        }
       
        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dgvProductos.SelectedRows.Count > 0)
            {
                int idProducto = Convert.ToInt32(dgvProductos.SelectedRows[0].Cells["Id_Producto"].Value);

                // Crear instancia del formulario de edición
                FrmAgregarProducto formEditar = new FrmAgregarProducto(idProducto);
                formEditar.ShowDialog();

                // (Opcional) Refrescar el DataGridView después de editar
                comportamientodeldgv(); // Método que carga los productos en el DataGridView
            }
            else
            {
                Form message = new FrmError("Seleccione Un Producto");
                message.ShowDialog();

            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvProductos.SelectedRows.Count > 0)
                {
                    // Confirmación antes de eliminar
                    Form message = new FrmError("¿ESTÁS SEGURO DE ELIMINARLO? 😢");
                    DialogResult result = message.ShowDialog();

                    if (result == DialogResult.OK)
                    {
                        // Obtiene el Id de la categoría seleccionada
                        E_Productos.Id_Producto1 = Convert.ToInt32(dgvProductos.SelectedRows[0].Cells["Id_Producto"].Value);


                        // Llama a la función de eliminación de categoría
                        N_Productos.EliminarProducto(E_Productos.Id_Producto1);

                        // Muestra un mensaje de éxito
                        FrmExitoso.ConfirmacionForm("¡ELIMINADO! 😭");
                        comportamientodeldgv();
                    }
                    else
                    {
                        dgvProductos.ClearSelection();
                    }
                }
                else
                {
                    Form message = new FrmError("Seleccione Un Producto");
                    message.ShowDialog();

                }

            }
            catch (Exception ex)
            {
                FrmError frmError = new FrmError("Error: " + ex.Message);
                frmError.ShowDialog();

            }

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }

        private void btnQuitar_Click_1(object sender, EventArgs e)
        {
            this.Close();   
        }
    }
    }
    



