using System;
using System.Collections.Generic;

#nullable disable

namespace ProyectoUSMP_GYM.Models.ModelDB
{
    public partial class Role
    {
        public Role()
        {
            Personaladms = new HashSet<Personaladm>();
        }

        public int PkErol { get; set; }
        public string Descripcion { get; set; }

        public virtual ICollection<Personaladm> Personaladms { get; set; }
    }
}
