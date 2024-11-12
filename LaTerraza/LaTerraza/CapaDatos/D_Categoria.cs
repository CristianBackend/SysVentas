using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;
using CapaEntidades;
using System.Data;


namespace CapaDatos
{
    public class D_Categoria
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conectar"].ConnectionString);
  
        public List <E_Categoria> ListarCategorias(string buscar)
        {
            SqlDataReader LeerFilas;
            SqlCommand cmd = new SqlCommand("SP_Buscar_Categoria",conn);
            cmd.CommandType = CommandType.StoredProcedure;
            conn.Open();

            cmd.Parameters.AddWithValue("@Buscar", buscar);

            LeerFilas = cmd.ExecuteReader();

            List<E_Categoria> Listar= new List<E_Categoria>();
            while( LeerFilas.Read())
            {
                Listar.Add(new E_Categoria
                {
                    Id_Categoria1 = LeerFilas.GetInt32(0),
                    Codigo_Categoria1 = LeerFilas.GetString(1),
                    Nombre_Categoria1= LeerFilas.GetString(2),
                    Descripcion_Categoria1 = LeerFilas.GetString(3)
                }); 
            }
            conn.Close();
            LeerFilas.Close();

            return Listar;

        }


        public void InsertarCategoria(E_Categoria Categoria)
        {
            SqlCommand cmd = new SqlCommand("SP_Insertar_Categoria", conn);
            cmd.CommandType = CommandType.StoredProcedure;  
            conn.Open();

            cmd.Parameters.AddWithValue("@Nombre", Categoria.Nombre_Categoria1);
            cmd.Parameters.AddWithValue("@Descripcion", Categoria.Descripcion_Categoria1);

            cmd.ExecuteNonQuery();
            conn.Close();

        }

        public void EditarCategoria(E_Categoria Categoria)
        {
            SqlCommand cmd = new SqlCommand("SP_Editar_Categoria", conn);
            cmd.CommandType= CommandType.StoredProcedure;
            conn.Open();
            cmd.Parameters.AddWithValue("@Id_Categoria", Categoria.Id_Categoria1);
            cmd.Parameters.AddWithValue("@Nombre",Categoria.Nombre_Categoria1);
            cmd.Parameters.AddWithValue("@Descripcion",Categoria.Descripcion_Categoria1);
            cmd.ExecuteNonQuery();
            conn.Close();

        }

        public void EliminarCategoria (E_Categoria Categoria)
        {
            SqlCommand cmd = new SqlCommand("SP_Eliminar_Categoria", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            conn.Open();
            cmd.Parameters.AddWithValue("@Id_Categoria", Categoria.Id_Categoria1);
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        
    }
}
