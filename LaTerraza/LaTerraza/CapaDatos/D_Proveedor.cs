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
    public class D_Proveedor
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conectar"].ConnectionString);
        
        public List<E_Proveedor> ListarProveedores(string buscar)
        {
            SqlDataReader LeerFilas;
            SqlCommand cmd = new SqlCommand("SP_Mostrar_Proveedor", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            conn.Open();

            cmd.Parameters.AddWithValue("@Buscar", buscar);

            LeerFilas = cmd.ExecuteReader();

            List<E_Proveedor> Listar = new List<E_Proveedor>();
            while (LeerFilas.Read())
            {
                Listar.Add(new E_Proveedor
                {

                    Id_Proveedor1 = LeerFilas.GetInt32(0),
                    Nombre1=LeerFilas.GetString(1),
                    Codigo1= LeerFilas.GetString(2),
                    Correo1= LeerFilas.GetString(3),
                    Telefono1=LeerFilas.GetString(4),
                    Direccion1 = LeerFilas.GetString(5)



                }) ;


            }
            conn.Close();
            LeerFilas.Close();

            return Listar;

        }

        public void InsertarProveedor(E_Proveedor E_Proveedor)
        {

            SqlCommand cmd = new SqlCommand("SP_Agregarproveedor", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            conn.Open();

            cmd.Parameters.AddWithValue("@Nombre", E_Proveedor.Nombre1);
            cmd.Parameters.AddWithValue("@Correo", E_Proveedor.Correo1);
            cmd.Parameters.AddWithValue("@Telefono", E_Proveedor.Telefono1);
            cmd.Parameters.AddWithValue("@Direccion", E_Proveedor.Direccion1);

            cmd.ExecuteNonQuery();
            conn.Close();

        }

        public void EliminarProveedor(int Id)
        {
            SqlCommand cmd = new SqlCommand("SP_EliminarProveedor", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            conn.Open();
            cmd.Parameters.AddWithValue("@Id_Proveedor", Id);
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public void EditarProveedor(E_Proveedor proveedor)
        {
            SqlCommand cmd = new SqlCommand("SP_Editar_Proveedor", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            conn.Open();
            cmd.Parameters.AddWithValue("@Id_Proveedor", proveedor.Id_Proveedor1);
            cmd.Parameters.AddWithValue("@Nombre", proveedor.Nombre1);
            cmd.Parameters.AddWithValue("@Correo", proveedor.Correo1);
            cmd.Parameters.AddWithValue("@Telefono", proveedor.Telefono1);
            cmd.Parameters.AddWithValue("@Direccion", proveedor.Direccion1);
            cmd.ExecuteNonQuery();
            conn.Close();

        }


        public int ObtenerCantidadProveedores()
        {
            int cantidad = 0;

                using (SqlCommand comando = new SqlCommand("ContarProveedores", conn))
                {
                    comando.CommandType = CommandType.StoredProcedure;

                    conn.Open();
                    cantidad = (int)comando.ExecuteScalar();
                }

            conn.Close() ;  
            return cantidad;
        }

    }
}
