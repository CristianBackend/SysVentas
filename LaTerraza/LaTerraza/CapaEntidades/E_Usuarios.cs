using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;


namespace CapaEntidades
{
    public class E_Usuarios
    {
        private int Id_Usuario;
        private string Usuario;
        private string Rol;
        private string Clave;

        public int Id_Usuario1 { get => Id_Usuario; set => Id_Usuario = value; }
        public string Usuario1 { get => Usuario; set => Usuario = value; }
        public string Rol1 { get => Rol; set => Rol = value; }
        public string Clave1 { get => Clave; set => Clave = value; }
    }
}
