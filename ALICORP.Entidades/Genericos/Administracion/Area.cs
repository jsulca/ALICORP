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
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public string ColorFondo { get; set; }
        public string ColorTexto { get; set; }
    }
}
