using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPWinForm_equipo_15A
{
    public class Articulo
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }

        public int IdMarca { get; set; }
        public string MarcaDescripcion { get; set; }   

        public int IdCategoria { get; set; }
        public string CategoriaDescripcion { get; set; }

    }
}
