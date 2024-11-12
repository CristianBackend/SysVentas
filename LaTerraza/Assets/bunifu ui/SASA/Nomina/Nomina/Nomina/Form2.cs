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
    public partial class Form2 : Form
    {
        OracleConnection ora = new OracleConnection("User Id=SYSTEM;Password=1935;Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=CristianAS)(PORT=1521))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=XE)))");

        public Form2()
        {
            InitializeComponent();
        }

        private void iconButton5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void iconButton2_Click(object sender, EventArgs e)
        {
            AgregarUsuario agregar = new AgregarUsuario();
            agregar.Show();
        }

        private void iconButton4_Click(object sender, EventArgs e)
        {
            EditarUsuario editar = new EditarUsuario(); editar.Show();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            ora.Open();
            OracleCommand commando = new OracleCommand("MostrarUsuarios", ora);
            commando.CommandType = System.Data.CommandType.StoredProcedure;
            commando.Parameters.Add("registros", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            OracleDataAdapter adapter = new OracleDataAdapter();
            adapter.SelectCommand = commando;
            DataTable tabla = new DataTable();
            adapter.Fill(tabla);
            dataGridView1.DataSource = tabla;
            ora.Close();
        }
    }
}
