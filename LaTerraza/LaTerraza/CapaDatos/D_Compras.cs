using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using CapaEntidades;
using Newtonsoft.Json;

namespace CapaDatos
{
    public class D_Compras
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conectar"].ConnectionString);

        private string connectionString = ConfigurationManager.ConnectionStrings["conectar"].ConnectionString;

        public void InsertarDetalleCompra(DetalleCompra detalle)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Detalle_Compras (Id_Compra, Descripcion_Producto, Cantidad, Precio_Unitario, SubTotal) " +
                               "VALUES (@IdCompra, @DescripcionProducto, @Cantidad, @PrecioUnitario, @Subtotal)";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@IdCompra", detalle.IdCompra);
                command.Parameters.AddWithValue("@DescripcionProducto", detalle.DescripcionProducto ?? string.Empty);
                command.Parameters.AddWithValue("@Cantidad", detalle.Cantidad);
                command.Parameters.AddWithValue("@PrecioUnitario", detalle.PrecioUnitario);
                command.Parameters.AddWithValue("@Subtotal", detalle.Subtotal);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public int InsertarCompra(E_Compras compra)
        {
            int compraId;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Compras (Id_Proveedor, Fecha, Total, Estado) OUTPUT INSERTED.Id_Compra " +
                               "VALUES (@IdProveedor, @Fecha, @Total, @Estado)";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@IdProveedor", compra.IdProveedor);
                command.Parameters.AddWithValue("@Fecha", compra.Fecha);
                command.Parameters.AddWithValue("@Total", compra.Total);
                command.Parameters.AddWithValue("@Estado", compra.Estado); // Añadir parámetro Estado

                connection.Open();
                compraId = (int)command.ExecuteScalar();
            }

            return compraId;
        }

        public List<E_Compras> ObtenerCompras()
        {
            List<E_Compras> compras = new List<E_Compras>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Compras", conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    E_Compras compra = new E_Compras()
                    {
                        IdCompra = (int)reader["Id_Compra"], // Asegúrate de que este nombre sea correcto
                        IdProveedor = (int)reader["Id_Proveedor"],
                        Fecha = (DateTime)reader["Fecha"],
                        Total = (decimal)reader["Total"]
                    };

                    compra.Detalles = ObtenerDetallesCompra(compra.IdCompra);
                    compras.Add(compra);
                }
            }

            return compras;
        }

        private List<DetalleCompra> ObtenerDetallesCompra(int idCompra)
        {
            List<DetalleCompra> detalles = new List<DetalleCompra>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Detalle_Compras WHERE Id_Compra = @IdCompra", conn);
                cmd.Parameters.AddWithValue("@IdCompra", idCompra);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    DetalleCompra detalle = new DetalleCompra()
                    {
                        IdDetalleCompra = (int)reader["Id_Detalle_Compra"],
                        IdCompra = (int)reader["Id_Compra"],
                        DescripcionProducto = reader["Descripcion_Producto"].ToString(),
                        Cantidad = (int)reader["Cantidad"],
                        PrecioUnitario = (decimal)reader["Precio_Unitario"]
                    };

                    detalles.Add(detalle);
                }
            }

            return detalles;
        }

        public DataTable ListarCompras()
        {
            DataTable dt = new DataTable();
            SqlDataReader LeerFilas;
            SqlCommand cmd = new SqlCommand("SP_MostrarCompras", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            conn.Open();
            LeerFilas = cmd.ExecuteReader();
            dt.Load(LeerFilas);

            LeerFilas.Close();
            conn.Close();

            return dt;

        }
        public DataTable BuscarCompra(DetalleCompra compras)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("BuscarCompra", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            conn.Open();
            cmd.Parameters.AddWithValue("@Buscar", compras.Buscar);

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
            sqlDataAdapter.Fill(dt);
            conn.Close();
            return dt;
        }

        public DataTable BuscarCuentasPorPagar(DetalleCompra compras)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("BuscarCuentasPorPagar", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            conn.Open();
            cmd.Parameters.AddWithValue("@Buscar", compras.Buscar);

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
            sqlDataAdapter.Fill(dt);
            conn.Close();
            return dt;
        }

        public DataTable ListaCuentasPagar()
        {
            DataTable dt = new DataTable();
            SqlDataReader LeerFilas;
            SqlCommand cmd = new SqlCommand("BuscarCompraPorPagar", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            conn.Open();
            LeerFilas = cmd.ExecuteReader();
            dt.Load(LeerFilas);

            LeerFilas.Close();
            conn.Close();

            return dt;

        }
        public int ObtenerCantidadCuentas()
        {
            int cantidad = 0;

            using (SqlCommand comando = new SqlCommand("ContarCuentasPorPagar", conn))
            {
                comando.CommandType = CommandType.StoredProcedure;

                conn.Open();
                cantidad = (int)comando.ExecuteScalar();
            }

            conn.Close();
            return cantidad;
        }

        public void CambiarEstadoAContado(int idCompra)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SP_CambiarEstadoAContado", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id_Compra", idCompra);

                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al cambiar estado: " + ex.Message);
                }
            }
        }

    }


    }



