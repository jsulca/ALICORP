﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALICORP.Entidades
{
    public partial class Instancia
    {
        public int Id { get; set; }
        public int ColorFondoId { get; set; }
        public int ColorTextoId { get; set; }
        public string Abreviatura { get; set; }
        public string Descripcion { get; set; }
    }
}
