using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;
using CapaEntidades;


namespace CapaNegocio
{
   public class N_Productos
    {
        E_Productos E_Productos=new E_Productos();
        D_Productos productos = new D_Productos();
       
        public void ActualizarStock(E_Productos productoss, int cantidad)
        {
            productos.ActualizarStock(productoss, cantidad);
        }
        public DataTable MostrarProductos()
        {
            return productos.ListaProductos();
        }

        public DataTable BuscarProducto(string buscar)
        {
            E_Productos.Buscar1 = buscar;
            return productos.BuscarProducto(E_Productos);
        }

        public void EliminarProducto(int Id)
        {
            productos.EliminarProducto(Id);
        }

        public void EditarProducto(E_Productos proc)
        {
            productos.ActualizarProducto(proc);
        }

        public E_Productos ObtenerProductoPorId(int idProducto)
        {
            D_Productos dataProducto = new D_Productos();
            return dataProducto.ObtenerProductoPorId(idProducto);
        }

        public void AgregarProducto(E_Productos producto)
        {
            productos.AgregarProducto(producto);
        }

    }
}
