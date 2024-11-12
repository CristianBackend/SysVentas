using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using CapaNegocio;
using CapaEntidades;

namespace CapaPresentacion
{
    public partial class FrmVentas : Form
    {
        N_Productos productos = new N_Productos();
        E_Productos n_productos = new E_Productos();
     

        public FrmVentas()
        {
            InitializeComponent();
            ConfigurarDataGridViewVentas();
            CargarProductos();
        }
        public void ListarCombobox()
        {
            N_Categoria n_Categoria = new N_Categoria();
            cmbCategoria.DataSource = n_Categoria.ListandoCategoria("");
            cmbCategoria.ValueMember = "Id_Categoria1";
            cmbCategoria.DisplayMember = "Nombre_Categoria1";
           
        }

        private void CargarProductos()
        {
            DataTable dtProductos = productos.MostrarProductos();
            flowLayoutPanelProductos.Controls.Clear();

            foreach (DataRow row in dtProductos.Rows)
            {
                if (row["Imagen"] != DBNull.Value)
                {
                    // Crear panel para cada producto
                    Panel panelProducto = new Panel
                    {
                        Width = 200,
                        Height = 250,
                        Margin = new Padding(10)
                    };

                    // Crear PictureBox para la imagen
                    PictureBox pictureBox = new PictureBox
                    {
                        Width = 180,
                        Height = 180,
                        SizeMode = PictureBoxSizeMode.Zoom,
                        Image = ConvertirImagen((byte[])row["Imagen"]),
                        Cursor = Cursors.Hand
                    };
                    pictureBox.Click += (s, e) => AgregarProductoAlDataGridView(row);
                    panelProducto.Controls.Add(pictureBox);

                    // Crear Label para el nombre del producto
                    Label labelNombre = new Label
                    {
                        Text = row["Nombre"].ToString(),
                        TextAlign = ContentAlignment.MiddleCenter,
                        Dock = DockStyle.Bottom,
                        Font = new Font("Leelawadee UI", 11.25F, FontStyle.Bold),
                        AutoSize = false,
                        Width = panelProducto.Width,
                        Cursor = Cursors.Hand
                    };
                    labelNombre.Click += (s, e) => AgregarProductoAlDataGridView(row);
                    panelProducto.Controls.Add(labelNombre);

                    panelProducto.Tag = row;

                    // Agregar panel al FlowLayoutPanel
                    flowLayoutPanelProductos.Controls.Add(panelProducto);
                }
            }
        }

        private Image ConvertirImagen(byte[] imagenBytes)
        {
            try
            {
                using (MemoryStream ms = new MemoryStream(imagenBytes))
                {
                    return Image.FromStream(ms);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al convertir imagen: {ex.Message}");
                return null;
            }
        }

        private void ConfigurarDataGridViewVentas()
        {
            dgvVentas.Columns.Add("Id_Producto", "ID");
            dgvVentas.Columns.Add("Nombre", "Nombre");
            dgvVentas.Columns.Add("PrecioProducto", "Precio Unitario Producto");
            dgvVentas.Columns.Add("Cantidad", "Cantidad");
            dgvVentas.Columns.Add("SubtotalProducto", "Subtotal Producto");
            dgvVentas.Columns.Add("Extra", "Extra");
            dgvVentas.Columns.Add("PrecioExtra", "Precio Unitario Extra");
            dgvVentas.Columns.Add("CantidadExtras", "Cantidad de Extras");
            dgvVentas.Columns.Add("SubtotalExtras", "Subtotal Extras");
            dgvVentas.Columns.Add("Total", "Total");

            // Ocultar columna ID
            dgvVentas.Columns["Id_Producto"].Visible = false;

            // Configurar el modo de ajuste automático de columnas
            dgvVentas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            // Estilos para los encabezados de las columnas
            dgvVentas.EnableHeadersVisualStyles = false;
            dgvVentas.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            dgvVentas.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 10F, FontStyle.Bold);
            dgvVentas.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // Ajustar el alto de los encabezados
            dgvVentas.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;

        }

