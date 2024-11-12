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
    public class N_Usuarios
    {
        D_Usuarios d_Usuarios = new D_Usuarios();

        public bool VerificarUsuarios(string Usuario, string Clave)
        {
            return d_Usuarios.VerificarUsuarios(Usuario, Clave);
        }

        public List<E_Usuarios> ListandoUsuarios(string buscar)
        {
            return d_Usuarios.ListarUsuarios(buscar);
        }

        public void InsertarUsuario(E_Usuarios e_Usuarios)
        {
            d_Usuarios.InsertarUsuario(e_Usuarios);

        }
        public void EliminarUsuario(int Id)
        {
            d_Usuarios.EliminarUsuario(Id);
        }
        public void EditarUsuario(E_Usuarios usuarios)
        {
            d_Usuarios.EditarUsuarios(usuarios);
        }

    }
}
