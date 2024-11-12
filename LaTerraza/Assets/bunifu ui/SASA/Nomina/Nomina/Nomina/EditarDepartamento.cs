using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nomina
{
    public partial class EditarDepartamento : Form
    {
        OracleConnection ora = new OracleConnection("User Id=SYSTEM;Password=1935;Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=CristianAS)(PORT=1521))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=XE)))");

        public EditarDepartamento()
        {
            InitializeComponent();
        }

        private void txtUsuario_Enter(object sender, EventArgs e)
        {
            if (txtUsuario.Text == "Nombre")
            {
                txtUsuario.Text = "";
                txtUsuario.ForeColor = Color.DimGray;
            }
        }

        private void txtUsuario_Leave(object sender, EventArgs e)
        {
            if (txtUsuario.Text == "")
            {
                txtUsuario.Text = "Nombre";
                txtUsuario.ForeColor = Color.DimGray;
            }
        }

        private void textBox7_Leave(object sender, EventArgs e)
        {
            
        }

        private void textBox7_Enter(object sender, EventArgs e)
        {
           
        }

        private void txtUsuario_TextChanged(object sender, EventArgs e)
        {

        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            AgregarCargos agregar   = new AgregarCargos();
            agregar.Show();
        }
    }
}
