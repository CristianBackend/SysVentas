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
using System.IO;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Security.Cryptography.X509Certificates;

namespace Nomina
{
    public partial class AgregarDepartamento : Form
    {

        public string connectionString = "Data Source=CRISTIANAS\\SQLEXPRESS;Initial Catalog=master;Integrated Security=True;";

        public SqlConnection connection;
      

        public AgregarDepartamento()
        {
            InitializeComponent();

            connection = new SqlConnection(connectionString);
        }


        private void txtUsuario_Enter(object sender, EventArgs e)
        {
            if (txtUsuario.Text == "Nombre")
            {
                txtUsuario.Text = "";
                txtUsuario.ForeColor = Color.DimGray;
            }
        }

        private void txtUsuario_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtUsuario_Leave(object sender, EventArgs e)
        {
            if (txtUsuario.Text == "")
            {
                txtUsuario.Text = "Nombre";
                txtUsuario.ForeColor = Color.DimGray;
            }
        }

        private void textBox7_Enter(object sender, EventArgs e)
        {

        }

        private void textBox7_Leave(object sender, EventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void AgregarDepartamento_Load(object sender, EventArgs e)
        {

            //SqlCommand commando = new SqlCommand("InsertarDepartamento", connection);
            //commando.CommandType = System.Data.CommandType.StoredProcedure;
            //commando.Parameters.Add("registros", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            //using (OracleDataReader reader = commando.ExecuteReader())
            //{
            //    while (reader.Read())
            //    {
            //        // Asumiendo que el nombre del edificio está en la primera columna
            //        string nombreEdificio = reader.GetString(0);
            //        comboBox1.Items.Add(nombreEdificio);
            //    }
            //}
            //connection.Close();
            cargardep();
            LeerEdificios(comboBox1);
            



        }
        private void cargardep()

        {
            try
            {
                connection.Open();
                Departamentos departamentos = new Departamentos();
                AddOwnedForm(departamentos);

                SqlCommand command = new SqlCommand("MostrarDepartamentos", connection);
                command.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable tabla = new DataTable();
                adapter.Fill(tabla);
                departamentos.dataGridView1.DataSource = tabla;
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error al mostrar los datos de los departamentos: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }



        }

        private void iconButton3_Click(object sender, EventArgs e)
        {

            try
            {
                using (connection)
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("InsertarDepartamento", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Departamento", txtUsuario.Text);
                    command.Parameters.AddWithValue("@Edificio", comboBox1.Text);
                    command.ExecuteNonQuery();
                }

                MessageBox.Show("¡Departamento Insertado Exitosamente! 😊");



                this.Close();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error al insertar el departamento: " + ex.Message);
            }
        }

        private void iconButton2_Click(object sender, EventArgs e)
        {
            AgregarEdificio agregar = new AgregarEdificio();
            agregar.ShowDialog();
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            AgregarCargos agregar = new AgregarCargos();
            agregar.Show();
        }

        private void iconButton4_Click(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        public void LeerEdificios(ComboBox comboBox)
        {
            try
            {
                // Abrimos la conexión a la base de datos
                connection.Open();

                // Creamos un nuevo DataTable para almacenar los datos
                DataTable tabla = new DataTable();

                // Ejecutamos el procedimiento almacenado para obtener los datos de los edificios
                using (SqlCommand command = new SqlCommand("LeerEdificios", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(tabla);
                }

                // Configuramos el ComboBox para mostrar los datos del DataTable
                comboBox.DataSource = tabla;
                comboBox.DisplayMember = "Edificio";
                // Columna que se mostrará en el ComboBox
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error al leer los datos de los edificios: " + ex.Message);
            }
            finally
            {
                // Cerramos la conexión a la base de datos en el bloque finally para asegurarnos de que se cierre correctamente
                connection.Close();
            }
        }

        




    }
}





    

