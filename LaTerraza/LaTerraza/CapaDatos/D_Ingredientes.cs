using CapaEntidades;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;


namespace CapaDatos
{
    public class D_Ingredientes
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conectar"].ConnectionString);

        public List<E_Ingredientes> ListarIngredientes(string buscar)
        {
            SqlDataReader LeerFilas;
            SqlCommand cmd = new SqlCommand("MostrarIngredientes", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            conn.Open();

            cmd.Parameters.AddWithValue("@Buscar", buscar);

            LeerFilas = cmd.ExecuteReader();

            List<E_Ingredientes> Listar = new List<E_Ingredientes>();
            while (LeerFilas.Read())
            {
                Listar.Add(new E_Ingredientes
                {
                  Id_Ingrediente1=LeerFilas.GetInt32(0),
                   Descripcion1 = LeerFilas.GetString(1),
                   Cantidad1 = LeerFilas.GetInt32(2),
                    
                });
            }
            conn.Close();
            LeerFilas.Close();

            return Listar;
        }

        

    }
}
