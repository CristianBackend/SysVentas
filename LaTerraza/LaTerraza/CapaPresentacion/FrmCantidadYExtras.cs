using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace CapaPresentacion
{
    public partial class FrmCantidadYExtras : Form
    {
        public int Cantidad { get; private set; }
        public string ExtraSeleccionado { get; private set; }
        public decimal PrecioExtra { get; private set; }
        public int CantidadExtras { get; private set; }

        public class ExtraItem
        {
            public string Nombre { get; set; }
            public decimal Precio { get; set; }

            public override string ToString() => Nombre; // Esto hace que el `ComboBox` muestre solo el nombre
        }
        public FrmCantidadYExtras()
        {
            InitializeComponent();
            CargarExtras();
            numCantidad.Minimum = 1;
            numCantidadExtras.Minimum = 1;
            cmbExtras.SelectedIndexChanged += cmbExtras_SelectedIndexChanged_1;
        }

        private void CargarExtras()
        {
            using (SqlConnection conexion = new SqlConnection("Server=DESKTOP-QP4FDV3\\SQLDEVELOPER;Integrated Security=yes; Database=Sistema_LaTerraza"))
            {
                conexion.Open();
                SqlCommand comando = new SqlCommand("SELECT Extra, Precio FROM Extras", conexion);
                SqlDataReader reader = comando.ExecuteReader();

                while (reader.Read())
                {
                    cmbExtras.Items.Add(new ExtraItem
                    {
                        Nombre = reader["Extra"].ToString(),
                        Precio = (decimal)reader["Precio"]
                    });
                }
                cmbExtras.SelectedIndex = 0;
            }
        }


        private void btnAceptar_Click_1(object sender, EventArgs e)
        {
            Cantidad = (int)numCantidad.Value;
            ExtraItem extraSeleccionado = cmbExtras.SelectedItem as ExtraItem;
            ExtraSeleccionado = extraSeleccionado.Nombre;
            PrecioExtra = extraSeleccionado.Precio;
            CantidadExtras = (int)numCantidadExtras.Value;
            DialogResult = DialogResult.OK;
        }

        private void cmbExtras_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (cmbExtras.SelectedIndex == 0)
            {
                numCantidadExtras.Enabled = false;
                PrecioExtra = 0;
            }
            else
            {
                var extra = (dynamic)cmbExtras.SelectedItem;
                PrecioExtra = extra.Precio;
                numCantidadExtras.Enabled = true;
            }

        }
    }
}