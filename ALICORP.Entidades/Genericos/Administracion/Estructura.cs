using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALICORP.Entidades
{
    public partial class Estructura
    {
        public int Id { get; set; }
        public int? PadreId { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public bool Tablero { get; set; }

        public List<EstructuraInstancia> Instancias { get; set; }
        public List<EstructuraArea> Areas { get; set; }
    }
}
