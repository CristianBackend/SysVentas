using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;
using CapaEntidades;

namespace CapaNegocio
{
   public class N_Ingredientes
    {
        D_Ingredientes D_Ingredientes = new D_Ingredientes();
        public List<E_Ingredientes> ListandoIngredientes(string buscar)
        {
            return D_Ingredientes.ListarIngredientes(buscar);
        }

     


    }
}
