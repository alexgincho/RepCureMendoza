using System;
using System.Collections.Generic;

#nullable disable

namespace ProyectoUSMP_GYM.Models.ModelDB
{
    public partial class Provincium
    {
        public Provincium()
        {
            Distritos = new HashSet<Distrito>();
        }

        public int PkProvincia { get; set; }
        public string Descripcion { get; set; }
        public int? FkDepartamento { get; set; }

        public virtual Departamento FkDepartamentoNavigation { get; set; }
        public virtual ICollection<Distrito> Distritos { get; set; }
    }
}
