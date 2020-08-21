using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALICORP.Entidades
{
    public partial class Respuesta
    {
        public int PreguntaId { get; set; }
        public int Valor { get; set; }
        public string Descripcion { get; set; }

        public Pregunta Pregunta { get; set; }
    }
}
