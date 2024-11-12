using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;

namespace CapaEntidades
{
    public class E_Productos
    {
        private int Id_Producto;
        private string Codigo_Producto;
        private string Nombre_Producto;
        private string Descripcion_Producto;
        private decimal Precio_Costo_Producto;
        private decimal Precio_Venta_Producto;
        private string Marca_Producto;
        private int Stock;
        private int P_Id_Categoria;
        private int P_Id_Proveedor;
        private string Buscar;
        private byte[] imagen;

        public int Id_Producto1 { get => Id_Producto; set => Id_Producto = value; }
        public string Codigo_Producto1 { get => Codigo_Producto; set => Codigo_Producto = value; }
        public string Nombre_Producto1 { get => Nombre_Producto; set => Nombre_Producto = value; }
        public string Descripcion_Producto1 { get => Descripcion_Producto; set => Descripcion_Producto = value; }
        public decimal Precio_Costo_Producto1 { get => Precio_Costo_Producto; set => Precio_Costo_Producto = value; }
        public decimal Precio_Venta_Producto1 { get => Precio_Venta_Producto; set => Precio_Venta_Producto = value; }
        public string Marca_Producto1 { get => Marca_Producto; set => Marca_Producto = value; }
        public int Stock1 { get => Stock; set => Stock = value; }
        public int P_Id_Categoria1 { get => P_Id_Categoria; set => P_Id_Categoria = value; }
        public int P_Id_Proveedor1 { get => P_Id_Proveedor; set => P_Id_Proveedor = value; }
        public string Buscar1 { get => Buscar; set => Buscar = value; }
        public byte[] Imagen { get => imagen; set => imagen = value; }
    }
}