        private void AgregarProductoAlDataGridView(DataRow producto)
        {
            using (FrmCantidadYExtras formCantidadYExtras = new FrmCantidadYExtras())
            {
                if (formCantidadYExtras.ShowDialog() == DialogResult.OK)
                {
                    int idProducto = Convert.ToInt32(producto["Id_Producto"]);
                    string nombre = producto["Nombre"].ToString();
                    decimal precioProducto = Convert.ToDecimal(producto["Precio_Venta"]);
                    int cantidad = formCantidadYExtras.Cantidad;

                    // Verificar si el extra seleccionado es válido
                    string extra = formCantidadYExtras.ExtraSeleccionado;
                    decimal precioExtra = 0;
                    int cantidadExtras = 0;

                    // Verificar si el extra es diferente de "No Lleva Extras"
                    if (extra != "No lleva extra")
                    {
                        precioExtra = formCantidadYExtras.PrecioExtra;  // Asignar el precio del extra
                        cantidadExtras = formCantidadYExtras.CantidadExtras;  // Asignar la cantidad de extras
                    }
                    else
                    {
                        // Si el extra es "No Lleva Extras", asegurarse de que el precio y la cantidad de extras sean 0
                        precioExtra = 0;
                        cantidadExtras = 0;
                    }
                    // Calcular subtotales y total general
                    decimal subtotalProducto = precioProducto * cantidad;
                    decimal subtotalExtras = precioExtra * cantidadExtras;
                    decimal totalPrecio = subtotalProducto + subtotalExtras;

                    // Verificar si el producto con el mismo extra ya está en el DataGridView
                    bool productoEncontrado = false;
                    foreach (DataGridViewRow row in dgvVentas.Rows)
                    {
                        if (Convert.ToInt32(row.Cells["Id_Producto"].Value) == idProducto && row.Cells["Extra"].Value.ToString() == extra)
                        {
                            // Sumar la nueva cantidad al producto existente
                            int cantidadExistente = Convert.ToInt32(row.Cells["Cantidad"].Value);
                            int cantidadExtrasExistente = Convert.ToInt32(row.Cells["CantidadExtras"].Value);

                            // Actualizar cantidades y subtotales
                            row.Cells["Cantidad"].Value = cantidadExistente + cantidad;
                            row.Cells["CantidadExtras"].Value = cantidadExtrasExistente + cantidadExtras;
                            row.Cells["SubtotalProducto"].Value = (cantidadExistente + cantidad) * precioProducto;
                            row.Cells["SubtotalExtras"].Value = precioExtra * (cantidadExtrasExistente + cantidadExtras);
                            row.Cells["Total"].Value = ((cantidadExistente + cantidad) * precioProducto) + (precioExtra * (cantidadExtrasExistente + cantidadExtras));

                            productoEncontrado = true;
                            break;
                        }
                    }

                    // Si no se encontró el producto con el mismo extra, agregar una nueva fila
                    if (!productoEncontrado)
                    {
                        dgvVentas.Rows.Add(idProducto, nombre, precioProducto, cantidad, subtotalProducto, extra, precioExtra, cantidadExtras, subtotalExtras, totalPrecio);
                    }
                }
            }
        }

        private void dgvVentas_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvVentas.Columns[e.ColumnIndex].Name == "Cantidad")
            {
                int cantidad = Convert.ToInt32(dgvVentas.Rows[e.RowIndex].Cells["Cantidad"].Value);
                decimal precio = Convert.ToDecimal(dgvVentas.Rows[e.RowIndex].Cells["Precio"].Value);
                dgvVentas.Rows[e.RowIndex].Cells["Total"].Value = cantidad * precio;
            }
        }

        private void btnQuitar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmVentas_Load(object sender, EventArgs e)
        {
            ListarCombobox();
        }
    }
}