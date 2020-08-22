using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALICORP.Entidades
{
    public partial class CompromisoEstado
    {
        public int CompromisoId { get; set; }
        public EstadoCompromiso Estado { get; set; }
        public DateTime FechaRegistro { get; set; }

        public Compromiso Compromiso { get; set; }
    }
}
