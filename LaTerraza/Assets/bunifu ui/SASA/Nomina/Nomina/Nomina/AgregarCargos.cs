using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oracle.ManagedDataAccess.Client;

namespace Nomina
{
    public partial class AgregarCargos : Form
    {
        OracleConnection ora = new OracleConnection("User Id=SYSTEM;Password=1935;Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=CristianAS)(PORT=1521))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=XE)))");

        public AgregarCargos()
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

        private void iconButton3_Click(object sender, EventArgs e)
        {
            try
            {
                ora.Open();
                OracleCommand command = new OracleCommand("InsertarCargo", ora);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.Add("Cargo", OracleDbType.Varchar2).Value = txtUsuario.Text;
                command.ExecuteNonQuery();
                MessageBox.Show("Cargo Insertado Exitosamente 😊");
               
                this.Close();


            }
            catch
            {
                MessageBox.Show("Erro Al Insertar el Cargo 😕");
                this.Close();
            }
            ora.Close();
        }
    }
    }

