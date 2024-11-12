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
    public partial class Departamentos : Form
    {
        public string connectionString = "Data Source=CRISTIANAS\\SQLEXPRESS;Initial Catalog=Master;Integrated Security=True;";
        public SqlConnection connection;
      
        public Departamentos()
        {
            InitializeComponent();
            connection = new SqlConnection(connectionString);

           
        }

        public void Modificar()
        {
           

        }



        private void iconButton5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void iconButton2_Click(object sender, EventArgs e)
        {
            AgregarDepartamento agregar = new AgregarDepartamento();
            agregar.Show();
        }

        private void iconButton4_Click(object sender, EventArgs e)
        {
                EditarDepartamento editar = new EditarDepartamento(); editar.Show();
        }

        private void Departamentos_Load(object sender, EventArgs e)
        {
            dataGridView1.ClearSelection();
            MostrarDatos();
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].Width = 150;
            dataGridView1.Columns[1].HeaderText = "Dep";
            dataGridView1.MultiSelect = false;
            dataGridView1.ReadOnly = true;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;

        }

        public void MostrarDatos()
        {
            try
            {
                connection.Open();

                SqlCommand command = new SqlCommand("MostrarDepartamentos", connection);
                command.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable tabla = new DataTable();
                adapter.Fill(tabla);
                dataGridView1.DataSource = tabla;
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

        private void btnEliminarDep_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count < 0)
            {
                MessageBox.Show("Por favor, seleccione un departamento para eliminar.", "Selección requerida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int idDepartamento = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["id_Departamento"].Value);

            try
            {
               
                    connection.Open();
                    SqlCommand command = new SqlCommand("EliminarDepartamento", connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@id", idDepartamento);
                    command.ExecuteNonQuery();
                
                MessageBox.Show("Departamento eliminado correctamente.", "Eliminación exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar el departamento: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    
        

        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Por favor, seleccione un departamento para eliminar.", "Selección requerida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }
    }
}
