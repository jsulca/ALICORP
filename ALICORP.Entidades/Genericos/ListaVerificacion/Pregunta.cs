using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALICORP.Entidades
{
    public partial class Pregunta
    {
        public int Id { get; set; }
        public int CategoriaId { get; set; }
        public int Orden { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public bool Eliminado { get; set; }

        public Categoria Categoria { get; set; }
        public List<Respuesta> Respuestas { get; set; }
    }
}
