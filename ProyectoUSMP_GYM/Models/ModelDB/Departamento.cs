using System;
using System.Collections.Generic;

#nullable disable

namespace ProyectoUSMP_GYM.Models.ModelDB
{
    public partial class Departamento
    {
        public Departamento()
        {
            Provincia = new HashSet<Provincium>();
        }

        public int PkDepartamento { get; set; }
        public string Descripcion { get; set; }

        public virtual ICollection<Provincium> Provincia { get; set; }
    }
}
