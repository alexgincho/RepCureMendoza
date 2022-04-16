using System;
using System.Collections.Generic;

#nullable disable

namespace ProyectoUSMP_GYM.Models.ModelDB
{
    public partial class Usuariomembresium
    {
        public int PkMembresia { get; set; }
        public int? FkUsuario { get; set; }
        public int? FkPersonalcrea { get; set; }
        public DateTime? Fechacrea { get; set; }
        public DateTime? Fechafinal { get; set; }
        public double? Costo { get; set; }
        public int? Estado { get; set; }
        public int? FkPersonaledita { get; set; }
        public DateTime? Fechaedita { get; set; }
        public bool? Isdelete { get; set; }

        public virtual Personaladm FkPersonalcreaNavigation { get; set; }
        public virtual Usuario FkUsuarioNavigation { get; set; }
    }
}
