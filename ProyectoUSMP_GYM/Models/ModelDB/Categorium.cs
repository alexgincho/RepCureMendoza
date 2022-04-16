using System;
using System.Collections.Generic;

#nullable disable

namespace ProyectoUSMP_GYM.Models.ModelDB
{
    public partial class Categorium
    {
        public Categorium()
        {
            Productos = new HashSet<Producto>();
        }

        public int PkCategoria { get; set; }
        public string Descripcion { get; set; }
        public bool? Isdelete { get; set; }

        public virtual ICollection<Producto> Productos { get; set; }
    }
}
