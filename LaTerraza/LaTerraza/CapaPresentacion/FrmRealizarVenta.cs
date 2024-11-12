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
using System.Windows;
using System.Windows.Forms;

namespace CapaPresentacion
{
    public partial class FrmRealizarVenta : Form
    {
        E_Ventas E_Ventas = new E_Ventas(); 
        public FrmRealizarVenta()
        {
            InitializeComponent();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
               
                E_Ventas ventas = new E_Ventas();
                ventas.Id_Producto1 = Convert.ToInt32(tId.Text);
                ventas.Cantidad1 = Convert.ToInt32(tCantidad.Text);
                ventas.Cliente1 = tCliente.Text;
                ventas.Precio_Unitario1 = Convert.ToDecimal(tPrecio.Text);

                
                decimal total = ventas.Cantidad1 * ventas.Precio_Unitario1;
                ventas.Total1 = total;

                ventas.Tipo_Pago1 = cmbTipodepago.SelectedItem.ToString();
                ventas.Fecha1 = ttfecha.Text;

                N_Ventas negocio = new N_Ventas();
                negocio.InsertarVenta(ventas);  

                FrmExitoso.ConfirmacionForm("¡Has vendido correctamente! 😊👌");
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Ha ocurrido un error: " + ex.Message);
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            
        }

        private void FrmRealizarVenta_Load(object sender, EventArgs e)
        {
            ttfecha.Text = dateTimePicker1.Value.ToString("yyyy-MM-dd");
        }
    }
}
