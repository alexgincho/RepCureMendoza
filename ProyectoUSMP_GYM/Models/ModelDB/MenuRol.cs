using System;
using System.Collections.Generic;

#nullable disable

namespace ProyectoUSMP_GYM.Models.ModelDB
{
    public partial class MenuRol
    {
        public int? FkMenu { get; set; }
        public int? FkRol { get; set; }

        public virtual Menu FkMenuNavigation { get; set; }
        public virtual Role FkRolNavigation { get; set; }
    }
}
