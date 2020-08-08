﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALICORP.Entidades
{
    public partial class Empleado
    {
        public int Id { get; set; }
        public int? CargoId { get; set; }
        public int? AreaId { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string NroDocumento { get; set; }
        public string Correo { get; set; }
        public string Telefono { get; set; }

        public Cargo Cargo { get; set; }
        public Area Area { get; set; }
    }
}
