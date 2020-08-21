using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALICORP.Entidades.Filtros
{
    public class CompromisoFiltro
    {
        public int? TableroId { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public DateTime? FechaRegistroDesde { get; set; }
        public DateTime? FechaRegistroHasta { get; set; }
        public int? Estado { get; set; }
    }
}
