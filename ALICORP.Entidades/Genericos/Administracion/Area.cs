using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALICORP.Entidades
{
    public partial class Area
    {
        public int Id { get; set; }
        public int ColorFondoId { get; set; }
        public int ColorTextoId { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }

        public Color ColorFondo { get; set; }
        public Color ColorTexto { get; set; }
    }
}
