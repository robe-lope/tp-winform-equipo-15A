using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_WinForm
{
    internal class Categoria
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        // Igual que en Marca, esto facilitará mostrarla en listas desplegables
        public override string ToString()
        {
            return Descripcion;
        }
    }
}
