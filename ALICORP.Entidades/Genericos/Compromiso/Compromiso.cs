using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALICORP.Entidades
{
    public partial class Compromiso
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public string Detalle { get; set; }
        public string Impacto { get; set; }
        public EstadoCompromiso Estado { get; set; }
        public DateTime FechaRegistro { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public DateTime? FechaProgramacion { get; set; }
        public DateTime? FechaReprogramacion { get; set; }
        public int EstructuraId { get; set; }
        public int TableroId { get; set; }
        public int? AreaId { get; set; }
        public int? EmpleadoId { get; set; }
        public string Respuesta { get; set; }
        public int? InstanciaId { get; set; }

        public Estructura Estructura { get; set; }
        public Estructura Tablero { get; set; }
        public Area Area { get; set; }
        public Instancia Instancia { get; set; }
        public Empleado Empleado { get; set; }
    }
}
