using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALICORP.Entidades
{
    public partial class Categoria
    {
        public int Id { get; set; }
        public int VerificacionId { get; set; }
        public string  Descripcion { get; set; }
        public int Orden { get; set; }
        public bool Eliminado { get; set; }

        public Verificacion Verificacion { get; set; }

        public List<Pregunta> Preguntas { get; set; }
    }
}
