using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;

namespace CapaEntidades
{
    public class E_Categoria
    {
       private int Id_Categoria;
       private string Codigo_Categoria;
       private string Nombre_Categoria; 
       private string Descripcion_Categoria;
        
        public int Id_Categoria1 { get => Id_Categoria; set => Id_Categoria = value; }
        public string Codigo_Categoria1 { get => Codigo_Categoria; set => Codigo_Categoria = value; }
        public string Nombre_Categoria1 { get => Nombre_Categoria; set => Nombre_Categoria = value; }
        public string Descripcion_Categoria1 { get => Descripcion_Categoria; set => Descripcion_Categoria = value; }


    }
}
