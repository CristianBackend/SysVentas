using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Nomina
{
    public partial class AgregarEdificio : Form
    {

        public string connectionString = "Data Source=CRISTIANAS\\SQLEXPRESS;Initial Catalog=Master;Integrated Security=True;";

        public SqlConnection connection;
        public AgregarEdificio()
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

        private void txtUsuario_Leave(object sender, EventArgs e)
        {
            if (txtUsuario.Text == "")
            {
                txtUsuario.Text = "Nombre";
                txtUsuario.ForeColor = Color.DimGray;
            }
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            txtUsuario.Clear();
        }
        private void cargar()
        {
            

        }

        private void iconButton3_Click(object sender, EventArgs e)
        {

            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand("CrearEdificio", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@NombreEdificio", txtUsuario.Text);
                command.ExecuteNonQuery();

                MessageBox.Show("¡Departamento Insertado Exitosamente! 😊");

                
               
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error al insertar el departamento: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        private void AgregarEdificio_Load(object sender, EventArgs e)
        {

        }
    }
}
