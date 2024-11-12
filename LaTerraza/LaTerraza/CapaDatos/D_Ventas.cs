using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using CapaEntidades;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography.X509Certificates;

namespace CapaDatos
{
    public class D_Ventas
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















        public void InsertarVenta(E_Ventas ventas)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SP_InsertarVenta", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();
                cmd.Parameters.AddWithValue("@Id_Producto", ventas.Id_Producto1);
                cmd.Parameters.AddWithValue("@Cantidad", ventas.Cantidad1);
                cmd.Parameters.AddWithValue("@Cliente", ventas.Cliente1);
                cmd.Parameters.AddWithValue("@Precio_Unitario", ventas.Precio_Unitario1);
                cmd.Parameters.AddWithValue("@Total", ventas.Total1);
                cmd.Parameters.AddWithValue("@Tipo_Pago", ventas.Tipo_Pago1);
                cmd.Parameters.AddWithValue("@Fecha", ventas.Fecha1);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al insertar la venta: " + ex.Message);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }

        public DataTable MostrarVentas()
        {
            DataTable dt = new DataTable();
            SqlDataReader LeerFilas;
            SqlCommand cmd = new SqlCommand("SP_Mostrar_Venta", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            conn.Open();
            LeerFilas = cmd.ExecuteReader();
            dt.Load(LeerFilas);

            LeerFilas.Close();
            conn.Close();

            return dt;
        }

        public DataTable BuscarVentas(E_Productos productos)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_Buscar_Venta", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            conn.Open();
            cmd.Parameters.AddWithValue("@Buscar", productos.Buscar1);

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
            sqlDataAdapter.Fill(dt);
            conn.Close();
            return dt;
        }
    }
}
