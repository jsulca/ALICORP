using ALICORP.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ALICORP.WebApp.Models
{
    public struct CompromisoModel
    {
        public class Nuevo
        {
            public string Descripcion { get; set; }
            public int? TableroId { get; set; }
            public string Detalle { get; set; }
            public string Impacto { get; set; }

            public Compromiso Get()
            {
                return new Compromiso
                {
                    Descripcion = Descripcion,
                    TableroId = TableroId.Value,
                    Detalle = Detalle,
                    Impacto = Impacto
                };
            }
        }

        public class Editar
        {
            public int? Id { get; set; }
            public string Descripcion { get; set; }
            public string Detalle { get; set; }
            public string Impacto { get; set; }

            public Compromiso Get()
            {
                return new Compromiso
                {
                    Id = Id.Value,
                    Descripcion = Descripcion,
                    Detalle = Detalle,
                    Impacto = Impacto
                };
            }
        }

        public class Verificar
        {
            public int? Id { get; set; }
            public string Respuesta { get; set; }
        }
    }
}