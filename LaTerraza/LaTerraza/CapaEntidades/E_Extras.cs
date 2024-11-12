using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;

namespace CapaEntidades
{
    public  class E_Extras
    {
        private int Id_Extra;
        private string Extra;
        private string Descripcion;
        private decimal PrecioDelExtra;
       

        public int Id_Extra1 { get => Id_Extra; set => Id_Extra = value; }
        public string Extra1 { get => Extra; set => Extra = value; }
        public decimal PrecioDelExtra1 { get => PrecioDelExtra; set => PrecioDelExtra = value; }
        public string Descripcion1 { get => Descripcion; set => Descripcion = value; }
    }
}
