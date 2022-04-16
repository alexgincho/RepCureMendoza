using System;
using System.Collections.Generic;

#nullable disable

namespace ProyectoUSMP_GYM.Models.ModelDB
{
    public partial class Proveedor
    {
        public Proveedor()
        {
            Productos = new HashSet<Producto>();
        }

        public int PkProveedor { get; set; }
        public string Ruc { get; set; }
        public string Razonsocial { get; set; }
        public string Direccion { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public int? FkDistrito { get; set; }
        public int? FkPersonalcrea { get; set; }
        public DateTime? Fechacrea { get; set; }
        public int? FkPersonaledita { get; set; }
        public DateTime? Fechaedita { get; set; }
        public bool? Isdelete { get; set; }

        public virtual ICollection<Producto> Productos { get; set; }
    }
}
