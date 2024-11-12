using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;
namespace CapaEntidades
{
    public class E_Compras
    {
        public int IdCompra { get; set; }
        public int IdProveedor { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Total { get; set; }
        public string Estado { get; set; } 
        public List<DetalleCompra> Detalles { get; set; }
        public string Buscar { get; set; }
    }
    public class DetalleCompra
    {
        public int IdDetalleCompra { get; set; }
        public int IdCompra { get; set; }
        public string DescripcionProducto { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Subtotal { get; set; }
        public string TasaItbis { get; set; } 
        public string Buscar {  get; set; }
    }
}
