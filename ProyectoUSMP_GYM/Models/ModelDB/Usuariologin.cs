using System;
using System.Collections.Generic;

#nullable disable

namespace ProyectoUSMP_GYM.Models.ModelDB
{
    public partial class Usuariologin
    {
        public int PkUsuariologin { get; set; }
        public string Usuario { get; set; }
        public string Passwords { get; set; }
        public int? FkUsuario { get; set; }

        public virtual Usuario FkUsuarioNavigation { get; set; }
    }
}
