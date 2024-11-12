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
    public partial class FrmVentasHechas : Form
    {

        N_Ventas N_Ventas=new N_Ventas();
        N_Productos N_Productos=new N_Productos();  
        public FrmVentasHechas()
        {
            InitializeComponent();
        }

        private void MostrarVentas()
        {
            dgvVentasHechas.DataSource = N_Ventas.MostrarVentas();
           
        }

        private void FrmVentasHechas_Load(object sender, EventArgs e)
        {
            MostrarVentas();
        }
        public void buscar(string buscar)
        {
          dgvVentasHechas.DataSource = N_Ventas.BuscarVentas(buscar);
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            buscar(txtBuscar.Text);
        {
            } }

        private void btnQuitar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
