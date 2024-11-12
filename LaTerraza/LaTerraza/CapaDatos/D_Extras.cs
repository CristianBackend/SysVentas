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
    public class D_Extras
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conectar"].ConnectionString);

        public List<E_Extras> ListarExtras(string buscar)
        {
            SqlDataReader LeerFilas;
            SqlCommand cmd = new SqlCommand("Sp_Buscar_Extra", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            conn.Open();

            cmd.Parameters.AddWithValue("@Buscar", buscar);

            LeerFilas = cmd.ExecuteReader();

            List<E_Extras> Listar = new List<E_Extras>();
            while (LeerFilas.Read())
            {
                Listar.Add(new E_Extras
                {
                    Id_Extra1 = LeerFilas.GetInt32(0),
                    Extra1 = LeerFilas.GetString(1),
                    Descripcion1 = LeerFilas.GetString(2),  
                    PrecioDelExtra1 = LeerFilas.GetDecimal(3)

                });
            }
            conn.Close();
            LeerFilas.Close();

            return Listar;

        }


        public void InsertarExtra(E_Extras extras)
        {
            SqlCommand cmd = new SqlCommand("Sp_Insertar_Extras", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            conn.Open();
            cmd.Parameters.AddWithValue("@Extra", extras.Extra1);
            cmd.Parameters.AddWithValue("@Descripcion", extras.Descripcion1);
            cmd.Parameters.AddWithValue("@Precio", extras.PrecioDelExtra1);
            cmd.ExecuteNonQuery();
            conn.Close();

        }

    }
}
