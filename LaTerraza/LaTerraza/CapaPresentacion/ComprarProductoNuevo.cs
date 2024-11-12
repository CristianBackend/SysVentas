using CapaEntidades;
using CapaNegocio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace CapaPresentacion
{
    public partial class ComprarProductoNuevo : Form
    {
        N_Compras n_Compras = new N_Compras();
        private decimal totalOriginal = 0;

        public ComprarProductoNuevo()
        {
            InitializeComponent();
        }

        public void ListarCombobox()
        {
            N_Proveedor prov = new N_Proveedor();
            cmbProveedores.DataSource = prov.ListandoProveedores("");
            cmbProveedores.ValueMember = "Id_Proveedor1";
            cmbProveedores.DisplayMember = "Nombre1";
        }

        private void ComprarProductoNuevo_Load(object sender, EventArgs e)
        {
            ListarCombobox();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                // Verifica que haya al menos una fila en el DataGridView
                if (dgvListaCompras.Rows.Count == 0 || (dgvListaCompras.Rows.Count == 1 && dgvListaCompras.Rows[0].IsNewRow))
                {
                    FrmError frmError = new FrmError("No se puede ingresar una compra sin productos. Por favor agrega productos.");
                    frmError.ShowDialog();
                    return;
                }
                // Verifica que todos los campos sean válidos
                if (cmbEstado.SelectedIndex == -1)
                {
                    FrmError frmError = new FrmError("Por Favor Selecciona Un Estado De Credito O Contado");
                    frmError.ShowDialog(); return;
                }

                E_Compras compra = new E_Compras
                {
                    IdProveedor = (int)cmbProveedores.SelectedValue,
                    Fecha = dtpFecha.Value,
                    Detalles = new List<DetalleCompra>(),
                    Estado = cmbEstado.Text
                };

                decimal total = 0;

                foreach (DataGridViewRow row in dgvListaCompras.Rows)
                {
                    if (row.IsNewRow) continue;

                    var descripcion = row.Cells["Descripcion"].Value?.ToString();
                    if (string.IsNullOrWhiteSpace(descripcion))
                    {
                        FrmError frmError = new FrmError("LA DESCRIPCION NO PUEDE ESTAR VACIA");
                        frmError.ShowDialog(); return;
                    }

                    var cantidad = Convert.ToInt32(row.Cells["Cantidad"].Value ?? 0);
                    if (cantidad < 0)
                    {
                        FrmError frmError = new FrmError("CANTIDAD NO PUEDE SER NEGATIVA");
                        frmError.ShowDialog();
                        return;
                    }

                    var precioUnitario = Convert.ToDecimal(row.Cells["Precio_Unitario"].Value ?? 0);
                    if (precioUnitario < 0)
                    {
                        FrmError frmError = new FrmError("PRECIO NO PUEDE SER NEGATIVO");
                        frmError.ShowDialog(); return;
                    }
                    if (string.IsNullOrWhiteSpace(txtTotal.Text))
                    {
                        FrmError frmError = new FrmError("Compruebe Que Esten Todos Los Datos");
                        frmError.ShowDialog(); return;
                    }

                    var detalle = new DetalleCompra
                    {
                        DescripcionProducto = descripcion,
                        Cantidad = cantidad,
                        PrecioUnitario = precioUnitario,
                        Subtotal = cantidad * precioUnitario
                    };

                    compra.Detalles.Add(detalle);
                    total += detalle.Subtotal;
                }

                // Asigna el total calculado y actualiza txtTotal
                compra.Total = total;
                totalOriginal = total;
                txtTotal.Text = total.ToString("0.00"); // Usa total directamente aquí

                // Insertar compra utilizando capa de negocio
                n_Compras.InsertarCompra(compra);

                FrmExitoso.ConfirmacionForm("¡Compra guardada correctamente!");
                FrmCompras frm = Application.OpenForms.OfType<FrmCompras>().FirstOrDefault();
                if (frm != null)
                {
                    frm.CargarDatos(); // Recarga los datos
                    frm.BringToFront();    // Trae el formulario al frente (opcional)
                }
                dgvListaCompras.Rows.Clear();
                txtTotal.Clear();
                Limpiar();


            }
            catch (Exception ex)
            {
                FrmError frmError = new FrmError("Error: " + ex.Message);
                frmError.ShowDialog();
            }
        }

        private void dgvListaCompras_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dgvListaCompras.Columns["Cantidad"].Index ||
        e.ColumnIndex == dgvListaCompras.Columns["Precio_Unitario"].Index)
            {
                CalcularSubtotalPorFila(e.RowIndex);
            }
            ActualizarTotalGeneral();

        }

        private void CalcularSubtotalPorFila(int rowIndex)
        {
            if (rowIndex < 0 || rowIndex >= dgvListaCompras.Rows.Count) return;

            var row = dgvListaCompras.Rows[rowIndex];
            var cantidad = Convert.ToInt32(row.Cells["Cantidad"].Value ?? 0);
            var precioUnitario = Convert.ToDecimal(row.Cells["Precio_Unitario"].Value ?? 0);
            var subtotal = cantidad * precioUnitario;

            // Asegúrate de que Sub_Total es una columna de solo lectura
            if (row.Cells["Sub_Total"].ReadOnly == false)
            {
                row.Cells["Sub_Total"].Value = subtotal; // Solo se debe calcular aquí
            }
        }

        private void ActualizarTotalGeneral()
        {
            decimal total = 0;

            foreach (DataGridViewRow row in dgvListaCompras.Rows)
            {
                if (row.IsNewRow) continue;

                // Intenta obtener el subtotal desde la fila
                var subTotal = row.Cells["Sub_Total"].Value;

                // Asegúrate de que el valor sea convertible a decimal
                if (subTotal != null)
                {
                    total += Convert.ToDecimal(subTotal);
                }
            }

            totalOriginal = total; // Actualiza el total original después de cualquier cambio
            txtTotal.Text = totalOriginal.ToString("0.00"); // Muestra el total actualizado
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            try
            {
                // Validación de campos requeridos con mensajes específicos
                if (string.IsNullOrWhiteSpace(tNombre.Text))
                {
                    FrmError frmError = new FrmError("El campo 'Nombre' está vacío. Por favor ingrésalo.");
                    frmError.ShowDialog();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtCantidad.Text))
                {
                    FrmError frmError = new FrmError("El campo 'Cantidad' está vacío. Por favor ingrésalo.");
                    frmError.ShowDialog();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtPrecio.Text))
                {
                    FrmError frmError = new FrmError("El campo 'Precio' está vacío. Por favor ingrésalo.");
                    frmError.ShowDialog();
                    return;
                }

                if (cmbProveedores.SelectedIndex == -1)
                {
                    FrmError frmError = new FrmError("No has seleccionado un proveedor. Por favor elige uno.");
                    frmError.ShowDialog();
                    return;
                }

                if (cmItebis.SelectedIndex == -1)
                {
                    FrmError frmError = new FrmError("No has seleccionado una tasa de ITBIS. Por favor elige una.");
                    frmError.ShowDialog();
                    return;
                }

                // Obtén los valores de los campos de texto
                var descripcion = tNombre.Text;
                var cantidad = Convert.ToInt32(txtCantidad.Text);
                var precioUnitario = Convert.ToDecimal(txtPrecio.Text);
                var tasaItbis = cmItebis.SelectedItem.ToString(); // Obtiene la tasa ITBIS del ComboBox

                // Calcula el subtotal en base a la tasa de ITBIS seleccionada
                decimal subtotal = cantidad * precioUnitario;
                if (tasaItbis == "18%")
                {
                    subtotal *= 1.18m; // Aplica un 18%
                }
                else if (tasaItbis == "16%")
                {
                    subtotal *= 1.16m; // Aplica un 16%
                }
                // Si es Exento, el subtotal no cambia

                // Agrega la fila con el subtotal calculado al DataGridView
                dgvListaCompras.Rows.Add(descripcion, cantidad, precioUnitario, subtotal, tasaItbis);

                // Actualiza el total general después de agregar la fila
                ActualizarTotalGeneral();

                // Limpia los campos de entrada
                Limpiar();
            }
            catch (FormatException)
            {
                FrmError frmError = new FrmError("Formato incorrecto en 'Cantidad' o 'Precio'. Por favor ingresa valores numéricos.");
                frmError.ShowDialog();
            }
            catch (Exception ex)
            {
                FrmError frmError = new FrmError("Error al cargar el producto: " + ex.Message);
                frmError.ShowDialog();
            }
        }

        private void Limpiar()
        {
      
            tNombre.Clear();
            txtCantidad.Clear();
            txtPrecio.Clear();
            cmbEstado.SelectedIndex = -1;

        }
        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
         
            if (dgvListaCompras.SelectedRows.Count > 0)
            {
                
                dgvListaCompras.Rows.RemoveAt(dgvListaCompras.SelectedRows[0].Index);

         
                ActualizarTotalGeneral();
                Limpiar();
            }
            else
            {
                FrmError frmError = new FrmError("SELECCIONE UNA FILA");
                frmError.ShowDialog();
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }
    }
}