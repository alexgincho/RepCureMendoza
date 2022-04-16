using System;
using System.Collections.Generic;

#nullable disable

namespace ProyectoUSMP_GYM.Models.ModelDB
{
    public partial class Metodopago
    {
        public int Pkmetodopago { get; set; }
        public string Propietario { get; set; }
        public string Numeroccv { get; set; }
        public string Numerotarjeta { get; set; }
        public string Fechaexpiraciontar { get; set; }
        public bool Tipotarjeta { get; set; }
    }
}
