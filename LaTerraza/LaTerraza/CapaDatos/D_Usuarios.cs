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
    public class D_Usuarios
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conectar"].ConnectionString);
        public bool VerificarUsuarios(string Usuario, string Clave)
        {
            SqlCommand cmd = new SqlCommand("SP_VerificarUsuario", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            // Agregar los parámetros
            cmd.Parameters.AddWithValue("@Usuario", Usuario);
            cmd.Parameters.AddWithValue("@Clave", Clave);

            conn.Open();

            // Obtener el resultado del procedimiento almacenado
            int resultado = (int)cmd.ExecuteScalar();

            conn.Close();

            // Devolver true si el usuario y clave son válidos
            return resultado == 1;
        }


        public List<E_Usuarios> ListarUsuarios(string buscar)
        {
            SqlDataReader LeerFilas;
            SqlCommand cmd = new SqlCommand("Sp_Mostrar_Usuarios", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            conn.Open();

            cmd.Parameters.AddWithValue("@Buscar", buscar);

            LeerFilas = cmd.ExecuteReader();

            List<E_Usuarios> Listar = new List<E_Usuarios>();
            while (LeerFilas.Read())
            {
                Listar.Add(new E_Usuarios
                {
                    Id_Usuario1 = LeerFilas.GetInt32(0),
                    Usuario1 = LeerFilas.GetString(1),
                    Rol1 = LeerFilas.GetString(2),
                    Clave1 = LeerFilas.GetString(3)
                });
            }
            conn.Close();
            LeerFilas.Close();

            return Listar;

        }


        public void InsertarUsuario(E_Usuarios E_Usuarios)
        {

            SqlCommand cmd = new SqlCommand("Sp_Agregar_Usuarios", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            conn.Open();

            cmd.Parameters.AddWithValue("@Nombre", E_Usuarios.Usuario1);
            cmd.Parameters.AddWithValue("@Rol", E_Usuarios.Rol1);
            cmd.Parameters.AddWithValue("@Clave", E_Usuarios.Clave1);

            cmd.ExecuteNonQuery();
            conn.Close();

        }

        public void EliminarUsuario(int Id)
        {
            SqlCommand cmd = new SqlCommand("Sp_Eliminar_Usuarios", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            conn.Open();
            cmd.Parameters.AddWithValue("@Id", Id);
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public void EditarUsuarios(E_Usuarios Usuarios)
        {
            SqlCommand cmd = new SqlCommand("Sp_Editar_Usuarios", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            conn.Open();
            cmd.Parameters.AddWithValue("@Id_Usuario", Usuarios.Id_Usuario1);
            cmd.Parameters.AddWithValue("@Nombre", Usuarios.Usuario1);
            cmd.Parameters.AddWithValue("@Rol", Usuarios.Rol1);
            cmd.Parameters.AddWithValue("@Clave", Usuarios.Clave1);
            cmd.ExecuteNonQuery();
            conn.Close();


        }
    }
}
