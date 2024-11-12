using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;


namespace CapaEntidades
{
    public class E_Proveedor
    {
        private int Id_Proveedor;
        private string Codigo;
        private string Nombre;
        private string Correo;
        private string Telefono;
        private string Direccion;

        public int Id_Proveedor1 { get => Id_Proveedor; set => Id_Proveedor = value; }
        public string Codigo1 { get => Codigo; set => Codigo = value; }
        public string Nombre1 { get => Nombre; set => Nombre = value; }
        public string Correo1 { get => Correo; set => Correo = value; }
        public string Telefono1 { get => Telefono; set => Telefono = value; }
        public string Direccion1 { get => Direccion; set => Direccion = value; }
    }
}
