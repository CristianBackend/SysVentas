using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;
using CapaEntidades;

namespace CapaNegocio
{
    public class N_Ventas
    {
        D_Ventas D_Ventas = new D_Ventas(); 
        E_Productos E_Productos = new E_Productos();

        public DataTable MostrarProductos()
        {
            return D_Ventas.ListaProductos();
        }








        public void InsertarVenta(E_Ventas ventas)
        {
            D_Ventas.InsertarVenta(ventas);
        }

        public DataTable MostrarVentas()
        {
            return D_Ventas.MostrarVentas();
        }

        public DataTable BuscarVentas(string buscar)
        {
            E_Productos.Buscar1 = buscar;
            return D_Ventas.BuscarVentas(E_Productos);
        }
    }
}
