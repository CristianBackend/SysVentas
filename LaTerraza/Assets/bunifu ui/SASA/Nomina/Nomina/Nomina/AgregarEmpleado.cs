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
    public partial class AgregarEmpleado : Form
    {
        OracleConnection ora = new OracleConnection("User Id=SYSTEM;Password=1935;Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=CristianAS)(PORT=1521))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=XE)))");

        public AgregarEmpleado()
        {
            InitializeComponent();
        }

        private void txtUsuario_MouseEnter(object sender, EventArgs e)
        {

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

       

        private void textBox1_Enter(object sender, EventArgs e)
        {
            if (textBox1.Text == "Apellido")
            {
                textBox1.Text = "";
                textBox1.ForeColor = Color.DimGray;
            }
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                textBox1.Text = "Apellido";
                textBox1.ForeColor = Color.DimGray;
            }
        }

        private void textBox2_Enter(object sender, EventArgs e)
        {
            if (textBox2.Text == "Edad")
            {
                textBox2.Text = "";
                textBox2.ForeColor = Color.DimGray;
            }
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            if (textBox2.Text == "")
            {
                textBox2.Text = "Edad";
                textBox2.ForeColor = Color.DimGray;
            }
        }

        private void textBox4_Enter(object sender, EventArgs e)
        {
            if (textBox4.Text == "Direccion")
            {
                textBox4.Text = "";
                textBox4.ForeColor = Color.DimGray;
            }
        }

        private void textBox4_Leave(object sender, EventArgs e)
        {
            if (textBox4.Text == "")
            {
                textBox4.Text = "Direccion";
                textBox4.ForeColor = Color.DimGray;
            }
        }

        private void textBox7_Enter(object sender, EventArgs e)
        {
            if (textBox7.Text == "NSS")
            {
                textBox7.Text = "";
                textBox7.ForeColor = Color.DimGray;
            }
        }

        private void textBox7_Leave(object sender, EventArgs e)
        {
            if (textBox7.Text == "")
            {
                textBox7.Text = "NSS";
                textBox7.ForeColor = Color.DimGray;
            }
        }

        private void textBox8_Enter(object sender, EventArgs e)
        {
            if (textBox8.Text == "NIF")
            {
                textBox8.Text = "";
                textBox8.ForeColor = Color.DimGray;
            }
        }

        private void textBox8_Leave(object sender, EventArgs e)
        {
            if (textBox8.Text == "")
            {
                textBox8.Text = "NIF";
                textBox8.ForeColor = Color.DimGray;
            }
        }

        private void textBox9_Enter(object sender, EventArgs e)
        {
            if (textBox9.Text == "Correo")
            {
                textBox9.Text = "";
                textBox9.ForeColor = Color.DimGray;
            }
        }

        private void textBox9_Leave(object sender, EventArgs e)
        {
            if (textBox9.Text == "")
            {
                textBox9.Text = "Correo";
                textBox9.ForeColor = Color.DimGray;
            }
        }

        private void textBox6_Enter(object sender, EventArgs e)
        {
            if (textBox6.Text == "Cedula")
            {
                textBox6.Text = "";
                textBox6.ForeColor = Color.DimGray;
            }
        }

        private void textBox6_Leave(object sender, EventArgs e)
        {
            if (textBox6.Text == "")
            {
                textBox6.Text = "Cedula";
                textBox6.ForeColor = Color.DimGray;
            }
        }

        private void textBox5_Enter(object sender, EventArgs e)
        {
            if (textBox5.Text == "Telefono")
            {
                textBox5.Text = "";
                textBox5.ForeColor = Color.DimGray;
            }
        }

        private void textBox5_Leave(object sender, EventArgs e)
        {
            if (textBox5.Text == "")
            {
                textBox5.Text = "Telefono";
                textBox5.ForeColor = Color.DimGray;
            }
        }

        private void textBox12_Enter(object sender, EventArgs e)
        {
            if (textBox12.Text == "Salario Base")
            {
                textBox12.Text = "";
                textBox12.ForeColor = Color.DimGray;
            }
        }

        private void textBox12_Leave(object sender, EventArgs e)
        {
            if (textBox12.Text == "")
            {
                textBox12.Text = "Salario Base";
                textBox12.ForeColor = Color.DimGray;
            }
        }

        private void iconButton3_Click(object sender, EventArgs e)
        {try
            {
                ora.Open();
                OracleCommand command = new OracleCommand("InsertarEmpleados",ora);
                command.CommandType=System.Data.CommandType.StoredProcedure;
                command.Parameters.Add("Nombre",OracleDbType.Varchar2).Value = txtUsuario.Text;
                command.Parameters.Add("Apellido", OracleDbType.Varchar2).Value = textBox1;
                command.Parameters.Add("Edad", OracleDbType.Varchar2).Value = txtUsuario.Text;


            }
            catch { }
            
        }

        private void AgregarEmpleado_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Add("Hombre");
            comboBox1.Items.Add("Mujer");

            ora.Open();
            OracleCommand commando = new OracleCommand("MostrarCargo", ora);
            commando.CommandType = System.Data.CommandType.StoredProcedure;
            commando.Parameters.Add("registros", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            using (OracleDataReader reader = commando.ExecuteReader())
            {
                while (reader.Read())
                {
                    // Asumiendo que el nombre del edificio está en la primera columna
                    string nombreEdificio = reader.GetString(0);
                    comboBox3.Items.Add(nombreEdificio);
                }
            }
            OracleCommand command = new OracleCommand("MostrarDepartamentos", ora);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.Add("registros", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            using (OracleDataReader reade = command.ExecuteReader())
            {
                while (reade.Read())
                {
                    // Asumiendo que el nombre del edificio está en la primera columna
                    string nombreDepartamento = reade.GetString(1);
                    comboBox2.Items.Add(nombreDepartamento);
                }
            }
            ora.Close();


        }
    }
}
