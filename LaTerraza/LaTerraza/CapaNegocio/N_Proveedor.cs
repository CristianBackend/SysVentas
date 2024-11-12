using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;
using CapaEntidades;

namespace CapaNegocio
{
    public class N_Proveedor
    {
        D_Proveedor d_Proveedor=new D_Proveedor();
      
        public List<E_Proveedor> ListandoProveedores(string buscar)
        {
            return d_Proveedor.ListarProveedores(buscar);
        }
        
        public void InsertarProveedor(E_Proveedor E_Proveedor)
        {
            d_Proveedor.InsertarProveedor(E_Proveedor);

        }

        public void EliminarProveedor(int Id)
        {
            d_Proveedor.EliminarProveedor(Id);

        }


        public void EditarProveedor(E_Proveedor proveedor)
        {
            d_Proveedor.EditarProveedor(proveedor);
        }

        public int ContarProveedores()
        {
            return d_Proveedor.ObtenerCantidadProveedores();
        }
    }
}
