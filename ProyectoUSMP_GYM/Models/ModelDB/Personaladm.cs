using System;
using System.Collections.Generic;

#nullable disable

namespace ProyectoUSMP_GYM.Models.ModelDB
{
    public partial class Personaladm
    {
        public Personaladm()
        {
            Usuariomembresia = new HashSet<Usuariomembresium>();
        }

        public int PkPersonal { get; set; }
        public string Dni { get; set; }
        public string Nombre { get; set; }
        public string Apellidopaterno { get; set; }
        public string Apellidomaterno { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public string Email { get; set; }
        public int? FkRol { get; set; }
        public string Usuario { get; set; }
        public string Passwords { get; set; }
        public int? FkPersonalcrea { get; set; }
        public DateTime? Fechacrea { get; set; }
        public int? FkPersonaledita { get; set; }
        public DateTime? Fechaedita { get; set; }
        public bool? Isdeleted { get; set; }

        public virtual Role FkRolNavigation { get; set; }
        public virtual ICollection<Usuariomembresium> Usuariomembresia { get; set; }
    }
}
