using System;
using System.Collections.Generic;

#nullable disable

namespace ProyectoUSMP_GYM.Models.ModelDB
{
    public partial class Menu
    {
        public int PkMenu { get; set; }
        public string Menu1 { get; set; }
        public string Controller { get; set; }
        public string Actions { get; set; }
        public string Icons { get; set; }
        public int? FkMenupadre { get; set; }
        public int? Tipomenu { get; set; }
        public int? Ordenmenu { get; set; }
    }
}
