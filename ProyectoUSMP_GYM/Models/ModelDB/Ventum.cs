using System;
using System.Collections.Generic;

#nullable disable

namespace ProyectoUSMP_GYM.Models.ModelDB
{
    public partial class Ventum
    {
        public Ventum()
        {
            Detalleventa = new HashSet<Detalleventum>();
        }

        public int PkVenta { get; set; }
        public string Codigo { get; set; }
        public int? FkUsuario { get; set; }
        public bool? Delivery { get; set; }
        public int? FkDistrito { get; set; }
        public string Observacion { get; set; }
        public int? FkPersonalcrea { get; set; }
        public DateTime? Fechacrea { get; set; }
        public DateTime? Fechaentrega { get; set; }
        public int? Estado { get; set; }
        public double? Totalventa { get; set; }
        public int? FkPersonaledita { get; set; }
        public DateTime? Fechaedita { get; set; }
        public bool? Isdelete { get; set; }

        public virtual ICollection<Detalleventum> Detalleventa { get; set; }
    }
}
