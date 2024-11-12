using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;
using CapaEntidades;


namespace CapaNegocio
{
    public  class N_Extras
    {
        D_Extras D_Ingredientes=new D_Extras();
        public List<E_Extras> ListandoExtras(string buscar)
        {
            return D_Ingredientes.ListarExtras(buscar);
        }


        public void InsertarExtra(E_Extras extras)
        {
            D_Ingredientes.InsertarExtra(extras);
        }
    }
}
