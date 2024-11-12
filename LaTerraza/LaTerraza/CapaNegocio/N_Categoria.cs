using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;
using CapaEntidades;

namespace CapaNegocio
{
    public class N_Categoria
    {
        D_Categoria categoria = new D_Categoria();
        public List<E_Categoria> ListandoCategoria(string buscar)
        {
            return categoria.ListarCategorias(buscar);
        }

        public void InsertarCategoria(E_Categoria Categoria)
        {
            categoria.InsertarCategoria(Categoria);

        }
        public void EditarCategoria (E_Categoria Categoria)
        {
            categoria.EditarCategoria(Categoria);
        }

        public void EliminarCategoria(E_Categoria Categoria)
        {
            categoria.EliminarCategoria(Categoria);
        }



    }
}
