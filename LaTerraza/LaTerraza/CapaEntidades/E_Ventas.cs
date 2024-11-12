using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;

namespace CapaEntidades
{
  public class E_Ventas
    {
        private int Id_DetalleVenta;
        private string Codiog;
        private int Id_Producto;
        private int Cantidad;
        private string Cliente;
        private decimal Precio_Unitario;
        private decimal Total;
        private string Tipo_Pago;
        private string Fecha;

        public int Id_DetalleVenta1 { get => Id_DetalleVenta; set => Id_DetalleVenta = value; }
        public string Codiog1 { get => Codiog; set => Codiog = value; }
        public int Id_Producto1 { get => Id_Producto; set => Id_Producto = value; }
        public int Cantidad1 { get => Cantidad; set => Cantidad = value; }
        public string Cliente1 { get => Cliente; set => Cliente = value; }
        public decimal Precio_Unitario1 { get => Precio_Unitario; set => Precio_Unitario = value; }
        public decimal Total1 { get => Total; set => Total = value; }
        public string Tipo_Pago1 { get => Tipo_Pago; set => Tipo_Pago = value; }
        public string Fecha1 { get => Fecha; set => Fecha = value; }
    }
}
