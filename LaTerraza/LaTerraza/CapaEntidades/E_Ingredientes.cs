using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;


namespace CapaEntidades
{
    public class E_Ingredientes
    {
        private int Id_Ingrediente;
        private string Descripcion;
        private int Cantidad;
    


        public string Descripcion1 { get => Descripcion; set => Descripcion = value; }
        public int Cantidad1 { get => Cantidad; set => Cantidad = value; }
        public int Id_Ingrediente1 { get => Id_Ingrediente; set => Id_Ingrediente = value; }
    }
}
