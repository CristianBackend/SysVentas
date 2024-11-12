using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using CapaEntidades;
using System.Data;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class D_Productos
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conectar"].ConnectionString);


        public DataTable ListaProductos()
        {
            DataTable dt = new DataTable();
            SqlDataReader LeerFilas;
            SqlCommand cmd = new SqlCommand("SP_Mostrar_Productos", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            conn.Open();
            LeerFilas = cmd.ExecuteReader();
            dt.Load(LeerFilas);

            LeerFilas.Close();
            conn.Close();

            return dt;

        }

        public DataTable BuscarProducto(E_Productos productos)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_Buscar_Producto", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            conn.Open();
            cmd.Parameters.AddWithValue("@Buscar", productos.Buscar1);

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
            sqlDataAdapter.Fill(dt);
            conn.Close();
            return dt;
        }

        public void EliminarProducto(int Id)
        {
            SqlCommand cmd = new SqlCommand("SP_Eliminar_Producto", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            conn.Open();
            cmd.Parameters.AddWithValue("@Id_Producto", Id);
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public void ActualizarProducto(E_Productos producto)
        {
            SqlCommand cmd = new SqlCommand("SP_Actualizar_Producto", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            conn.Open();

            cmd.Parameters.AddWithValue("@Id_Producto", producto.Id_Producto1);
            cmd.Parameters.AddWithValue("@Nombre", producto.Nombre_Producto1);
            cmd.Parameters.AddWithValue("@Descripcion", producto.Descripcion_Producto1);
            cmd.Parameters.AddWithValue("@Precio_Venta", producto.Precio_Venta_Producto1);
            cmd.Parameters.AddWithValue("@Id_Categoria", producto.P_Id_Categoria1);
            cmd.Parameters.AddWithValue("@Imagen", producto.Imagen);

            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public E_Productos ObtenerProductoPorId(int idProducto)
        {
            E_Productos producto = null;

            using (SqlConnection conn = new SqlConnection("Server=DESKTOP-QP4FDV3\\SQLDEVELOPER;Integrated Security=yes; Database=Sistema_LaTerraza"))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Productos WHERE Id_Producto = @IdProducto", conn);
                cmd.Parameters.AddWithValue("@IdProducto", idProducto);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        producto = new E_Productos
                        {
                            Id_Producto1 = (int)reader["Id_Producto"],
                            Nombre_Producto1 = reader["Nombre"].ToString(),
                            Descripcion_Producto1 = reader["Descripcion"].ToString(),
                            Precio_Venta_Producto1 = (decimal)reader["Precio_Venta"],
                            P_Id_Categoria1 = (int)reader["Id_Categoria"],
                            Imagen = reader["Imagen"] as byte[]
                        };
                    }
                }
            }

            return producto;
        }
        public void AgregarProducto(E_Productos productos)
        {
            SqlCommand cmd = new SqlCommand("SP_Crear_Producto", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            conn.Open();

            cmd.Parameters.AddWithValue("@Nombre", productos.Nombre_Producto1);
            cmd.Parameters.AddWithValue("@Descripcion", productos.Descripcion_Producto1);
            cmd.Parameters.AddWithValue("@Precio_Venta", productos.Precio_Venta_Producto1);
            cmd.Parameters.AddWithValue("@Id_Categoria", productos.P_Id_Categoria1);
            cmd.Parameters.AddWithValue("@Imagen", productos.Imagen);


            cmd.ExecuteNonQuery();
            conn.Close();
        }
      


        public void ActualizarStock(E_Productos productos, int cantidad)
        {
            SqlCommand cmd = new SqlCommand("SP_AumentarStockProducto", conn);
            cmd.CommandType= CommandType.StoredProcedure;
            conn.Open();

            cmd.Parameters.AddWithValue("@Nombre", productos.Nombre_Producto1);
            cmd.Parameters.AddWithValue("@Cantidad", cantidad);
            cmd.ExecuteNonQuery();
            conn.Close();


        }
    }
}
