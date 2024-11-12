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
    
    public class N_Compras
    {
        DetalleCompra detalleCompra = new DetalleCompra();
        private D_Compras datosCompras = new D_Compras();

        // Método para insertar una compra, recibe un objeto de tipo E_Compras
        public void InsertarCompra(E_Compras compra)
        {
            // Insertar la compra principal y obtener el ID
            int compraId = datosCompras.InsertarCompra(compra);

            // Insertar los detalles de la compra
            foreach (var detalle in compra.Detalles)
            {
                detalle.IdCompra = compraId; // Asigna el ID de la compra principal a cada detalle
                datosCompras.InsertarDetalleCompra(detalle); // Inserta cada detalle
            }
        }

        // Método para obtener la lista de compras
        public List<E_Compras> ObtenerCompras()
        {
            // Obtener compras desde la capa de datos
            return datosCompras.ObtenerCompras();
        }
        public DataTable MostrarCompras()
        {
            return datosCompras.ListarCompras();
        }
        public DataTable BuscarCompra(string buscar)
        {
            detalleCompra.Buscar = buscar;
            return datosCompras.BuscarCompra(detalleCompra);
        }

        public DataTable BuscarCuentasporpagar(string buscar)
        {
            detalleCompra.Buscar = buscar;
            return datosCompras.BuscarCompra(detalleCompra);
        }

        public DataTable MostrarCuentasPorPagar()
        {
            return datosCompras.ListaCuentasPagar();
        }

        public int ContarCuentasPagar()
        {
            return datosCompras.ObtenerCantidadCuentas();
        }

        public void CambiarEstadoAContado(int idCompra)
        {
           datosCompras.CambiarEstadoAContado(idCompra);
        }
    }
}

