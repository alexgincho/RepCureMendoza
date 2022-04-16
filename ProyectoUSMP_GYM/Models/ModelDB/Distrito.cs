using System;
using System.Collections.Generic;

#nullable disable

namespace ProyectoUSMP_GYM.Models.ModelDB
{
    public partial class Distrito
    {
        public int PkDistrito { get; set; }
        public string Descripcion { get; set; }
        public int? FkProvincia { get; set; }

        public virtual Provincium FkProvinciaNavigation { get; set; }
    }
}
